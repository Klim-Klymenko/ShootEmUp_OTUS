using Atomic.Extensions;

namespace Objects
{
    internal static class ZombieAPI
    {
        [Contract(typeof(ZombieAnimatorDispatcher))]
        internal const string ZombieAnimatorDispatcher = nameof(ZombieAnimatorDispatcher);
    }
}