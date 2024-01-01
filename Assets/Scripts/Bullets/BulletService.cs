using UnityEngine;

namespace ShootEmUp
{
    [System.Serializable]
    public sealed class BulletService
    {
        [SerializeField]
        private GameObject _gameObject;

        [SerializeField] 
        private Transform _transform;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        
        [SerializeField]
        private Rigidbody2D _rigidbody;

        public GameObject GameObject => _gameObject;
        public Transform Transform => _transform;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public Rigidbody2D Rigidbody => _rigidbody;
    }
}