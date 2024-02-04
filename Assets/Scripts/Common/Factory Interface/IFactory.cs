namespace Common
{
    public interface IFactory<out TPrefab> where TPrefab : class
    {
        TPrefab Create();
    }
}