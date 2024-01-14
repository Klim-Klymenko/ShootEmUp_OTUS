namespace ShootEmUp
{
    public sealed class InputInstaller : Installer
    {
        [Listener]
        private InputMoveController _inputMoveController = new();
    
        [Listener]
        private InputShootController _inputShootController = new();
    }
}