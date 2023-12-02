namespace ShootEmUp
{
    public interface IGamePauseListener : IGameListener
    {
        void OnPause();
    }
}