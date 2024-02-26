using System;

namespace Common
{
    public sealed class Timer
    {
        public event Action OnTimeOver;

        private const int StartTime = 0;
        private float _currentTime;
        
        private readonly float _duration;

        public Timer(float duration)
        {
            _duration = duration;
            _currentTime = duration;
        }
        
        public void Tick(float deltaTime)
        {
            _currentTime -= deltaTime;
            
            if (_currentTime <= StartTime)
            {
                OnTimeOver?.Invoke();
                _currentTime = _duration;
            }
        }
    }
}