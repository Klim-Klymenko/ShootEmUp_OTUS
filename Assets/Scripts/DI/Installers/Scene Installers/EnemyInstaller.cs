using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyInstaller : DependencyInstaller
    {
        [SerializeField, Service]
        private EnemyPositionsGenerator _enemyPositionsGenerator;
        
        [SerializeField, Service, Listener]
        private EnemySpawner _enemySpawner;

        [Listener]
        private EnemySpawnController _enemySpawnController = new();
    }
}