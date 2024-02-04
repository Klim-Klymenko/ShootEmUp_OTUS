using UnityEngine;
using Zenject;

namespace PM
{
    internal sealed class CharacterViewFactory : Common.IFactory<CharacterView>
    {
        private readonly CharacterView _prefab;
        private readonly DiContainer _diContainer;
        
        internal CharacterViewFactory(CharacterView prefab, DiContainer diContainer)
        {
            _prefab = prefab;
            _diContainer = diContainer;
        }
        
        CharacterView Common.IFactory<CharacterView>.Create()
        {
            CharacterView characterView = Object.Instantiate(_prefab);
            _diContainer.InjectGameObject(characterView.gameObject);
            
            return characterView;
        }
    }
}