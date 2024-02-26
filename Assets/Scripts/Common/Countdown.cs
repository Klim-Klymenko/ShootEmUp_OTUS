namespace Common
{
    public sealed class Countdown
    {
        private const int StartTime = 0;
        private float _currentTime;
        
        private readonly float _duration;

        public Countdown(float duration)
        {
            _duration = duration;
            _currentTime = duration;
        }
        
        public void Tick(float deltaTime)
        {
            _currentTime -= deltaTime;
        }

        public void Reset()
        {
            _currentTime = _duration;
        }
        
        public void SetZero()
        {
            _currentTime = StartTime;
        }
        
        public bool IsPlaying()
        {
            return _currentTime > StartTime;
        }

        public bool IsFinished()
        {
            return _currentTime <= StartTime;
        }
    }
}