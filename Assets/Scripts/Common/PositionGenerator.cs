using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Common
{
    [UsedImplicitly]
    public sealed class PositionGenerator
    {
        private readonly List<Transform> _points;

        public PositionGenerator(List<Transform> points)
        {
            _points = points;
        }

        public Vector3 GetRandomPosition()
        {
            int index = Random.Range(0, _points.Count);
            return _points[index].position;
        }
    }
}