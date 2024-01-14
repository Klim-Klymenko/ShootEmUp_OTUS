namespace PM
{
    public sealed class PopupPresenter
    {
        public CharacterPresenter CharacterPresenter { get; }

        private readonly UserInfoPresenter _userInfoPresenter;
        private readonly PlayerLevelPresenter _playerLevelPresenter;
        private readonly CharacterInfoPresenter _characterInfoPresenter;

        public PopupPresenter(string[] valuesNames, UserInfo userInfo, PlayerLevel playerLevel, CharacterInfo characterInfo, PopupView popupView)
        {
            _userInfoPresenter = new UserInfoPresenter(userInfo, popupView);
            _playerLevelPresenter = new PlayerLevelPresenter(playerLevel, popupView);
            _characterInfoPresenter = new CharacterInfoPresenter(characterInfo, popupView);
            CharacterPresenter characterPresenter = new CharacterPresenter(valuesNames, userInfo, playerLevel, characterInfo);

            CharacterPresenter = characterPresenter;
        }
        
        public void DestroyPresenters()
        {
            _userInfoPresenter.Dispose();
            _playerLevelPresenter.Dispose();
            _characterInfoPresenter.Dispose();
        }
    }
}