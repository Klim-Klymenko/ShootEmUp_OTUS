using Common;
using EcsEngine.Extensions;
using GameCycle;
using UnityEngine;
using Zenject;

namespace Objects.DIInstallers
{
    internal sealed class PoolInstaller : MonoInstaller, IInitializeGameListener
    {
        [SerializeField] 
        private int _poolSize;
        
        [SerializeField]
        private Entity _archerRedPrefab;
        
        [SerializeField]
        private Entity _archerBluePrefab;
        
        [SerializeField]
        private Entity _swordsmanRedPrefab;
        
        [SerializeField]
        private Entity _swordsmanBluePrefab;

        [SerializeField]
        private Entity _arrowRedPrefab;
        
        [SerializeField]
        private Entity _arrowBluePrefab;
        
        [SerializeField]
        private Entity _baseRedPrefab;
        
        [SerializeField]
        private Entity _baseBluePrefab;
        
        [SerializeField]
        private Transform _unitContainer;
        
        [SerializeField]
        private Transform _arrowContainer;

        [SerializeField]
        private Transform _buildingContainer;
        
        private Pool[] _pools;
        
        public override void InstallBindings()
        {
            BindPools();
        }

        void IInitializeGameListener.OnInitialize()
        {
            for (int i = 0; i < _pools.Length; i++)
                _pools[i].Reserve();
        }
        
        private void BindPools()
        {
            Container.Bind<Pool[]>().FromMethod(CreatePools).AsSingle();
        }
        
        private Pool[] CreatePools()
        {
            _pools = new Pool[]
            {
                new Pool(_poolSize, _archerRedPrefab, _unitContainer, Container),
                new Pool(_poolSize, _archerBluePrefab, _unitContainer, Container),
                new Pool(_poolSize, _swordsmanRedPrefab, _unitContainer, Container),
                new Pool(_poolSize, _swordsmanBluePrefab, _unitContainer, Container),
                new Pool(_poolSize, _arrowRedPrefab, _arrowContainer, Container),
                new Pool(_poolSize, _arrowBluePrefab, _arrowContainer, Container),
                new Pool(_poolSize, _baseRedPrefab, _buildingContainer, Container),
                new Pool(_poolSize, _baseBluePrefab, _buildingContainer, Container),
            };

            return _pools;
        }
    }
}