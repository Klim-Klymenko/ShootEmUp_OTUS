using System;

namespace SaveSystem
{
    public abstract class SaveLoadMediator<TService, TData> : ISaveLoader
    {
        private readonly TService _service;
        private readonly IGameRepository _repository;
        
        protected SaveLoadMediator(TService service, IGameRepository repository)
        {
            _service = service;
            _repository = repository;
        }
        
        void ISaveLoader.Save()
        {
            TData data = ConvertToData(_service);
            _repository.SetData(data);
        }

        void ISaveLoader.Load()
        {
            if (_repository.TryGetData(out TData data))
                ApplyData(_service, data); 
            else
                throw new Exception($"Data of type {typeof(TData)} not found");
        }
        
        protected abstract TData ConvertToData(TService service);
        protected abstract void ApplyData(TService service, TData data);
    }
}