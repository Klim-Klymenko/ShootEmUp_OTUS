namespace GameCycle
{
    public interface ILateUpdateGameListener : IGameListener
    {
        void OnLateUpdate();
    }
}