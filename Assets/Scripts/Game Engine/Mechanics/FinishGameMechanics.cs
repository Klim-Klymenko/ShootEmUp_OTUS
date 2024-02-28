using Atomic.Elements;
using GameCycle;

namespace GameEngine
{
    public sealed class FinishGameMechanics
    {
        private readonly IAtomicObservable<int> _takeDamageObservable;
        private readonly IAtomicValue<bool> _aliveCondition;
        private readonly GameCycleManager _gameCycleManager;

        public FinishGameMechanics(IAtomicObservable<int> takeDamageObservable, IAtomicValue<bool> aliveCondition, GameCycleManager gameCycleManager)
        {
            _takeDamageObservable = takeDamageObservable;
            _aliveCondition = aliveCondition;
            _gameCycleManager = gameCycleManager;
        }

        public void OnEnable()
        {
            _takeDamageObservable.Subscribe(FinishGame);
        }
        
        public void OnDisable()
        {
            _takeDamageObservable.Unsubscribe(FinishGame);
        }

        private void FinishGame(int _)
        {
            if (_aliveCondition.Value) return;
            
            _gameCycleManager.OnDestroy();
        }
    }
}