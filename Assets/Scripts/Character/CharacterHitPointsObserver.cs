namespace ShootEmUp
{
    public sealed class CharacterHitPointsObserver : IGameStartListener,
        IGameFinishListener, IGameResumeListener, IGamePauseListener
    {
        private HitPointsComponent _hitPointsComponent;
        private GameManager _gameManager;

        [Inject]
        public void Construct(GameManager gameManager, CharacterService characterService)
        {
            _gameManager = gameManager;
            _hitPointsComponent = characterService.HitPointsComponent;
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