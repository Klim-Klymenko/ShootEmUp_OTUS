using Atomic.Elements;
using Atomic.Extensions;

namespace GameEngine
{
    public static class GunAPI
    {
        [Contract(typeof(IAtomicAction))] 
        public const string SwitchOnEvent = nameof(SwitchOnEvent);
        
        [Contract(typeof(IAtomicAction))] 
        public const string SwitchOffEvent = nameof(SwitchOffEvent);
    }
}