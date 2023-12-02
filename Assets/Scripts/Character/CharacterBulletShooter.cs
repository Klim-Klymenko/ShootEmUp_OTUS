using System;
using ShootEmUp.Interfaces;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(WeaponComponent))]
    public sealed class CharacterBulletShooter : MonoBehaviour
    {
        [SerializeField] private Color _bulletColour;
        [SerializeField] private int _bulletDamage;
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private WeaponComponent _weaponComponent;

        private IBulletSpawner _bulletSpawner;
        [SerializeField] private BulletManager _bulletManager;

        private void OnValidate() => _weaponComponent = GetComponent<WeaponComponent>();

        //косяк с таким кривым прокидыванием будет исправлен в домашке с DI
        private void Awake() => _bulletSpawner = _bulletManager;

        public void ShootBullet()
        {
            _bulletSpawner.SpawnBullet(new Args 
            {
                CohesionType = CohesionType.Player, 
                PhysicsLayer = (int) PhysicsLayer.PLAYER_BULLET, 
                Color = _bulletColour,
                Damage = _bulletDamage, 
                Position = _weaponComponent.Position, 
                Velocity = _weaponComponent.Rotation * Vector3.up * _bulletSpeed
            });
        }
    }
}