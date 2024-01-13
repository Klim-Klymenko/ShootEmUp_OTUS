using UnityEngine;
using PM;

namespace Factory
{
    public sealed class PopupViewFactory : IFactory<PopupView>
    {
        private readonly PopupView _prefab;
        private readonly Transform _parent;
        
        public PopupViewFactory(PopupView prefab, Transform parent)
        {
            _prefab = prefab;
            _parent = parent;
        }
        
        PopupView IFactory<PopupView>.Create()
        {
            return Object.Instantiate(_prefab, _parent);
        }
    }
}