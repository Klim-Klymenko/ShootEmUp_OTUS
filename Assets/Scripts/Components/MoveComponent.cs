using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class MoveComponent : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rigidbody2D;
        
        [SerializeField]
        private Transform _transform;
        
        [SerializeField] 
        private float _speed = 5.0f;
        
        public Vector2 Position => _transform.position;

        private void OnValidate()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _transform = transform;
        }

        public void Move(Vector2 direction)
        {
            Vector2 nextPosition = _rigidbody2D.position + direction * _speed;
            _rigidbody2D.MovePosition(nextPosition);
        }
    }
}