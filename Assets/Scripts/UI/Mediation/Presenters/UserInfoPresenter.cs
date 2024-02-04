using System;
using GameSystem;
using JetBrains.Annotations;
using PM;
using UniRx;
using UnityEngine;

namespace PM
{
    [UsedImplicitly]
    internal sealed class UserInfoPresenter : IUserInfoPresenter, IDisposable
    {
        IReadOnlyReactiveProperty<string> IUserInfoPresenter.Name => _name;
        IReadOnlyReactiveProperty<string> IUserInfoPresenter.Description => _description;
        IReadOnlyReactiveProperty<Sprite> IUserInfoPresenter.Icon => _icon;
        
        private readonly ReactiveProperty<string> _name;
        private readonly ReactiveProperty<string> _description;
        private readonly ReactiveProperty<Sprite> _icon;
        
        private readonly UserInfo _userInfo;
        
        internal UserInfoPresenter(UserInfo userInfo)
        {
            _userInfo = userInfo;

            _name = new ReactiveProperty<string>(_userInfo.Name);
            _description = new ReactiveProperty<string>(_userInfo.Description);
            _icon = new ReactiveProperty<Sprite>(_userInfo.Icon);
            
            _userInfo.OnNameChanged += UpdateName;
            _userInfo.OnDescriptionChanged += UpdateDescription;
            _userInfo.OnIconChanged += UpdateAvatar;
        }
        
        private void UpdateName(string name)
        {
            _name.Value = name;
        }

        private void UpdateDescription(string description)
        {
            _description.Value = description;
        }

        private void UpdateAvatar(Sprite icon)
        {
            _icon.Value = icon;
        }

        void IDisposable.Dispose()
        {
            _userInfo.OnNameChanged -= UpdateName;
            _userInfo.OnDescriptionChanged -= UpdateDescription;
            _userInfo.OnIconChanged -= UpdateAvatar;
        }
    }
}