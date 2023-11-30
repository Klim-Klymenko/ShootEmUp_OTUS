using UnityEngine;

namespace ShootEmUp
{
    public sealed class PoolsInitializer : MonoBehaviour, IGameInitializeListener
    {
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private BulletSpawner bulletSpawner;

        void IGameInitializeListener.OnInitialize()
        {
            enemySpawner.InitializePool();
            bulletSpawner.InitializePool();
        }
    }
}