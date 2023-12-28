using GameEngine;
using UnityEngine;

namespace SaveSystem
{
    public interface IUnitSpawner
    {
        Unit SpawnUnit(Unit prefab, Vector3 position, Quaternion rotation);
    }
}