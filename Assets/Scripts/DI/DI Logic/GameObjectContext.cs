using System.Threading.Tasks;

namespace ShootEmUp
{
    internal sealed class GameObjectContext : Context
    {
        private readonly ServiceLocator _serviceLocator = new();
        
        private Task InitializeServices()
        {
            InitializeDi(_serviceLocator);
            InstallServices();
            return Task.CompletedTask;
        }
        
        private async void Awake()
        {
            await InitializeServices();

            InjectGameObjectContext();
        }
    }
}