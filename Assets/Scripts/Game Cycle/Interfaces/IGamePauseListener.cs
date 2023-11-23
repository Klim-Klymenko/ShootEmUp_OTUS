namespace ShootEmUp
{
    public interface IGamePauseListener : IGameListener, ISwitchable
    {
        void OnPause();
    }
}