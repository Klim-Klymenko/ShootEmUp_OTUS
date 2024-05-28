using System;
using UnityEngine;

namespace Sample
{
    public abstract class UpgradeConfig : ScriptableObject
    {
        protected const float SPACE_HEIGHT = 10.0f;

        [field: SerializeField]
        public string Id { get; private set; }

        [Range(2, 99)] 
        [SerializeField] 
        public int MaxLevel = 2;

        [Space(SPACE_HEIGHT)]
        [SerializeField]
        private PriceTable _priceTable;

        public abstract Upgrade InstantiateUpgrade();

        private void OnValidate()
        {
            try
            {
                Validate();
            }
            catch (Exception)
            {
                // ignored
            }
        }
        
        protected virtual void Validate()
        {
            _priceTable.OnValidate(MaxLevel);
        }
        
        public int GetPrice(int level)
        {
            return _priceTable.GetPrice(level);
        }
    }
}