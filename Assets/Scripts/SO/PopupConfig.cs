using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "PopupConfig", menuName = "Configs/PopupConfig")]
    public sealed class PopupConfig : ScriptableObject
    {
        [SerializeField]
        private string[] _statNames;
        
        [SerializeField]
        private string _name;
        
        [SerializeField]
        private string _description;
        
        [SerializeField]
        private Sprite _icon;
        
        [SerializeField]
        private int[] _values;
        
        public string[] StatNames => _statNames;
        public string Name => _name;
        public string Description => _description;
        public Sprite Icon => _icon;
        public int[] Values => _values;
    }
}