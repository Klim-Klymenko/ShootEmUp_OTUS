using System.Collections.Generic;
using Atomic.Objects;
using Common;
using UnityEngine;
using Zenject;

namespace Objects
{
    internal sealed class ZombieInstaller : MonoInstaller
    {
        [SerializeField]
        private int _reservationAmount;
        
        [SerializeField]
        private Zombie _prefab;

        [SerializeField]
        private Transform _poolContainer;
        
        [SerializeField]
        private List<Transform> _spawnPoints;

        [SerializeField]
        private AtomicObject _character;

        [SerializeField]
        private Transform _characterTransform;

        [SerializeField]
        private AudioClip _deathClip;
        
        public override void InstallBindings()
        {
            BindPool();
            BindPositionGenerator();
            BindSpawner();
        }
        
        private void BindPool()
        {
            Container.Bind<Pool<Zombie>>().AsSingle().WithArguments(_reservationAmount, _prefab, _poolContainer);
        }

        private void BindPositionGenerator()
        {
            Container.Bind<PositionGenerator>().AsSingle().WithArguments(_spawnPoints);
        }
        
        private void BindSpawner()
        {
            Container.Bind<ISpawner<Zombie>>().To<ZombieSpawner>().AsSingle().WithArguments(_characterTransform, _character, _deathClip.length);
        }
    }
}