using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemySystemInstaller : Installer
    {
        [SerializeField, Service]
        private EnemyPositionsGenerator _enemyPositionsGenerator;
        
        [SerializeField, Service, Listener]
        private EnemySpawner _enemySpawner;

        [Listener]
        private EnemySpawnController _enemySpawnController = new();
    }
}