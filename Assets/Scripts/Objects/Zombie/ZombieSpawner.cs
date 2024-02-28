using System.Threading.Tasks;
using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using Common;
using GameCycle;
using GameEngine;
using JetBrains.Annotations;
using UnityEngine;

namespace Objects
{
    [UsedImplicitly]
    internal sealed class ZombieSpawner : ISpawner<Zombie>
    {
        private const int MilliSecondsInSecond = 1000;
        
        private readonly Pool<Zombie> _pool;
        private readonly GameCycleManager _gameCycleManager;
        private readonly PositionGenerator _positionGenerator;
        private readonly Transform _characterTransform;
        private readonly AtomicObject _character;
        private readonly float _deathClipLength;

        internal ZombieSpawner(Pool<Zombie> pool, GameCycleManager gameCycleManager, PositionGenerator positionGenerator,
            Transform characterTransform, AtomicObject character, float deathClipLength)
        {
            _pool = pool;
            _gameCycleManager = gameCycleManager;
            _positionGenerator = positionGenerator;
            _characterTransform = characterTransform;
            _character = character;
            _deathClipLength = deathClipLength;
        }

        public Zombie Spawn()
        {
            Zombie zombie = _pool.Get();
            
            zombie.Compose();
            
            if (zombie.Is(ObjectTypes.Damageable))
            {
                IAtomicObservable deathEvent = zombie.GetObservable(LiveableAPI.DeathObservable);
                deathEvent.Subscribe(() => Despawn(zombie));
            }
            
            if (zombie.Is(ObjectTypes.NavMeshAgent))
            {
                IAtomicVariable<Transform> agentTargetTransform = zombie.GetVariable<Transform>(AiAPI.AgentTargetTransform);
                agentTargetTransform.Value = _characterTransform;
            }

            if (zombie.Is(ObjectTypes.Attacker))
            {
                ZombieAnimatorDispatcher animatorDispatcher = zombie.Get<ZombieAnimatorDispatcher>(ZombieAPI.ZombieAnimatorDispatcher);
                animatorDispatcher.Target = _character;
                
                IAtomicVariable<Transform> attackTargetTransform = zombie.GetVariable<Transform>(AttackerAPI.AttackTargetTransform);
                attackTargetTransform.Value = _characterTransform;
            }
            
            if (!_gameCycleManager.ContainsListener(zombie))
                _gameCycleManager.AddListener(zombie);
            
            zombie.transform.position = _positionGenerator.GetRandomPosition();

            return zombie;
        }
        
        public async void Despawn(Zombie zombie)
        {
            SkinnedMeshRenderer meshRenderer = zombie.Get<SkinnedMeshRenderer>(VisualAPI.SkinnedMeshRenderer);
            
            if (meshRenderer.enabled)
                meshRenderer.enabled = false;
            
            zombie.OnFinish();
            _gameCycleManager.RemoveListener(zombie);

            await Task.Delay((int) (_deathClipLength * MilliSecondsInSecond));
            
            if (zombie == null) return;
            
            _pool.Put(zombie);
        }
    }
}