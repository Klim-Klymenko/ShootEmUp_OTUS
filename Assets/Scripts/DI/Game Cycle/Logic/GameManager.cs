using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameManager
    {
        public GameState CurrentGameState { get; private set; }
        public bool HasGameRun { get; set; }
        public bool HasGameStarted { get; private set; }
        
        private readonly List<IGameUpdateListener> _updateListeners = new();
        private readonly List<IGameFixedUpdateListener> _fixedUpdateListeners = new();
        
        private readonly List<IGameInitializeListener> _initializeListeners = new();
        private readonly List<IGameStartListener> _startListeners = new();
        private readonly List<IGameFinishListener> _finishListeners = new();
        private readonly List<IGameResumeListener> _resumeListeners = new();
        private readonly List<IGamePauseListener> _pauseListeners = new();
        
        public void OnUpdate()
        {
            if (!HasGameStarted) return;

            if (CurrentGameState != GameState.Playing)
                return;
            
            for (int i = 0; i < _updateListeners.Count; i++)
                _updateListeners[i].OnUpdate();
        }

        public void OnFixedUpdate()
        {
            if (!HasGameStarted) return;
            
            if (CurrentGameState != GameState.Playing) return;
            
            for (int i = 0; i < _fixedUpdateListeners.Count; i++)
                _fixedUpdateListeners[i].OnFixedUpdate();
        }

        public void OnInitialize()
        {
            if (CurrentGameState != GameState.None) return;
            
            for (int i = 0; i < _initializeListeners.Count; i++)
                _initializeListeners[i].OnInitialize();

            CurrentGameState = GameState.Initialized;
        }
        
        public void OnStart()
        {
            if (CurrentGameState != GameState.Initialized) return;

            for (int i = 0; i < _startListeners.Count; i++)
                _startListeners[i].OnStart();

            CurrentGameState = GameState.Playing;
            HasGameStarted = true;
        }
        
        public void OnFinish()
        {
            if (CurrentGameState == GameState.Finished) return;
            
            for (int i = 0; i < _finishListeners.Count; i++)
                _finishListeners[i].OnFinish();

            CurrentGameState = GameState.Finished;
        }
        
        public void OnResume()
        {
            if (CurrentGameState != GameState.Paused) return;
            
            for (int i = 0; i < _resumeListeners.Count; i++) 
                _resumeListeners[i].OnResume();

            CurrentGameState = GameState.Playing;
        }
        
        public void OnPause()
        {
            if (CurrentGameState is GameState.Paused or GameState.Finished) return;
            
            for (int i = 0; i < _pauseListeners.Count; i++) 
                _pauseListeners[i].OnPause();
            
            CurrentGameState = GameState.Paused;
        }

        public void AddGameListeners(IEnumerable<IGameListener> listeners)
        {
            foreach (var listener in listeners) 
                AddGameListener(listener);
        }

        public void AddGameListener(IGameListener listener)
        {
            if (listener is IGameUpdateListener updateListener)
                if (!_updateListeners.Contains(updateListener))
                    AddUpdateListener(updateListener);
            
            if (listener is IGameFixedUpdateListener fixedUpdateListener)
                if (!_fixedUpdateListeners.Contains(fixedUpdateListener))
                    AddFixedUpdateListener(fixedUpdateListener);
            
            if (listener is IGameInitializeListener initializeListener)
                if (!_initializeListeners.Contains(initializeListener))
                    AddInitializeListener(initializeListener);

            if (listener is IGameStartListener startListener)
            {
                if (!_startListeners.Contains(startListener))
                {
                    AddStartListener(startListener);
                    
                    if (HasGameStarted)
                        startListener.OnStart();
                }
            }
            
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
        private void AddInitializeListener(IGameInitializeListener initializeListener) => _initializeListeners.Add(initializeListener);
        private void AddStartListener(IGameStartListener startListener) => _startListeners.Add(startListener);
        private void AddFinishListener(IGameFinishListener finishListener) => _finishListeners.Add(finishListener);
        private  void AddResumeListener(IGameResumeListener resumeListener) => _resumeListeners.Add(resumeListener);
        private  void AddPauseListener(IGamePauseListener pauseListener) => _pauseListeners.Add(pauseListener);
    }
}