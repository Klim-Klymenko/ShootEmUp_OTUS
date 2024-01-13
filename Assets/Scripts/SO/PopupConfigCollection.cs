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
    }
}