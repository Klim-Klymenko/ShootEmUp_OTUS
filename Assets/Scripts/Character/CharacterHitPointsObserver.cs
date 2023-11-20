using UnityEngine;

namespace ShootEmUp
{
    public class CharacterHitPointsObserver : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;

        private HitPointsComponent _hitPointsComponent;

        private void Awake() => _hitPointsComponent = GetComponent<HitPointsComponent>();

        private void OnEnable() => _hitPointsComponent.OnDeath += CharacterDeath;

        private void OnDisable() =>_hitPointsComponent.OnDeath -= CharacterDeath;

        private void CharacterDeath() => _gameManager.FinishGame();
    }  
}