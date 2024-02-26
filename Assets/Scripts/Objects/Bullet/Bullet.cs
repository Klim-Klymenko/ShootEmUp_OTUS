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
        private AtomicValue<float> _speed;

        [SerializeField]
        private AtomicValue<int> _damage;
        
        private AtomicValue<Vector3> _direction;
        
        [SerializeField]
        [Get(ObjectAPI.DeathObservable)]
        private AtomicEvent _attackEvent;
        
        private readonly AtomicVariable<bool> _isAlive = new(true);
        
        private MoveMechanics _moveMechanics;
        private AttackMechanics _attackMechanics;

        private bool _composed;
        
        public override void Compose()
        {
            base.Compose();
            
            _isAlive.Value = true;
            
            _direction = new AtomicValue<Vector3>(_transform.forward);
            _moveMechanics = new MoveMechanics(_direction, _speed, _isAlive, _transform);
            _attackMechanics = new AttackMechanics(_attackEvent, _isAlive, _damage, _target);
            
            _attackMechanics.OnEnable();
            
            _composed = true;
        }

        void IUpdateGameListener.OnUpdate()
        {
            if (!_composed) return;
           
            _moveMechanics.Update();
        }

        public void OnCollisionEnter(Collision other)
        {
            if (!_composed) return;
            
            if (!other.gameObject.TryGetComponent(out AtomicObject atomicObject)) return;

            if (!atomicObject.Is(ObjectTypes.Damageable) || !atomicObject.Is(ObjectTypes.Zombie)) return;
            
            _target.Value = atomicObject;
            _attackEvent?.Invoke();
        }

        public void OnFinish()
        {
            if (!_composed) return;
            
            _isAlive.Value = false;
            
            _attackMechanics.OnDisable();
            Dispose();
        }

        private void Dispose()
        {
            _target?.Dispose();
            _attackEvent?.Dispose();
            _isAlive?.Dispose();
        }

        void IDisposable.Dispose()
        {
            Dispose();
        }
    }
}