using System;
using Common.GameEngine;
using UnityEngine;

namespace EcsEngine.Components
{
    [Serializable]
    public struct WeaponType
    {
        [field: SerializeField]
        public Weapon Value { get; private set; }
    }
}