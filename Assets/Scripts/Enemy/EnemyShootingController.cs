using UnityEngine;

namespace ShootEmUp
{
    public class EnemyShootingController : MonoBehaviour
    {
        private EnemyManager EnemyManager => _referenceComponent.EnemyManager;
        private EnemyAttackAgent AttackAgent => _referenceComponent.AttackAgent;
        
        [SerializeField] private EnemyReferenceComponent _referenceComponent;

        private void OnValidate() => _referenceComponent = GetComponent<EnemyReferenceComponent>();

        private void OnEnable() => AttackAgent.OnFired += RunBullet;

        private void OnDisable() => AttackAgent.OnFired -= RunBullet;

        private void RunBullet(Vector2 startPosition, Vector2 directionToPlayer) =>
            EnemyManager.RunBullet(startPosition, directionToPlayer);
    }
}