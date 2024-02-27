using Atomic.Elements;
using Atomic.Extensions;
using UnityEngine;

namespace GameEngine
{
    public static class AiAPI
    {
        [Contract(typeof(IAtomicVariable<Transform>))]
        public const string AgentTargetTransform = nameof(AgentTargetTransform);
    }
}