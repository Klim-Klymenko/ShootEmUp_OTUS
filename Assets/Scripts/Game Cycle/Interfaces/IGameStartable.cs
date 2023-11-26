namespace ShootEmUp
{
    public interface IGameStartable
    {
        GameState CurrentGameState { get; }
        bool HasGameRun { get; set; }
        bool HasGameStarted { get; }
        void OnStart();
    }
}