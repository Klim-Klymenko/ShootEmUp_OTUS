using UnityEngine;

namespace ShootEmUp
{ 
    public sealed class CharacterBulletManager : MonoBehaviour
    {
        [SerializeField] private CharacterBulletBuilder _bulletBuilder;

        private void OnValidate() => _bulletBuilder = GetComponent<CharacterBulletBuilder>();

        public void RunBullet() => _bulletBuilder.SpawnBullet();
    }
}