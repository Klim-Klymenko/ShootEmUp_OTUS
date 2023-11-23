namespace ShootEmUp
{
    public interface IGameFixedUpdateListener : IGameListener
    {
        void OnFixedUpdate();
    }

}