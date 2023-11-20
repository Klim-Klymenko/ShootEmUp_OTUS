using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPositionsGenerator : MonoBehaviour
    {
        [SerializeField] private Transform[] spawnPositions;

        [SerializeField] private Transform[] attackPositions;

        public Vector2 RandomSpawnPosition()
        {
            return RandomTransform(spawnPositions);
        }

        public Vector2 RandomAttackPosition()
        {
            return RandomTransform(attackPositions);
        }

        private Vector2 RandomTransform(Transform[] transforms)
        {
            var index = Random.Range(0, transforms.Length);
            return transforms[index].position;
        }
    }
}