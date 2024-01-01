using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterInstaller : Installer
    {
        [SerializeField, Service]
        private CharacterService _characterService;
        
        [SerializeField, Service]
        private CharacterBulletShooter _characterBulletShooter;
        
        [Listener]
        private CharacterHitPointsObserver _characterHitPointsObserver = new();
    }
}