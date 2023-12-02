namespace ShootEmUp
{
    public interface IGameFinishListener : IGameListener
    {
        void OnFinish();
    }
}