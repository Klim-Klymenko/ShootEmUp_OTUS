using System;
using SaveSystem;

namespace Domain
{
    public abstract class SaveLoadMediator<TService, TData> : ISaveLoader
    {
        private readonly TService _service;

        protected SaveLoadMediator(TService service)
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
        
        protected abstract TData ConvertToData(TService service);
        protected abstract void ApplyData(TData data);
    }
}