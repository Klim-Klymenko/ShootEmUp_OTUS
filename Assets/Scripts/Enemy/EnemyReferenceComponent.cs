using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    [RequireComponent(typeof(EnemyContextInstaller))]
    public sealed class EnemyReferenceComponent : MonoBehaviour, IPoolable
    {
        public Transform Transform => _transform;
        public GameObject GameObject => _gameObject;
        public DependencyAssembler EnemyDependencyAssembler => _enemyDependencyAssembler;
        public EnemyContextInstaller EnemyContextInstaller => _enemyContextInstaller;
        
        [SerializeField] private Transform _transform;
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private EnemyContextInstaller _enemyContextInstaller;
        private DependencyAssembler _enemyDependencyAssembler;
        
        [Inject]
        private void Construct(DependencyAssembler dependencyAssembler)
        {
            _enemyDependencyAssembler = dependencyAssembler;
        }
        
        private void OnValidate()
        {
            _transform = transform;
            _gameObject = gameObject;
            _enemyContextInstaller = GetComponent<EnemyContextInstaller>();
        }
    }
}