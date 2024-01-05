using System;
using SaveSystem;

namespace Domain
{
    internal abstract class SaveLoadMediator<TService, TData> : ISaveLoader
    {
        private readonly TService _service;

        internal SaveLoadMediator(TService service)
        {
            _service = service;
        }
        
        void ISaveLoader.Save(IGameRepository repository)
        {
            TData data = ConvertToData(_service);
            repository.SetData(data);
        }

        void ISaveLoader.Load(IGameRepository repository)
        {
            if (repository.TryGetData(out TData data))
                ApplyData(data); 
            else
                throw new Exception($"Data of type {typeof(TData)} not found");
        }
        
        internal abstract TData ConvertToData(TService service);
        internal abstract void ApplyData(TData data);
    }
}