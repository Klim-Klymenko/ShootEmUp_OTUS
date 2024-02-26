using Atomic.Elements;
using Common;
using UnityEngine;

namespace GameEngine
{
    public sealed class MoveTimeFXController
    {
        private readonly IAtomicValue<bool> _moveCondition;
        private readonly IAtomicAction _playEvent;
        private readonly IAtomicAction _stopEvent;
        private readonly Countdown _countdown;

        public MoveTimeFXController(IAtomicValue<bool> moveCondition, IAtomicValue<float> duration,
            IAtomicAction playEvent, IAtomicAction stopEvent)
        {
            _moveCondition = moveCondition;
            _playEvent = playEvent;
            _stopEvent = stopEvent;
            _countdown = new Countdown(duration.Value);
            
            _countdown.SetZero();
        }

        public void OnEnable()
        {
            _playEvent?.Invoke();
        }
        
        public void Update()
        {
            if (_moveCondition.Value)
            {
                _countdown.Tick(Time.deltaTime);

                if (_countdown.IsPlaying()) return;

                _playEvent?.Invoke();
                _countdown.Reset();
            }
            else
            {
                _stopEvent?.Invoke();
                _countdown.SetZero();
            }
        }
    }
}