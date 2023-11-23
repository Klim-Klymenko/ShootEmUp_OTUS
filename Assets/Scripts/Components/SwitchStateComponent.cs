using UnityEngine;

namespace ShootEmUp
{
    public sealed class SwitchStateComponent : MonoBehaviour
    {
        public GameManager GameManager;

        public void TurnOn<T>(T listener) where T : ISwitchable
        {
            if (!(listener is MonoBehaviour switchableScript))
                return;
            
            if (listener.IsOnlyUnityMethods)
                switchableScript.enabled = true;
            else
                if (listener is IGameListener gameListener)
                    GameManager.AddUpdateListeners(gameListener);
        }

        public void TurnOff<T>(T listener) where T : ISwitchable
        {
            if (!(listener is MonoBehaviour switchableScript))
                return;
            
            if (listener.IsOnlyUnityMethods)
                switchableScript.enabled = false;
            else
                if (listener is IGameListener gameListener)
                    GameManager.RemoveUpdateListeners(gameListener);
        }
    }
}
