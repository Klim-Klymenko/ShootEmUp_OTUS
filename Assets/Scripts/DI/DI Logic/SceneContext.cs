namespace ShootEmUp
{
    internal sealed class SceneContext : Context
    {
        private GameManagerInstaller _gameManagerInstaller;
        private readonly GameManager _gameManager = new();
        
        private void Awake()
        {
            InitializeDi();
            
            _gameManagerInstaller = new GameManagerInstaller(_gameManager);
            
            SystemInstallableArgs args = new SystemInstallableArgs
            {
                DiContainer = DiContainer, GameManager = _gameManager
            };
            InstallServices(args);
            InjectSceneContext();
            
            _gameManagerInstaller.InstallListeners(Installers);
            _gameManager.OnInitialize(); 
        }
        
        private void Update() => _gameManager.OnUpdate();

        private void FixedUpdate() => _gameManager.OnFixedUpdate();
    }
}