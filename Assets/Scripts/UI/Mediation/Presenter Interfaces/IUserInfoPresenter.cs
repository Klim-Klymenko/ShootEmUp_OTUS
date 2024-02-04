using UniRx;
using UnityEngine;

namespace PM
{
    public interface IUserInfoPresenter
    {
       IReadOnlyReactiveProperty<string> Name { get; }
       IReadOnlyReactiveProperty<string> Description { get; }
       IReadOnlyReactiveProperty<Sprite> Icon { get; }
    }
}