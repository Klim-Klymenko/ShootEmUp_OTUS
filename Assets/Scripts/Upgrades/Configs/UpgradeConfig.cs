using System;
using UnityEngine;

namespace Sample
{
    public abstract class UpgradeConfig : ScriptableObject
    {
        private protected const float SPACE_HEIGHT = 10.0f;

        [field: SerializeField]
        internal string Id { get; private set; }

        [Range(2, 99)] 
        [SerializeField] 
        internal int MaxLevel = 2;

        [Space(SPACE_HEIGHT)]
        [SerializeField]
        private PriceTable _priceTable;

        internal abstract Upgrade InstantiateUpgrade();

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
        
        private protected virtual void Validate()
        {
            _priceTable.OnValidate(MaxLevel);
        }
        
        internal int GetPrice(int level)
        {
            return _priceTable.GetPrice(level);
        }
    }
}