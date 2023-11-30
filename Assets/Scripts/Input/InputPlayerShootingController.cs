using UnityEngine;

namespace ShootEmUp
{
    public class InputPlayerShootingController : MonoBehaviour, IGameUpdateListener, 
        IGameStartListener, IGameFinishListener, IGameResumeListener, IGamePauseListener
    {
        [SerializeField] private CharacterBulletShooter _bulletShooter;
        
        [SerializeField] private SwitchStateComponent _switchComponent;

        public bool IsOnlyUnityMethods { get; } = false;

        private void OnValidate() => _switchComponent = GetComponent<SwitchStateComponent>();

        void IGameUpdateListener.OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _bulletShooter.ShootBullet();
        }

        public void OnPause()
        {
            _switchComponent.TurnOff(this);
        }

        public void OnResume()
        {
            _switchComponent.TurnOn(this);
        }

        public void OnFinish()
        {
            _switchComponent.TurnOff(this);
        }

        public void OnStart()
        {
            _switchComponent.TurnOn(this);
        }
    }
}