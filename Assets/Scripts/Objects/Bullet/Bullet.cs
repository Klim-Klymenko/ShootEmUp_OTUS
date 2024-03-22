using System;
using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using GameCycle;
using GameEngine;
using UnityEngine;

namespace Objects
{
    internal sealed class Bullet : AtomicObject, IDisposable, IUpdateGameListener, IFinishGameListener
    {
        [SerializeField]
        private Transform _transform;
        
        private readonly AtomicEvent<AtomicObject> _attackEvent = new();
        
        [Get(LiveableAPI.DeathObservable)]
        private readonly AtomicEvent _deathEvent = new();
        
        private readonly AtomicVariable<bool> _aliveCondition = new();
        private readonly BulletTakeDamageCondition _bulletTakeDamageCondition = new();

        [SerializeField]
        private MoveComponent _moveComponent;

        [SerializeField]
        private AttackComponent _attackComponent;
        
        private PassOnTargetMechanics _passOnTargetMechanics;

        private bool _composed;
        
        public override void Compose()
        {
            base.Compose();
            
           AtomicVariable<Vector3> moveDirection = new(_transform.forward);

           _moveComponent.Let(it =>
           {
               it.Compose(_transform, moveDirection);
               it.MoveCondition.Append(_aliveCondition);
           });

           _attackComponent.Let(it =>
           {
               it.Compose(_attackEvent);
               it.AttackCondition.Append(_aliveCondition);
           });
           
            _passOnTargetMechanics = new PassOnTargetMechanics(_bulletTakeDamageCondition, _attackEvent);

            _attackComponent.OnEnable();

            _aliveCondition.Value = true;
            _composed = true;
        }

        void IUpdateGameListener.OnUpdate()
        {
            if (!_composed) return;
           
            _moveComponent.Update();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!_composed) return;
            
           _passOnTargetMechanics.OnCollisionEnter(other);
           _deathEvent?.Invoke();
           _aliveCondition.Value = false;
        }

        public void OnFinish()
        {
            if (!_composed) return;
            
            _attackComponent.OnDisable();
            Dispose();
        }

        public void Dispose()
        {
            _attackEvent?.Dispose();
            _deathEvent?.Dispose();
        }
    }
}