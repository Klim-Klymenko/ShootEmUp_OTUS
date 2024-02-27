using Atomic.Elements;

namespace GameEngine
{
    public sealed class ReplenishMechanics
    {
        private readonly IAtomicObservable _replenishObservable;
        private readonly IAtomicVariable<int> _charges;

        public ReplenishMechanics(IAtomicObservable replenishObservable, IAtomicVariable<int> charges)
        {
            _replenishObservable = replenishObservable;
            _charges = charges;
        }
        
        public void OnEnable()
        {
            _replenishObservable.Subscribe(OnReplenish);
        }
        
        public void OnDisable()
        {
            _replenishObservable.Unsubscribe(OnReplenish);
        }

        private void OnReplenish()
        {
            _charges.Value++;
        }
    }
}