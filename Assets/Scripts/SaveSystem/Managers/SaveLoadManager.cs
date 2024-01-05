using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Zenject;

namespace SaveSystem
{
    [Serializable]
    internal sealed class SaveLoadManager
    {
        private IGameRepository _repository;
        private List<ISaveLoader> _saveLoaders;

        [Inject]
        private void Construct(IGameRepository repository, List<ISaveLoader> saveLoaders)
        {
            _repository = repository;
            _saveLoaders = saveLoaders;
        }
        
        [Button]
        private void Save()
        {
            for (int i = 0; i < _saveLoaders.Count; i++) 
                _saveLoaders[i].Save(_repository);
            
            _repository.SaveState();
        }
        
        [Button]
        private void Load()
        {
            _repository.LoadState();

            for (int i = 0; i < _saveLoaders.Count; i++)
                _saveLoaders[i].Load(_repository);
        }
    }
}