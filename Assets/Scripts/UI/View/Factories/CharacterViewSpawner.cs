using Common;
using JetBrains.Annotations;
using UnityEngine;

namespace PM
{
    [UsedImplicitly]
    internal sealed class CharacterViewSpawner : ISpawner<CharacterView>
    {
        private readonly Pool<CharacterView> _viewPool;
        private readonly Transform _popupPanel; 
        
        internal CharacterViewSpawner(Pool<CharacterView> viewPool, Transform popupPanel)
        {
            _viewPool = viewPool;
            _popupPanel = popupPanel;
        }

        CharacterView ISpawner<CharacterView>.Spawn()
        {
            CharacterView characterView = _viewPool.Get();
            characterView.transform.SetParent(_popupPanel);
            
            return characterView;
        }

        void ISpawner<CharacterView>.Despawn(CharacterView characterView)
        {
            _viewPool.Put(characterView);
        }
    }
}