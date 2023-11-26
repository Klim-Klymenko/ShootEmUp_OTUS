using UnityEngine;

namespace ShootEmUp
{
    public sealed class StartButtonManager : MonoBehaviour
    {
        [SerializeField] private GameObject _startButton;

        public void DisableStartButton() => _startButton.SetActive(false);
    }
}