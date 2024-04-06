﻿using System.Collections.Generic;
using UnityEngine;

namespace GameCycle
{
    public sealed class GameCycleManager : MonoBehaviour
    {
        public GameState GameState { get; private set; }
        
        private readonly List<IInitializeGameListener> _initializeListeners = new();
        private readonly List<IStartGameListener> _startListeners = new();
        private readonly List<IUpdateGameListener> _updateListeners = new();
        private readonly List<IFinishGameListener> _finishListeners = new();

        private void Awake()
        {
            if (GameState != GameState.None) return;
            
            for (int i = 0; i < _initializeListeners.Count; i++)
                _initializeListeners[i].OnInitialize();
            
            GameState = GameState.Initialized;
        }

        private void Start()
        {
            if (GameState != GameState.Initialized) return;
            
            for (int i = 0; i < _startListeners.Count; i++)
                _startListeners[i].OnStart();
            
            GameState = GameState.Active;
        }
        
        private void Update()
        {
            if (GameState != GameState.Active) return;
            
            for (int i = 0; i < _updateListeners.Count; i++)
                _updateListeners[i].OnUpdate();
        }

        public void OnDestroy()
        {
            if (GameState == GameState.Finished) return;

            for (int i = 0; i < _finishListeners.Count; i++)
                _finishListeners[i].OnFinish();
            
            GameState = GameState.Finished;
        }
        
        public void AddListener(IGameListener listener)
        {
            if (listener is IInitializeGameListener initializeListener)
            {
                if (!_initializeListeners.Contains(initializeListener))
                {
                    _initializeListeners.Add(initializeListener);
                    
                    if (GameState is GameState.Initialized or GameState.Active)
                        initializeListener.OnInitialize();
                }
            }
            
            if (listener is IStartGameListener startListener)
            {
                if (!_startListeners.Contains(startListener))
                {
                    _startListeners.Add(startListener);
                    
                    if (GameState == GameState.Active)
                        startListener.OnStart();
                }
            }

            if (listener is IUpdateGameListener updateListener)
            {
                if (!_updateListeners.Contains(updateListener))
                    _updateListeners.Add(updateListener); 
            }

            if (listener is IFinishGameListener finishListener)
            {
                if (!_finishListeners.Contains(finishListener))
                    _finishListeners.Add(finishListener); 
            }
        }

        public void RemoveListener(IGameListener listener)
        {
            if (listener is IInitializeGameListener initializeListener)
                if (_initializeListeners.Contains(initializeListener))
                    _initializeListeners.Remove(initializeListener);
            
            if (listener is IStartGameListener startListener)
                if (_startListeners.Contains(startListener))
                    _startListeners.Remove(startListener);
            
            if (listener is IUpdateGameListener updateListener)
                if (_updateListeners.Contains(updateListener))
                    _updateListeners.Remove(updateListener);
            
            if (listener is IFinishGameListener finishListener)
                if (_finishListeners.Contains(finishListener))
                    _finishListeners.Remove(finishListener);
        }
    }
}