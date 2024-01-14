using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyReferenceComponent : MonoBehaviour, IPoolable
    {
        public Vector3 Position
        {
            set => transform.position = value;  
        } 
        public Transform Transform => transform;
        public GameObject GameObject => gameObject;
        
        private DiContainer _enemyDiContainer;
        
        [Inject]
        public void Construct(DiContainer diContainer)
        {
            _enemyDiContainer = diContainer;
        }
        
        public T Resolve<T>() where T : class => _enemyDiContainer.Resolve<T>();
    }
}