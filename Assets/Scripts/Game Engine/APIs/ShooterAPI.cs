using Atomic.Elements;
using Atomic.Extensions;

namespace GameEngine
{
    public static class ShooterAPI
    {
        [Contract(typeof(IAtomicAction))]
        public const string ShootAction = nameof(ShootAction);

        [Contract(typeof(IAtomicValue<int>))] 
        public const string ShootingInterval = nameof(ShootingInterval);
    }
}