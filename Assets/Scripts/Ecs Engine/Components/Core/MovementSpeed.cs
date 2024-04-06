﻿using System;
using UnityEngine;

namespace EcsEngine.Components
{
    [Serializable]
    public struct MovementSpeed
    {
        [field: SerializeField]
        public float Value { get; private set; }
    }
}