using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputController : MonoBehaviour
    {
        [SerializeField] private MoveComponent _characterMoveComponent;
        [SerializeField] private CharacterBulletManager bulletManager;

        private float _horizontal;
        
        private void Update()
        {
            _horizontal = Input.GetAxis("Horizontal");
            
            if (Input.GetKeyDown(KeyCode.Space))
                bulletManager.RunBullet();
        }
        
        private void FixedUpdate()
        {
            _characterMoveComponent.Move(new Vector2(_horizontal, 0) * Time.fixedDeltaTime);
        }
    }
}