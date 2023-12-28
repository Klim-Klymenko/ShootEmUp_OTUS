namespace SaveSystem
{
    public interface ISaveLoader
    {
        void Save(IGameRepository repository);
        void Load(IGameRepository repository);
    }
}