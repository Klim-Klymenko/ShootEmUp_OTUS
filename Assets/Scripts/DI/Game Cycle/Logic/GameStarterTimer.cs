using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameStarterTimer
    {
        public event Action OnGameStarted;

        private const int InitialSecondsAmount = 3;
        private const int TimerFinishTime = 0;
        
        private int _secondsToStart;
        private float _secondsToStartDecimal;

        public void InitTimer() => _secondsToStartDecimal = _secondsToStart = InitialSecondsAmount;

        public void TimerCountdown(bool isRun)
        {
            if (!isRun) return;

            if (_secondsToStartDecimal > TimerFinishTime)
            {
                if (InitialSecondsAmount == _secondsToStart)
                    Debug.Log(_secondsToStart);
                    

                _secondsToStartDecimal -= Time.deltaTime;

                int ceiledSecondsToStart = Mathf.CeilToInt(_secondsToStartDecimal);
                if (ceiledSecondsToStart < _secondsToStart)
                {
                    _secondsToStart = ceiledSecondsToStart;
                    Debug.Log(_secondsToStart);
                }
            }
            else
            {
                OnGameStarted?.Invoke();
                OnGameStarted = null;
            }
        }
    }
}