using UnityEngine;

namespace PM
{
    public interface ICharacterPresenter
    {
        string Name { get; }
        string[] ValueNames { get; }
        string Description { get; }
        int[] Values { get; }
        Sprite Icon { get; }
        int Experience { get; }
        int RequiredExperience { get; }
        int Level { get; }
        void LevelUp();
        bool CanLevelUp();
    }
}