using System;
using System.Collections.Generic;
using System.IO;
using IDZ_Digital.Extensions;
using JetBrains.Annotations;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace SaveSystem
{
    [UsedImplicitly]
    internal sealed class Repository : IGameRepository
    {
        private Dictionary<Type, string> _storage = new();
        
        private const string FileName = "/Storage.json";
        private readonly string _filePath = Application.persistentDataPath + FileName;
        
        void IGameRepository.SetData<T>(T data)
        {
            Type dataType = typeof(T);
            string serializedData = JsonUtility.ToJson(data);
            _storage[dataType] = serializedData;
        }

        T IGameRepository.GetData<T>()
        {
            Type dataType = typeof(T);
            string serializedData = _storage[dataType];
            T deserializedData = JsonUtility.FromJson<T>(serializedData);
            
            return deserializedData;
        }

        bool IGameRepository.TryGetData<T>(out T data)
        {
            Type dataType = typeof(T);
            
            if (_storage.TryGetValue(dataType, out string serializedData))
            {
                data = JsonUtility.FromJson<T>(serializedData);
                return true;
            }
            
            data = default;
            return false;
        }
        
        void IGameRepository.SaveState()
        {
            string serializedStorage = JsonConvert.SerializeObject(_storage);
            string encryptedStorage = AesEncryptor.Encrypt(serializedStorage);
            File.WriteAllText(_filePath, encryptedStorage);
        }
        
        void IGameRepository.LoadState()
        {
            if (!File.Exists(_filePath)) return;
            
            string serializedStorage = File.ReadAllText(_filePath);
            string decryptedStorage = AesEncryptor.Decrypt(serializedStorage);
            _storage = JsonConvert.DeserializeObject<Dictionary<Type, string>>(decryptedStorage);
        }
    }
}