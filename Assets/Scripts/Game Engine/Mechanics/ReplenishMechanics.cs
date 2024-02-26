using Atomic.Elements;

namespace GameEngine
{
    public sealed class ReplenishMechanics
    {
        private readonly IAtomicObservable _replenishEvent;
        private readonly IAtomicVariable<int> _charges;

        public ReplenishMechanics(IAtomicObservable replenishEvent, IAtomicVariable<int> charges)
        {
            _replenishEvent = replenishEvent;
            _charges = charges;
        }
        
        public void OnEnable()
        {
            _replenishEvent.Subscribe(OnReplenish);
        }
        
        public void OnDisable()
        {
            _replenishEvent.Unsubscribe(OnReplenish);
        }

        private void OnReplenish()
        {
            _charges.Value++;
        }
    }
}