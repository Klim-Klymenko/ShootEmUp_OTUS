using System;
using Atomic.Elements;
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
        
        private readonly AtomicVariable<bool> _aliveCondition = new(true);

        private AtomicVariable<Vector3> _moveDirection;

        [SerializeField] 
        [HideInInspector] 
        private BulletTakeDamageCondition _bulletTakeDamageCondition;

        [SerializeField]
        private MoveComponent _moveComponent;

        [SerializeField]
        private AttackComponent _attackComponent;
        
        private PassTargetMechanics _passTargetMechanics;

        private bool _composed;
        
        public override void Compose()
        {
            base.Compose();
            
           _moveDirection = new AtomicVariable<Vector3>(_transform.forward);
            
            _moveComponent.Compose(_transform, _aliveCondition, _moveDirection);
            
            _attackComponent.Compose(_attackEvent, _aliveCondition);
            _passTargetMechanics = new PassTargetMechanics(_bulletTakeDamageCondition, _attackEvent);

            _attackComponent.OnEnable();

            _aliveCondition.Value = true;
            _composed = true;
        }

        void IUpdateGameListener.OnUpdate()
        {
            if (!_composed) return;
           
            _moveComponent.Update();
        }

        internal void OnCollisionEnter(Collision other)
        {
            if (!_composed) return;
            
           _passTargetMechanics.OnCollisionEnter(other);
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
            _aliveCondition?.Dispose();
            _moveDirection?.Dispose();
        }
    }
}