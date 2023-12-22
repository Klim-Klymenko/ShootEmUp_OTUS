using UnityEngine;

namespace ShootEmUp
{
    public sealed class UIInstaller : DependencyInstaller
    {
        [SerializeField, Listener, Service]
        private GameStarterController _gameStarterController;
        
        [SerializeField, Listener]
        private StartButtonManager startButtonManager;

        [Service]
        private GameResumePauseDecorator _gameResumePauseDecorator = new();
        
        [SerializeField, Listener]
        private ResumePauseButtonsController _resumePauseButtonsController;
    }
}