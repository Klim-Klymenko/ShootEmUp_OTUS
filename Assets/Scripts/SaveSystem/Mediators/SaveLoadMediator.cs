using System;

namespace SaveSystem
{
    public abstract class SaveLoadMediator<TData> : ISaveLoader
    {
        private readonly IGameRepository _repository;
        
        protected SaveLoadMediator(IGameRepository repository)
        {
            _repository = repository;
        }
        
        void ISaveLoader.Save()
        {
            TData data = ConvertToData();
            _repository.SetData(data);
        }

        void ISaveLoader.Load()
        {
            if (_repository.TryGetData(out TData data))
                ApplyData(data); 
            else
                throw new Exception($"Data of type {typeof(TData)} not found");
        }
        
        protected abstract TData ConvertToData();
        protected abstract void ApplyData(TData data);
    }
}