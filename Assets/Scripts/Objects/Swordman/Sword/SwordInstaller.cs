using EcsEngine.Components;
using EcsEngine.Extensions;
using UnityEngine;

namespace Objects.Swordsman
{
    public sealed class SwordInstaller : EntityInstaller
    {
        [SerializeField] 
        private Timer _timer;
        
        [SerializeField]
        private Damage _damage;
        
        public override void Install(Entity entity)
        {
            entity
                .AddComponent(_timer)
                .AddComponent(_damage);
        }
    }
}