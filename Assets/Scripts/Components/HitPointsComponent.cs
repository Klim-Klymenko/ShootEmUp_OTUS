using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        public event Action OnDeath;
        
        [SerializeField]
        private int _hitPoints;
        
        public bool AnyHitPoints => _hitPoints > 0;

        public void TakeDamage(int damage)
        {
            _hitPoints -= damage;
            
            if (_hitPoints > 0)
                return;
            
            OnDeath?.Invoke();
        }
    }
}