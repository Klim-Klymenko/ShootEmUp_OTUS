namespace Common
{
    public interface ISpawner<TPrefab> where TPrefab : class
    {
        TPrefab Spawn();
        void Despawn(TPrefab prefab);
    }
}