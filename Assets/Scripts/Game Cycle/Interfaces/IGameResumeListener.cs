namespace ShootEmUp
{
    public interface IGameResumeListener : IGameListener, ISwitchable
    {
        void OnResume();
    }
}