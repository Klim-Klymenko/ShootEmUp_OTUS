using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameStarterTimer : MonoBehaviour
    {
        public event Action OnGameStarted;

        [SerializeField] private int _initialSecondsAmount = 3;
        private int _secondsToStart;
        private float _secondsToStartDecimal;
        private const int TimerFinishTime = 0;

        private void Awake() => _secondsToStartDecimal = _secondsToStart = _initialSecondsAmount;
        
        public void TimerCountdown(bool isRun)
        {
            if (!isRun)
                return;

            if (_secondsToStartDecimal > TimerFinishTime)
            {
                if (_initialSecondsAmount == _secondsToStart)
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