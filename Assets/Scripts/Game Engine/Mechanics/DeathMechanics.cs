using Atomic.Elements;

namespace GameEngine
{
    public sealed class DeathMechanics
    {
        private readonly IAtomicObservable<int> _hitPoints;
        private readonly IAtomicAction _deathAction;

        public DeathMechanics(IAtomicObservable<int> hitPoints, IAtomicAction deathAction)
        {
            _hitPoints = hitPoints;
            _deathAction = deathAction;
        }
        
        public void OnEnable()
        {
            _hitPoints.Subscribe(OnTakeDamage);
        }
        
        public void OnDisable()
        {
            _hitPoints.Unsubscribe(OnTakeDamage);
        }
        
        private void OnTakeDamage(int hitPoints)
        {
            if (hitPoints <= 0)
                _deathAction.Invoke();
        }
    }
}