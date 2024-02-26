namespace GameCycle
{
    public interface IInitializeGameListener : IGameListener
    {
        void OnInitialize();
    }
}