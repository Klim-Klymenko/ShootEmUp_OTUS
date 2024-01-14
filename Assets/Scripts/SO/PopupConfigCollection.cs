using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "PopupConfigCollection", menuName = "Configs/PopupConfigCollection")]
    public sealed class PopupConfigCollection : ScriptableObject
    {
        [SerializeField]
        private List<PopupConfig> _popupConfigs;
        public List<PopupConfig> PopupConfigs => _popupConfigs;
        
        public int Count => _popupConfigs.Count;

        private int _currentIndex;

        public PopupConfig GetPopupConfig()
        {
            if (_currentIndex >= _popupConfigs.Count)
                _currentIndex = 0;
            
            PopupConfig popupConfig = _popupConfigs[_currentIndex];
            _currentIndex++;
            
            return popupConfig;
        }
    }
}