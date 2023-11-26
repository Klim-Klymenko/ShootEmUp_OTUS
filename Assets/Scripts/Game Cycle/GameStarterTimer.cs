using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameStarterTimer : MonoBehaviour
    {
        public event Action OnGameStarted;
        //public event Action OnSecondChanged;
        
        [SerializeField] private int _initialSecondsAmount = 3;
        private int _secondsToStart;
        private float _secondsToStartDecimal;
        private const int TimerFinishTime = 0;

        private void Awake() => _secondsToStartDecimal = _secondsToStart = _initialSecondsAmount;

        //можно разделить логику отсчета и отображения секунд, но для простоты это находится в таймере (KISS). Надеюсь, не критично
        public void TimerCountdown(bool isRun)
        {
            if (!isRun)
                return;

            if (_secondsToStartDecimal > TimerFinishTime)
            {
                if (_initialSecondsAmount == _secondsToStart)
                    Debug.Log(_secondsToStart);
                    //OnSecondChanged?.Invoke(); - в случае сторогого SRP я бы вызывал данное событие

                _secondsToStartDecimal -= Time.deltaTime;

                int ceiledSecondsToStart = Mathf.CeilToInt(_secondsToStartDecimal);
                if (ceiledSecondsToStart < _secondsToStart)
                {
                    _secondsToStart = ceiledSecondsToStart;
                    Debug.Log(_secondsToStart);
                    //OnSecondChanged?.Invoke(); - в случае сторогого SRP я бы вызывал данное событие
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