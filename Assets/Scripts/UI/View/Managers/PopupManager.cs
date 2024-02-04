using Common;
using JetBrains.Annotations;
    
namespace PM
{
    [UsedImplicitly]
    public sealed class PopupManager
    {
        private CharacterView _characterView;

        private readonly ISpawner<CharacterView> _viewSpawner;
        
        public PopupManager(ISpawner<CharacterView> viewSpawner)
        {
            _viewSpawner = viewSpawner;
        }

        public void Show()
        {
            _characterView = _viewSpawner.Spawn();
            _characterView.Show();
        }
        
        public void Hide()
        {
            _characterView.Hide();
            _viewSpawner.Despawn(_characterView);
        }
    }
}