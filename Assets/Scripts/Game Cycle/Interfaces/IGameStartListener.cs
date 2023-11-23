namespace ShootEmUp
{
    public interface IGameStartListener : IGameListener, ISwitchable
    {
        void OnStart();
    }
}