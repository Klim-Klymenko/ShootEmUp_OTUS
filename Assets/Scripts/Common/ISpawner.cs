using UnityEngine;

namespace Common
{
    public interface ISpawner<T> where T : Object
    {
        T Spawn();
        void Despawn(T obj);
    }
}