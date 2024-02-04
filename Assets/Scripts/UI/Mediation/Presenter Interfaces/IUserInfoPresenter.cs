using System;
using UniRx;
using UnityEngine;

namespace PM
{
    public interface IUserInfoPresenter
    {
        Action OnHided { get; }
        
       IReadOnlyReactiveProperty<string> Name { get; }
       IReadOnlyReactiveProperty<string> Description { get; }
       IReadOnlyReactiveProperty<Sprite> Icon { get; }
    }
}