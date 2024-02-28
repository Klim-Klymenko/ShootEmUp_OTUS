﻿using System;
using Atomic.Elements;
using GameCycle;
using Zenject;

namespace GameEngine
{
    [Serializable]
    public sealed class FinishGameComponent
    {
       private FinishGameMechanics _finishGameMechanics;
        
        public void Compose(DiContainer diContainer, IAtomicValue<bool> aliveCondition, IAtomicObservable<int> takeDamageEvent) 
        {
            GameCycleManager gameCycleManager = diContainer.Resolve<GameCycleManager>();
            
            _finishGameMechanics = new FinishGameMechanics(takeDamageEvent, aliveCondition, gameCycleManager);
        }

        public void OnEnable()
        {
            _finishGameMechanics.OnEnable();
        }

        public void OnDisable()
        {
            _finishGameMechanics.OnDisable();
        }
    }
}