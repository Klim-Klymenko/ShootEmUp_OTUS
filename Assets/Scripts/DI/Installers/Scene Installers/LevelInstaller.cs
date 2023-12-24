using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelInstaller : DependencyInstaller
    {
        [SerializeField, Listener]
        private LevelBackground _levelBackground;
        
        [SerializeField, Service]
        private LevelBounds _levelBounds;
    }
}