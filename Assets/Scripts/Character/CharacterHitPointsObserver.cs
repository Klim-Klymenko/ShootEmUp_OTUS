namespace ShootEmUp
{
    public sealed class CharacterHitPointsObserver : IGameStartListener,
        IGameFinishListener, IGameResumeListener, IGamePauseListener
    {
        private HitPointsComponent _hitPointsComponent;
        private GameManager _gameManager;

        [Inject]
        private void Construct(GameManager gameManager, HitPointsComponent hitPointsComponent)
        {
            _gameManager = gameManager;
            _hitPointsComponent = hitPointsComponent;
        }
        
        private void Enable() => _hitPointsComponent.OnDeath += CharacterDeath;

        private void Disable() => _hitPointsComponent.OnDeath -= CharacterDeath;

        private void CharacterDeath() => _gameManager.OnFinish();
        
        void IGameStartListener.OnStart() => Enable();

        void IGameFinishListener.OnFinish() => Disable();

        void IGameResumeListener.OnResume() => Enable();
        
        void IGamePauseListener.OnPause() => Disable();
    }  
}