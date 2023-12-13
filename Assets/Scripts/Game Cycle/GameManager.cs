using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameManager : MonoBehaviour, IGameStartable
    {
        public GameState CurrentGameState { get; private set; }
        public bool HasGameRun { get; set; }
        private bool HasGameStarted { get; set; }
        
        private readonly List<IGameUpdateListener> _updateListeners = new();
        private readonly List<IGameFixedUpdateListener> _fixedUpdateListeners = new();
        
        private readonly List<IGameInitializeListener> _initializeListeners = new();
        private readonly List<IGameStartListener> _startListeners = new();
        private readonly List<IGameFinishListener> _finishListeners = new();
        private readonly List<IGameResumeListener> _resumeListeners = new();
        private readonly List<IGamePauseListener> _pauseListeners = new();

        private readonly GameStateController _gameStateController = new();
        
        private void Awake() => OnInitialize();

        private void Update()
        {
            if (!HasGameStarted) return;

            if (CurrentGameState != GameState.Playing)
                return;
            
            for (int i = 0; i < _updateListeners.Count; i++)
                _updateListeners[i].OnUpdate();
        }

        private void FixedUpdate()
        {
            if (!HasGameStarted) return;
            
            if (CurrentGameState != GameState.Playing)
                return;
            
            for (int i = 0; i < _fixedUpdateListeners.Count; i++)
                _fixedUpdateListeners[i].OnFixedUpdate();
        }

        private void OnInitialize()
        {
            if (CurrentGameState != GameState.None)
                return;
            
            for (int i = 0; i < _initializeListeners.Count; i++)
                _initializeListeners[i].OnInitialize();

            CurrentGameState = GameState.Initialized;
        }
        
        void IGameStartable.OnStart()
        {
            if (CurrentGameState != GameState.Initialized)
                return;

            for (int i = 0; i < _startListeners.Count; i++)
                _startListeners[i].OnStart();

            CurrentGameState = GameState.Playing;
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
            if (!HasGameRun) return;
            
            if (CurrentGameState != GameState.Paused && CurrentGameState != GameState.Initialized)
                return;

            List<IGameResumeListener> checkedResumeListeners = _gameStateController.ChangeState(_resumeListeners, HasGameStarted);

            for (int i = 0; i < checkedResumeListeners.Count; i++) 
                checkedResumeListeners[i].OnResume();

            if (HasGameStarted)
                CurrentGameState = GameState.Playing;
        }
        
        public void OnPause()
        {
            if (CurrentGameState == GameState.Paused || CurrentGameState == GameState.Finished || !HasGameRun)
                return;
            
            List<IGamePauseListener> checkedPauseListeners = _gameStateController.ChangeState(_pauseListeners, HasGameStarted);

            for (int i = 0; i < checkedPauseListeners.Count; i++) 
                checkedPauseListeners[i].OnPause();
            
            
            if (HasGameStarted)
                CurrentGameState = GameState.Paused;
        }

        public void AddListeners(IGameListener listener)
        {
            AddUpdateListeners(listener);
            AddEventListeners(listener);
        }
        
        private void AddUpdateListeners(IGameListener listener)
        {
            if (listener is IGameUpdateListener updateListener)
                if (!_updateListeners.Contains(updateListener))
                    AddUpdateListener(updateListener);
            
            if (listener is IGameFixedUpdateListener fixedUpdateListener)
                if (!_fixedUpdateListeners.Contains(fixedUpdateListener))
                    AddFixedUpdateListener(fixedUpdateListener);
        }

        private void AddEventListeners(IGameListener listener)
        {
            if (listener is IGameInitializeListener initializeListener)
                if (!_initializeListeners.Contains(initializeListener))
                    AddInitializeListener(initializeListener);
            
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
        private void AddInitializeListener(IGameInitializeListener initializeListener) => _initializeListeners.Add(initializeListener);
        private void AddStartListener(IGameStartListener startListener) => _startListeners.Add(startListener);
        private void AddFinishListener(IGameFinishListener finishListener) => _finishListeners.Add(finishListener);
        private  void AddResumeListener(IGameResumeListener resumeListener) => _resumeListeners.Add(resumeListener);
        private  void AddPauseListener(IGamePauseListener pauseListener) => _pauseListeners.Add(pauseListener);
    }
}