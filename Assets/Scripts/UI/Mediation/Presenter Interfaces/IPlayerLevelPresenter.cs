using System;
using UniRx;

namespace PM
{
    public interface IPlayerLevelPresenter
    {
        Action OnHided { get; }
        
        IReadOnlyReactiveProperty<string> Experience { get; }
        IReadOnlyReactiveProperty<string> Level { get; }
        
        void LevelUp();
        bool CanLevelUp();
    }
}