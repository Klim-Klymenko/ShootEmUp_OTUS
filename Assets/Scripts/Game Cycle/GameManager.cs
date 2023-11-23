using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameManager : MonoBehaviour
    {
        private readonly List<IGameUpdateListener> _updateListeners = new List<IGameUpdateListener>();
        private readonly List<IGameFixedUpdateListener> _fixedUpdateListeners = new List<IGameFixedUpdateListener>();
        
        private readonly List<IGameStartListener> _startListeners = new List<IGameStartListener>();
        private readonly List<IGameFinishListener> _finishListeners = new List<IGameFinishListener>();
        private readonly List<IGameResumeListener> _resumeListeners = new List<IGameResumeListener>();
        private readonly List<IGamePauseListener> _pauseListeners = new List<IGamePauseListener>();
        
        public void FinishGame()
        {
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }

        private void Update()
        {
            for (int i = 0; i < _updateListeners.Count; i++)
                _updateListeners[i].OnUpdate();
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < _fixedUpdateListeners.Count; i++)
                _fixedUpdateListeners[i].OnFixedUpdate();
        }

        [ContextMenu("OnStart")]
        private void OnStart()
        {
            for (int i = 0; i < _startListeners.Count; i++)
                _startListeners[i].OnStart();
        }
        
        [ContextMenu("OnFinish")]
        private void OnFinish()
        {
            for (int i = 0; i < _finishListeners.Count; i++)
                _finishListeners[i].OnFinish();
        }
        
        [ContextMenu("OnResume")]
        private void OnResume()
        {
            for (int i = 0; i < _resumeListeners.Count; i++)
                _resumeListeners[i].OnResume();
        }
        
        [ContextMenu("OnPause")]
        private void OnPause()
        {
            for (int i = 0; i < _pauseListeners.Count; i++)
                _pauseListeners[i].OnPause();
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