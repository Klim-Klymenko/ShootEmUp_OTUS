using UnityEngine;

namespace EcsEngine
{
    internal abstract class EntityInstaller : MonoBehaviour
    {
        public abstract void Install(Entity entity);
        public abstract void Uninstall(Entity entity);
    }
}