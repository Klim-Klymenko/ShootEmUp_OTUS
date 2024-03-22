using Atomic.Elements;
using Common;
using UnityEngine;

namespace GameEngine
{
    public sealed class MoveTimeController
    {
        private readonly IAtomicValue<bool> _moveCondition;
        private readonly IAtomicAction _playAction;
        private readonly IAtomicAction _stopAction;
        private readonly Countdown _countdown;

        public MoveTimeController(IAtomicValue<bool> moveCondition, IAtomicValue<float> duration,
            IAtomicAction playAction, IAtomicAction stopAction)
        {
            _moveCondition = moveCondition;
            _playAction = playAction;
            _stopAction = stopAction;
            _countdown = new Countdown(duration.Value);
            
            _countdown.SetZero();
        }
        
        public void Update()
        {
            if (_moveCondition.Value)
            {
                _countdown.Tick(Time.deltaTime);

                if (_countdown.IsPlaying()) return;

                _playAction?.Invoke();
                _countdown.Reset();
            }
            else
            {
                _stopAction?.Invoke();
                _countdown.SetZero();
            }
        }
    }
}