using System.Threading.Tasks;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameObjectContext : MonoBehaviour
    {
        [SerializeField] private DependencyInstaller[] _installers;
        
        private DependencyAssembler _dependencyAssembler;
        private readonly ServiceLocator _serviceLocator = new();
        private readonly DependencyInjector _dependencyInjector = new();
        
        private Task InitializeServices()
        {
            _dependencyAssembler = new(_serviceLocator, _dependencyInjector);

            _serviceLocator.InstallServices(_dependencyAssembler, _installers);
            return Task.CompletedTask;
        }
        
        private async void OnEnable()
        {
            await InitializeServices();
            
            _dependencyAssembler.InjectRequiredInstancesOnly(_installers);
        }
    }
}