using UnityEngine;

namespace ShootEmUp
{
    public interface IPoolable
    {
        Transform Transform { get; }
        GameObject GameObject { get; }
    }
}