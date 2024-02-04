using UnityEngine;
using UnityEngine.UI;

namespace PM
{
    [RequireComponent(typeof(UserInfoView))]
    [RequireComponent(typeof(PlayerLevelView))]
    [RequireComponent(typeof(CharacterStatsView))]
    public sealed class CharacterView : MonoBehaviour
    {
        [SerializeField]
        private Button _closeButton;

        [SerializeField]
        private UserInfoView _userInfoView;
        
        [SerializeField]
        private PlayerLevelView _playerLevelView;
        
        [SerializeField]
        private CharacterStatsView _characterStatsView;

        private void OnValidate()
        {
            _userInfoView = GetComponent<UserInfoView>();
            _playerLevelView = GetComponent<PlayerLevelView>();
            _characterStatsView = GetComponent<CharacterStatsView>();
        }

        internal void Show()
        {
            gameObject.SetActive(true);
            
            _userInfoView.Show();
            _playerLevelView.Show();
            _characterStatsView.Show();
            
            _closeButton.onClick.AddListener(Hide);
        }
        
        internal void Hide()
        {
            gameObject.SetActive(false);
            
            _userInfoView.Hide();
            _playerLevelView.Hide();
            _characterStatsView.Hide();
            
            _closeButton.onClick.RemoveListener(Hide);
        }
    }
}