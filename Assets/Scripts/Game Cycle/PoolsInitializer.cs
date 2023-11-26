using UnityEngine;

namespace ShootEmUp
{
    public sealed class PoolsInitializer : MonoBehaviour, IGameInitializeListener
    {
        [SerializeField] private EnemyBuilder _enemyBuilder;
        [SerializeField] private BulletBuilder _bulletBuilder;

        void IGameInitializeListener.OnInitialize()
        {
            _enemyBuilder.InitializePool();
            _bulletBuilder.InitializePool();
        }
    }
}