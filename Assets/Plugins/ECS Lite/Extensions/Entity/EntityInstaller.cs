using UnityEngine;

namespace EcsEngine.Extensions
{
    public abstract class EntityInstaller : MonoBehaviour
    {
        public abstract void Install(Entity entity);
        public virtual void Uninstall(Entity entity) { }
    }
}