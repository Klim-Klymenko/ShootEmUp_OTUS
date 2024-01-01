using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public sealed class UIInstaller : Installer
    {
        [SerializeField, Listener, Service]
        private GameStarterController _gameStarterController;
        
        [SerializeField, Listener]
        private StartFinishButtonsController _startFinishButtonsController;

        [Service]
        private GameResumePauseDecorator _gameResumePauseDecorator = new();
        
        [SerializeField, Listener]
        private ResumePauseButtonsController _resumePauseButtonsController;
    }
}