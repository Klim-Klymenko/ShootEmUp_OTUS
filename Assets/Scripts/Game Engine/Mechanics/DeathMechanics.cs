using Atomic.Elements;

namespace GameEngine
{
    public sealed class DeathMechanics
    {
        private readonly IAtomicObservable<int> _hitPoints;
        private readonly IAtomicAction _deathEvent;

        public DeathMechanics(IAtomicObservable<int> hitPoints, IAtomicAction deathEvent)
        {
            _hitPoints = hitPoints;
            _deathEvent = deathEvent;
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
                _deathEvent.Invoke();
        }
    }
}