using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(SwitchStateComponent))]
    public sealed class Bullet : MonoBehaviour, IPoolable, IGameStartListener,
        IGameFinishListener, IGameResumeListener, IGamePauseListener
    {
        public event Action<Bullet, Collision2D> OnCollisionEntered;
        
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Transform _transform;
        [SerializeField] private GameObject _gameObject;

        private Vector2 _previousVelocity;
        
        private SwitchStateComponent _switchComponent;

        public SwitchStateComponent SwitchComponent
        {
            set => _switchComponent = value;
        }

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

        private void OnCollisionEnter2D(Collision2D collision) => OnCollisionEntered?.Invoke(this, collision);

        private void AccessFields()
        {
            _transform = transform;
            _gameObject = gameObject;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void OnStart() => _switchComponent.TurnOn(this);

        void IGameFinishListener.OnFinish()
        {
            ResetVelocity();
            _switchComponent.TurnOff(this);
        }
        void IGameResumeListener.OnResume()
        {
            _switchComponent.TurnOn(this);
            ReturnVelocity();
        }

        void IGamePauseListener.OnPause()
        {
            ResetVelocity();
            _switchComponent.TurnOff(this);
        }

        private void ResetVelocity()
        {
            _previousVelocity = Velocity;
            Velocity = Vector2.zero;
        }

        private void ReturnVelocity() => Velocity = _previousVelocity;
    }
}