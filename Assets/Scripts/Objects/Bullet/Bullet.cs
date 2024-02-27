using System;
using Atomic.Elements;
using Atomic.Objects;
using GameCycle;
using GameEngine;
using UnityEngine;
using UnityEngine.Serialization;

namespace Objects
{
    public sealed class Bullet : AtomicObject, IDisposable, IUpdateGameListener, IFinishGameListener
    {
        [SerializeField]
        private Transform _transform;

        [SerializeField] 
        private AtomicVariable<AtomicObject> _target;

        [SerializeField]
        private AtomicValue<int> _damage;

        [SerializeField]
        private MoveComponent _moveComponent;

        [SerializeField]
        [HideInInspector]
        [Get(LiveableAPI.DeathObservable)]
        private AtomicEvent _attackEvent;
        
        private readonly AtomicVariable<bool> _aliveCondition = new(true);
        
        private AttackMechanics _attackMechanics;

        private bool _composed;
        
        public override void Compose()
        {
            base.Compose();
            
            AtomicVariable<Vector3> direction = new(_transform.forward);
            _moveComponent.Compose(_transform, _aliveCondition, direction);
            
            _attackMechanics = new AttackMechanics(_attackEvent, _aliveCondition, _damage, _target);
            
            _attackMechanics.OnEnable();

            _aliveCondition.Value = true;
            _composed = true;
        }

        void IUpdateGameListener.OnUpdate()
        {
            if (!_composed) return;
           
            _moveComponent.Update();
        }

        public void OnCollisionEnter(Collision other)
        {
            if (!_composed) return;
            
            //TODO: probably refactor this
            
            if (!other.gameObject.TryGetComponent(out AtomicObject atomicObject)) return;

            if (!atomicObject.Is(ObjectTypes.Damageable) || !atomicObject.Is(ObjectTypes.Zombie)) return;
            
            _target.Value = atomicObject;
            _attackEvent?.Invoke();
        }

        public void OnFinish()
        {
            if (!_composed) return;
            
            _aliveCondition.Value = false;
            
            _attackMechanics.OnDisable();
            Dispose();
        }

        private void Dispose()
        {
            _target?.Dispose();
            _attackEvent?.Dispose();
            _aliveCondition?.Dispose();
        }

        void IDisposable.Dispose()
        {
            Dispose();
        }
    }
}