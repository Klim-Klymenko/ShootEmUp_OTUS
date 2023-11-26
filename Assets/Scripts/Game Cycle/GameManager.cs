using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameManager : MonoBehaviour, IGameStartable
    {
        private readonly List<IGameUpdateListener> _updateListeners = new List<IGameUpdateListener>();
        private readonly List<IGameFixedUpdateListener> _fixedUpdateListeners = new List<IGameFixedUpdateListener>();
        
        private readonly List<IGameStartListener> _startListeners = new List<IGameStartListener>();
        private readonly List<IGameFinishListener> _finishListeners = new List<IGameFinishListener>();
        private readonly List<IGameResumeListener> _resumeListeners = new List<IGameResumeListener>();
        private readonly List<IGamePauseListener> _pauseListeners = new List<IGamePauseListener>();
        
        public GameState CurrentGameState { get; set; }
        public bool HasGameRun { get; set; }
        public bool HasGameStarted { get; private set; }

        private void Update()
        {
            if (CurrentGameState != GameState.Started && CurrentGameState != GameState.Resumed)
                return;
            
            for (int i = 0; i < _updateListeners.Count; i++)
                _updateListeners[i].OnUpdate();
        }

        private void FixedUpdate()
        {
            if (CurrentGameState != GameState.Started && CurrentGameState != GameState.Resumed)
                return;
            
            for (int i = 0; i < _fixedUpdateListeners.Count; i++)
                _fixedUpdateListeners[i].OnFixedUpdate();
        }
        
        void IGameStartable.OnStart()
        {
            if (CurrentGameState != GameState.Initialized && CurrentGameState != GameState.Resumed)
                return;

            for (int i = 0; i < _startListeners.Count; i++)
                _startListeners[i].OnStart();

            CurrentGameState = GameState.Started;
            HasGameStarted = true;
        }
        
        public void OnFinish()
        {
            if (CurrentGameState == GameState.Finished)
                return;
            
            for (int i = 0; i < _finishListeners.Count; i++)
                _finishListeners[i].OnFinish();

            CurrentGameState = GameState.Finished;
        }
        
        
        public void OnResume()
        {
            if (CurrentGameState != GameState.Paused)
                return;

            for (int i = 0; i < _resumeListeners.Count; i++)
            {
                //если мы поставили на паузу во время обратного отсчета до начала игры
                if (!HasGameStarted)
                    
                    if (_resumeListeners[i] is IGameRunner)
                        _resumeListeners[i].OnResume();
                    
                    else continue;
                
                else _resumeListeners[i].OnResume();
            }

            CurrentGameState = GameState.Resumed;
        }
        
        public void OnPause()
        {
            if (CurrentGameState == GameState.Paused || CurrentGameState == GameState.Finished || !HasGameRun)
                return;
            
            for (int i = 0; i < _pauseListeners.Count; i++)
                _pauseListeners[i].OnPause();

            CurrentGameState = GameState.Paused;
        }

        public void AddUpdateListeners<T>(T listener) where T : IGameListener
        {
            if (listener is IGameUpdateListener updateListener)
                if (!_updateListeners.Contains(updateListener))
                    AddUpdateListener(updateListener);
            
            if (listener is IGameFixedUpdateListener fixedUpdateListener)
                if (!_fixedUpdateListeners.Contains(fixedUpdateListener))
                    AddFixedUpdateListener(fixedUpdateListener);
        }

        public void RemoveUpdateListeners<T>(T listener) where T : IGameListener
        {
            if (listener is IGameUpdateListener updateListener)
                if (_updateListeners.Contains(updateListener))
                    RemoveUpdateListener(updateListener);
            
            if (listener is IGameFixedUpdateListener fixedUpdateListener)
                if (_fixedUpdateListeners.Contains(fixedUpdateListener))
                    RemoveFixedUpdateListener(fixedUpdateListener);
        }

        public void AddEventListeners<T>(T listener) where T : IGameListener
        {
            if (listener is IGameStartListener startListener)
                if (!_startListeners.Contains(startListener))
                    AddStartListener(startListener);
            
            if (listener is IGameFinishListener finishListener)
                if (!_finishListeners.Contains(finishListener))
                    AddFinishListener(finishListener);
            
            if (listener is IGameResumeListener resumeListener)
                if (!_resumeListeners.Contains(resumeListener))
                    AddResumeListener(resumeListener);
            
            if (listener is IGamePauseListener pauseListener)
                if (!_pauseListeners.Contains(pauseListener))
                    AddPauseListener(pauseListener);
        }
        
        private void AddUpdateListener(IGameUpdateListener updateListener) => _updateListeners.Add(updateListener);
        private void AddFixedUpdateListener(IGameFixedUpdateListener fixedUpdateListener) => _fixedUpdateListeners.Add(fixedUpdateListener);
        private void AddStartListener(IGameStartListener startListener) => _startListeners.Add(startListener);
        private void AddFinishListener(IGameFinishListener finishListener) => _finishListeners.Add(finishListener);
        private  void AddResumeListener(IGameResumeListener resumeListener) => _resumeListeners.Add(resumeListener);
        private  void AddPauseListener(IGamePauseListener pauseListener) => _pauseListeners.Add(pauseListener);
        
        private  void RemoveUpdateListener(IGameUpdateListener updateListener) => _updateListeners.Remove(updateListener);
        private  void RemoveFixedUpdateListener(IGameFixedUpdateListener fixedUpdateListener) => _fixedUpdateListeners.Remove(fixedUpdateListener);
    }
}