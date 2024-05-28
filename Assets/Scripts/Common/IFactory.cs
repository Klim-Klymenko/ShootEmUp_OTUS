namespace Common
{
    public interface IFactory<out T> where T : class
    {
        T Create();
    }
}