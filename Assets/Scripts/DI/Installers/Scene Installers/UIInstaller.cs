using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public sealed class UIInstaller : Installer
    {
        [SerializeField, Listener, Service]
        private GameStarterController _gameStarterController;
        
        [SerializeField, Listener]
        private StartFinishButtonsAdapter _startFinishButtonsAdapter;

        [Service]
        private GameResumePauseDecorator _gameResumePauseDecorator = new();
        
        [SerializeField, Listener]
        private ResumePauseButtonsController _resumePauseButtonsController;
    }
}