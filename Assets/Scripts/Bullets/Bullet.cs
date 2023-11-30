using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class Bullet : MonoBehaviour, IPoolable, IGameStartListener,
        IGameFinishListener, IGameResumeListener, IGamePauseListener
    {
        public event Action OnBulletDestroyed;
        
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Transform _transform;
        [SerializeField] private GameObject _gameObject;

        private Vector2 _previousVelocity;

        public Transform Transform => _transform;
        public GameObject GameObject => _gameObject;

        public bool IsOnlyUnityMethods { get; } = true;
        
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
            set => _transform.position = value;
            get => _transform.position;
        }

        public Color Color
        {
            set => _spriteRenderer.color = value;
        }
        
        private void OnValidate() => AccessFields();

        private void OnCollisionEnter2D(Collision2D collision) => DealDamage(collision);

        private void AccessFields()
        {
            _transform = transform;
            _gameObject = gameObject;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        
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

        public void OnStart() => enabled = true;

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