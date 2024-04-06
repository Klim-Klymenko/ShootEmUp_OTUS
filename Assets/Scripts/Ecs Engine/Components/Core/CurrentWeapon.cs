using System;
using EcsEngine.Extensions;
using Leopotam.EcsLite;
using UnityEngine;

namespace EcsEngine.Components
{
    [Serializable]
    public struct CurrentWeapon
    {
        [SerializeField]
        private Entity _entity;
        
        public EcsPackedEntity Value => _entity.PackedEntity;
    }
}