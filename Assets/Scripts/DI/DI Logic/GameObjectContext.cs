using System.Threading.Tasks;

namespace ShootEmUp
{
    public sealed class GameObjectContext : GameContext
    {
        private readonly ServiceLocator _serviceLocator = new();
        
        private Task InitializeServices()
        {
            InitializeDi(_serviceLocator);
            InstallServices();
            return Task.CompletedTask;
        }
        
        public async void Awake()
        {
            await InitializeServices();

            InjectGameObjectContext();
        }
    }
}