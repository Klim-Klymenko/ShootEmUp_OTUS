using UnityEngine;

namespace GameEngine
{
    public interface IUnitSpawner
    {
        Unit SpawnUnit(Unit prefab, Vector3 position, Quaternion rotation);
    }
}