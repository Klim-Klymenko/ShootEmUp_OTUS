namespace ShootEmUp
{
    public interface IGameFinishListener : IGameListener, ISwitchable
    {
        void OnFinish();
    }
}