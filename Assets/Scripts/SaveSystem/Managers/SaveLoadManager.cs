using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Zenject;

namespace SaveSystem
{
    [Serializable]
    public sealed class SaveLoadManager
    {
        private IGameRepository _repository;
        private List<ISaveLoader> _saveLoaders;

        [Inject]
        public void Construct(IGameRepository repository, List<ISaveLoader> saveLoaders)
        {
            _repository = repository;
            _saveLoaders = saveLoaders;
        }
        
        [Button]
        public void Save()
        {
            for (int i = 0; i < _saveLoaders.Count; i++) 
                _saveLoaders[i].Save();
            
            _repository.SaveState();
        }
        
        [Button]
        public void Load()
        {
            _repository.LoadState();

            for (int i = 0; i < _saveLoaders.Count; i++)
                _saveLoaders[i].Load();
        }
    }
}