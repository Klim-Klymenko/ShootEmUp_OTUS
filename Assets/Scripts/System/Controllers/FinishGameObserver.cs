using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using GameCycle;
using GameEngine;
using JetBrains.Annotations;

namespace System
{
    [UsedImplicitly]
    internal sealed class FinishGameObserver : IStartGameListener, IFinishGameListener
    {
        private IAtomicObservable _deathObservable;
        
        private readonly IAtomicObject _character;
        private readonly GameCycleManager _gameCycleManager;
            
        internal FinishGameObserver(IAtomicObject character, GameCycleManager gameCycleManager)
        {
            _character = character;
            _gameCycleManager = gameCycleManager;
        }

        void IStartGameListener.OnStart()
        {
            _deathObservable = _character.GetObservable(LiveableAPI.DeathObservable);
            
            _deathObservable.Subscribe(FinishGame);
        }

        void IFinishGameListener.OnFinish()
        {
            _deathObservable.Unsubscribe(FinishGame);
        }
        
        private void FinishGame()
        {
            _gameCycleManager.OnDestroy();
        }
    }
}