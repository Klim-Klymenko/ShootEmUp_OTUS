namespace PM
{
    public sealed class PopupPresenter
    {
        public CharacterPresenter CharacterPresenter { get; }

        private readonly UserInfoPresenter _userInfoPresenter;
        private readonly PlayerLevelPresenter _playerLevelPresenter;
        private readonly CharacterInfoPresenter _characterInfoPresenter;

        private PopupView _popupView;
        
        public PopupPresenter(string[] valuesNames, UserInfo userInfo, PlayerLevel playerLevel, CharacterInfo characterInfo)
        {
            CharacterPresenter characterPresenter = new CharacterPresenter(valuesNames, userInfo, playerLevel, characterInfo);
            _userInfoPresenter = new UserInfoPresenter(userInfo);
            _playerLevelPresenter = new PlayerLevelPresenter(playerLevel);
            _characterInfoPresenter = new CharacterInfoPresenter(characterInfo);

            CharacterPresenter = characterPresenter;
        }

        public void Construct(PopupView popupView)
        {
            _userInfoPresenter.Construct(popupView);
            _playerLevelPresenter.Construct(popupView);
            _characterInfoPresenter.Construct(popupView);
        }
    }
}