using Atomic.Elements;
using Common;
using UnityEngine;

namespace GameEngine
{
    public sealed class CooldownMechanics
    {
        private readonly IAtomicAction _timeOverAction;
        private readonly IAtomicValue<bool> _cooldownCondition;
        private readonly Countdown _countdown;

        public CooldownMechanics(IAtomicAction timeOverAction, IAtomicValue<float> cooldownInterval, IAtomicValue<bool> cooldownCondition)
        {
            _timeOverAction = timeOverAction;
            _cooldownCondition = cooldownCondition;
            _countdown = new Countdown(cooldownInterval.Value);
        }

        public void Update()
        {
            if (!_cooldownCondition.Value)
            {
                _countdown.Reset();
                return;
            }
            
            _countdown.Tick(Time.deltaTime);
            
            if (_countdown.IsPlaying()) return;
            
            _timeOverAction.Invoke();
            _countdown.Reset();
        }
    }
}