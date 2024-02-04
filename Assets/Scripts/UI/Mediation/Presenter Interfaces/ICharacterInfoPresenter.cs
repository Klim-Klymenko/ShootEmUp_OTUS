using System;
using UniRx;
using UnityEngine;

namespace PM
{
    public interface ICharacterInfoPresenter
    {
       IReadOnlyReactiveProperty<string> Name { get; }
       IReadOnlyReactiveProperty<string> Description { get; }
       IReadOnlyReactiveProperty<Sprite> Icon { get; }
    }
}