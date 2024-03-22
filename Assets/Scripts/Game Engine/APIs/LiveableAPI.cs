using Atomic.Elements;
using Atomic.Extensions;

namespace GameEngine
{
    public static class LiveableAPI
    {
        [Contract(typeof(IAtomicValue<int>))]
        public const string HitPoints = nameof(HitPoints);
        
        [Contract(typeof(IAtomicAction<int>))]
        public const string TakeDamageAction = nameof(TakeDamageAction);
        
        [Contract(typeof(IAtomicObservable))]
        public const string DeathObservable = nameof(DeathObservable);
    }
}