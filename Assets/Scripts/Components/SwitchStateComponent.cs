using UnityEngine;

namespace ShootEmUp
{
    public sealed class SwitchStateComponent : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;

        public GameManager GameManager
        {
            set => _gameManager = value;
        }

        public void TurnOn<T>(T listener) where T : ISwitchable
        {
            if (!(listener is MonoBehaviour switchableScript))
                return;
            
            if (listener.IsOnlyUnityMethods)
                EnableUnityScript(switchableScript);
            else if (listener is IGameListener gameListener)
            {
                _gameManager.AddUpdateListeners(gameListener);
                EnableUnityScript(switchableScript);
            }
        }

        public void TurnOff<T>(T listener) where T : ISwitchable
        {
            if (!(listener is MonoBehaviour switchableScript))
                return;
            
            if (listener.IsOnlyUnityMethods)
                DisableUnityScript(switchableScript);
            else if (listener is IGameListener gameListener)
            {
                _gameManager.RemoveUpdateListeners(gameListener);
                DisableUnityScript(switchableScript);
            }
        }

        private void EnableUnityScript(MonoBehaviour script) => script.enabled = true;
        private void DisableUnityScript(MonoBehaviour script) => script.enabled = false;
    }
}
