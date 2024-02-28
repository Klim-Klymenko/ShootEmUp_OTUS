using Atomic.Elements;

namespace GameEngine
{
    public sealed class DeathMechanics
    {
        private readonly IAtomicObservable<int> _takeDamageObservable;
        private readonly IAtomicAction _deathAction;

        public DeathMechanics(IAtomicObservable<int> takeDamageObservable, IAtomicAction deathAction)
        {
            _takeDamageObservable = takeDamageObservable;
            _deathAction = deathAction;
        }
        
        public void OnEnable()
        {
            _takeDamageObservable.Subscribe(OnTakeDamage);
        }
        
        public void OnDisable()
        {
            _takeDamageObservable.Unsubscribe(OnTakeDamage);
        }
        
        private void OnTakeDamage(int hitPoints)
        {
            if (hitPoints <= 0)
                _deathAction.Invoke();
        }
    }
}