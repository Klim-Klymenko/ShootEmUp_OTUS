using Atomic.Elements;
using Atomic.Extensions;

namespace GameEngine
{
    public static class SwitchableAPI
    {
        [Contract(typeof(IAtomicAction))] 
        public const string SwitchOnAction = nameof(SwitchOnAction);
        
        [Contract(typeof(IAtomicAction))] 
        public const string SwitchOffAction = nameof(SwitchOffAction);
    }
}