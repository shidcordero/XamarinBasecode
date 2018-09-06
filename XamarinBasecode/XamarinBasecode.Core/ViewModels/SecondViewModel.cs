using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Xamarin.Forms;
using XamarinBasecode.Core.Base;
using XamarinBasecode.Core.Services;
using XamarinBasecode.Core.Utilities;

namespace XamarinBasecode.Core.ViewModels
{
    public class SecondViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly ILocalizeService _localizeService;
        private readonly IAppSettings _settings;
        private readonly IUserDialogs _userDialogs;
        private Dictionary<string, string> _parameter;

        public string MainPageButtonText { get; set; }
        public DateTime DateValue { get; set; }

        public SecondViewModel(IMvxNavigationService navigationService, IAppSettings settings,
            IUserDialogs userDialogs, ILocalizeService localizeService) 
            : base(userDialogs, localizeService)
        {
            _navigationService = navigationService;
            _settings = settings;
            _userDialogs = userDialogs;
            _localizeService = localizeService;

            MainPageButtonText = "test";
        }

        public IMvxAsyncCommand BackCommand => new MvxAsyncCommand(async () =>
        {
            var localizedText = _localizeService.Translate(_settings.PersistentText);

            await _userDialogs.AlertAsync(localizedText);
            await _navigationService.Close(this);
        });

        public override void Prepare(Dictionary<string, string> parameter)
        {
            _parameter = parameter;

            if (_parameter != null && _parameter.ContainsKey("ButtonText"))
                MainPageButtonText = "ButtonText";
        }

        public string PersistentText
        {
            get => _settings.PersistentText;
            set => _settings.PersistentText = value;
        }
    }
}