namespace ShootEmUp
{
    public interface IGameStartable
    {
        bool HasGameRun { get; set; }
        void OnStart();
    }
}