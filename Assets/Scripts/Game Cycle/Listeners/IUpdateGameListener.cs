namespace GameCycle
{
    public interface IUpdateGameListener : IGameListener
    {
        void OnUpdate();
    }
}