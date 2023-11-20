using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class Bullet : MonoBehaviour, IPoolable
    {
        public event Action<Bullet, Collision2D> OnCollisionEntered;
        
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Transform _transform;
        [SerializeField] private GameObject _gameObject;

        public Transform Transform => _transform;
        public GameObject GameObject => _gameObject;

        public CohesionType CohesionType { get; set; }
        public int Damage { get; set; }
        
        public Vector2 Velocity
        {
            set => _rigidbody.velocity = value;
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
    }
}