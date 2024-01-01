using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour, IPoolable, IGameStartListener,
        IGameFinishListener, IGameResumeListener, IGamePauseListener
    {
        public event Action OnBulletDestroyed;
        
        private Vector2 _previousVelocity;

        public Transform Transform => transform;
        public GameObject GameObject => gameObject;

        public CohesionType CohesionType { get; set; }
        public int Damage { get; set; }
        
        public Vector2 Velocity
        {
            set => _rigidbody.velocity = value;
            private get => _rigidbody.velocity;
        }

        public int PhysicsLayer
        {
            set => gameObject.layer = value;
        }

        public Vector3 Position
        {
            set => transform.position = value;
            get => transform.position;
        }

        public Color Color
        {
            set => _spriteRenderer.color = value;
        }
     
        private Rigidbody2D _rigidbody;
        private SpriteRenderer _spriteRenderer;
        private DiContainer _diContainer;

        [Inject]
        public void Construct(DiContainer diContainer, BulletService bulletService)
        {
            _diContainer = diContainer;
            _rigidbody = bulletService.Rigidbody;
            _spriteRenderer = bulletService.SpriteRenderer;
        }
        
        public T Resolve<T>() where T : class => _diContainer.Resolve<T>();
        
        private void OnCollisionEnter2D(Collision2D collision) => DealDamage(collision);
        
        private void DealDamage(Collision2D collision)
        {
            if (!collision.gameObject.TryGetComponent(out TeamComponent teamComponent))
                return;
            
            if (teamComponent.CohesionType == CohesionType)
                return;
            
            if (!collision.gameObject.TryGetComponent(out HitPointsComponent hitPointsComponent))
                return;
            
            hitPointsComponent.TakeDamage(Damage);
            OnBulletDestroyed?.Invoke();
        }

        void IGameStartListener.OnStart() => enabled = true;

        void IGameFinishListener.OnFinish()
        {
            ResetVelocity();
            
            enabled = false;
        }
        void IGameResumeListener.OnResume()
        {
            enabled = true;
            
            ReturnVelocity();
        }

        void IGamePauseListener.OnPause()
        {
            ResetVelocity();

            enabled = false;
        }

        private void ResetVelocity()
        {
            _previousVelocity = Velocity;
            Velocity = Vector2.zero;
        }

        private void ReturnVelocity() => Velocity = _previousVelocity;
    }
}