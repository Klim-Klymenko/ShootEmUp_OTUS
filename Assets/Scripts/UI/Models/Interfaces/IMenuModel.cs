using System.Collections.Generic;

namespace PM
{
    public interface IMenuModel
    {
        IReadOnlyList<PopupModel> PopupModels { get; }
        void DestroyModel(PopupModel popupModel);
    }
}