using UnityEngine;

namespace ShootEmUp
{
    public sealed class TeamComponent : MonoBehaviour
    {
        public CohesionType CohesionType => _cohesionType;

        [SerializeField] 
        private CohesionType _cohesionType;
    }
}