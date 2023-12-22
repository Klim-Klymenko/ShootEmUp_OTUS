namespace ShootEmUp
{
    public interface IGameResumeListener : IGameListener
    {
        void OnResume();
    }
}