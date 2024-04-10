using EcsEngine.Components;
using EcsEngine.Components.Requests;
using EcsEngine.Extensions;
using GameCycle;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Common.GameEngine
{
    internal sealed class UnitSpawnTester : MonoBehaviour
    {
        [SerializeField]
        private Entity[] _prefabs;
        
        [SerializeField] 
        private Transform[] _spawnPoints;
        
        private EcsEntityBuilder _entityBuilder;
        private GameCycleManager _gameCycleManager;

        [Inject]
        internal void Construct(EcsEntityBuilder entityBuilder, GameCycleManager gameCycleManager)
        {
            _entityBuilder = entityBuilder;
            _gameCycleManager = gameCycleManager;
        }
        
        [Button]
        internal void SpawnUnit()
        {
            if (_gameCycleManager.GameState == GameState.Finished) return;
        
            Entity randomPrefab = _prefabs[Random.Range(0, _prefabs.Length)];
            Transform randomSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
            
            _entityBuilder.CreateEntity()
                .Add(new SpawnRequest())
                .Add(new Spawn { Prefab = randomPrefab, SpawnPoint = randomSpawnPoint });
        }
    }
}