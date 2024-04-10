using Leopotam.EcsLite;
using UnityEngine;

namespace EcsEngine.Extensions
{
    public abstract class EntityInstaller : MonoBehaviour
    {
        public abstract void Install(Entity entity, EcsWorld world);
        public virtual void Uninstall(Entity entity, EcsWorld world) { }
    }
}