using Atomic.Elements;
using GameCycle;

namespace GameEngine
{
    public sealed class FinishGameMechanics
    {
        private readonly IAtomicObservable<int> _takeDamageEvent;
        private readonly IAtomicValue<bool> _finishGameCondition;
        private readonly GameCycleManager _gameCycleManager;

        public FinishGameMechanics(IAtomicObservable<int> takeDamageEvent, IAtomicValue<bool> finishGameCondition, GameCycleManager gameCycleManager)
        {
            _takeDamageEvent = takeDamageEvent;
            _finishGameCondition = finishGameCondition;
            _gameCycleManager = gameCycleManager;
        }

        public void OnEnable()
        {
            _takeDamageEvent.Subscribe(FinishGame);
        }
        
        public void OnDisable()
        {
            _takeDamageEvent.Unsubscribe(FinishGame);
        }

        private void FinishGame(int _)
        {
            if (_finishGameCondition.Value) return;
            
            _gameCycleManager.OnDestroy();
        }
    }
}