using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField] private EnemyBuilder _enemyBuilder;
        [SerializeField] private BulletManager _bulletManager;
        
        public int ReservationsAmount => _enemyBuilder.ReservationAmount;
        
        public void SpawnEnemy() => _enemyBuilder.SpawnEnemy();

        public void UnspawnEnemy(EnemyReferenceComponent enemy) => _enemyBuilder.UnspawnEnemy(enemy);

        public void RunBullet(Vector2 startPosition, Vector2 directionToPlayer)
        {
            _bulletManager.SpawnBullet(new Args
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