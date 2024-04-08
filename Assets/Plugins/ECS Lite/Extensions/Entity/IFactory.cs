namespace Plugins.ECSLite.Extensions.Entity
{
    public interface IFactory<out T>
    {
        T Create();
    }
}