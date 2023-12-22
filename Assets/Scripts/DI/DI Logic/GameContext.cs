using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameContext : MonoBehaviour
    {
        [SerializeField] private DependencyInstaller[] _installers;
        
        private DependencyAssembler _dependencyAssembler;
        private readonly ServiceLocator _serviceLocator = new();
        private readonly DependencyInjector _dependencyInjector = new();
        private readonly GameManager _gameManager = new();
        
        private void Awake()
        {
            _dependencyAssembler = new(_serviceLocator, _dependencyInjector);

            SystemInstallablesArgs args = new SystemInstallablesArgs
            {
                DependencyAssembler = _dependencyAssembler, GameManager = _gameManager
            };
            _serviceLocator.InstallServices(args, _installers);
            
            _gameManager.InstallListeners(_installers);
            _gameManager.OnInitialize();
        }

        private void Start()
        {
            _dependencyAssembler.Inject(_installers);
        }

        private void Update() => _gameManager.OnUpdate();

        private void FixedUpdate() => _gameManager.OnFixedUpdate();
        
        public void OnFinish() => _gameManager.OnFinish();
    }
}