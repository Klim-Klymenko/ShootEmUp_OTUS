using System.Collections.Generic;

namespace PM
{
    public interface IMenuPresenter
    {
        IReadOnlyList<CharacterPresenter> GetCharacterPresenters();
        IReadOnlyList<PopupPresenter> PopupPresenters { get; }
        
        void DestroyPresenter(PopupPresenter popupPresenter);
    }
}