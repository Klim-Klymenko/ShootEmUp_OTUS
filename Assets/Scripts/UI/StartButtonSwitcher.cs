using UnityEngine;

namespace ShootEmUp
{
    public sealed class StartButtonSwitcher : MonoBehaviour
    {
        [SerializeField] private GameObject _startButton;

        private void OnValidate() => _startButton = gameObject;

        public void DisableStartButton() => _startButton.SetActive(false);
    }
}