using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBounds : MonoBehaviour
    {
        [SerializeField]
        private Transform leftBorder;

        [SerializeField]
        private Transform rightBorder;

        [SerializeField]
        private Transform downBorder;

        [SerializeField]
        private Transform topBorder;
        
        public bool InBounds(Vector3 position)
        {
            return position.x > leftBorder.position.x
                   && position.x < rightBorder.position.x
                   && position.y > downBorder.position.y
                   && position.y < topBorder.position.y;
        }
    }
}