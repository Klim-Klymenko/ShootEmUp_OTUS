using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour
    {
        public event Action<Vector2, Vector2> OnFired;
        
        public Transform Target { private get; set; }

        public void Fire(Vector2 startPosition)
        {
            if (Target == null)
                return;
            
            Vector2 vectorToPlayer = (Vector2) Target.position - startPosition;
            Vector2 directionToPlayer = vectorToPlayer.normalized;
            OnFired?.Invoke(startPosition, directionToPlayer);
        }
    }
}