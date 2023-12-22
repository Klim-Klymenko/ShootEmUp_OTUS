namespace ShootEmUp
{
    public interface IGameInitializeListener : IGameListener
    {
        void OnInitialize();
    }
}