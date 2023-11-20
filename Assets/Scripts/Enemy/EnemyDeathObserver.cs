using UnityEngine;

namespace ShootEmUp
{
    public class EnemyDeathObserver : MonoBehaviour
    {
        private EnemyManager EnemyManager => _referenceComponent.EnemyManager;
        
        [SerializeField] private HitPointsComponent _hitPointsComponent;

        [SerializeField] private EnemyReferenceComponent _referenceComponent;

        private void OnValidate()
        {
            _referenceComponent = GetComponent<EnemyReferenceComponent>();
            _hitPointsComponent = GetComponent<HitPointsComponent>();
        }

        private void OnEnable() => _hitPointsComponent.OnDeath += UnspawnEnemy;

        private void OnDisable() => _hitPointsComponent.OnDeath -= UnspawnEnemy;

        private void UnspawnEnemy() =>
            EnemyManager.UnspawnEnemy(_referenceComponent);
    }
}