using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent
    {
        public Transform Target { private get; set; }

        public void Fire(Vector2 startPosition, IBulletSpawner bulletSpawner)
        {
            if (Target == null) return;
            
            Vector2 vectorToPlayer = (Vector2) Target.position - startPosition;
            Vector2 directionToPlayer = vectorToPlayer.normalized;
            
            ShootBullet(startPosition, directionToPlayer, bulletSpawner);
        }
        
        private void ShootBullet(Vector2 startPosition, Vector2 directionToPlayer, IBulletSpawner bulletSpawner)
        {
            bulletSpawner.SpawnBullet(new Args
            {
                CohesionType = CohesionType.Enemy,
                PhysicsLayer = (int) PhysicsLayer.ENEMY_BULLET,
                Color = Color.red,
                Damage = 1,
                Position = startPosition,
                Velocity = directionToPlayer * 2.0f
            });
        }
    }
}