using Atomic.Extensions;
using UnityEngine;

namespace GameEngine
{
    public static class VisualAPI
    {
        [Contract(typeof(SkinnedMeshRenderer))]
        public const string SkinnedMeshRenderer = nameof(SkinnedMeshRenderer);
    }
}