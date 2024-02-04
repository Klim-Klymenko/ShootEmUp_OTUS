using System;
using JetBrains.Annotations;
using UnityEngine;

namespace GameSystem
{
    [UsedImplicitly]
    public sealed class UserInfo
    {
        public event Action<string> OnNameChanged;
        public event Action<string> OnDescriptionChanged;
        public event Action<Sprite> OnIconChanged; 
        
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Sprite Icon { get; private set; }

        public string InitialName { private get; set; }
        public string InitialDescription { private get; set; }
        public Sprite InitialIcon { private get; set; }
        
        public void ResetValues()
        {
            ChangeName(InitialName);
            ChangeDescription(InitialDescription);
            ChangeIcon(InitialIcon);
        }
        
        public void ChangeName(string name)
        {
            Name = name;
            OnNameChanged?.Invoke(name);
        }
        
        public void ChangeDescription(string description)
        {
            Description = description;
            OnDescriptionChanged?.Invoke(description);
        }
        
        public void ChangeIcon(Sprite icon)
        {
            Icon = icon;
            OnIconChanged?.Invoke(icon);
        }
    }
}