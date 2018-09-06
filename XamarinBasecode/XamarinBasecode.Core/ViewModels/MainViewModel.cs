using System;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinBasecode.Core.Base;
using XamarinBasecode.Core.Services;
using XamarinBasecode.Core.Utilities;

namespace XamarinBasecode.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IAppSettings _settings;
        private readonly IWebService _webService;

        public MainViewModel(IMvxNavigationService navigationService, IAppSettings settings, 
            IUserDialogs userDialogs, ILocalizeService localizeService, IWebService webService) 
            : base(userDialogs, localizeService)
        {
            _navigationService = navigationService;
            _settings = settings;
            _webService = webService;

            ButtonText = Resources.AppResources.MainPageButton;
        }

        public IMvxCommand PressMeCommand => new MvxCommand(() => 
        {
            ButtonText = Resources.AppResources.MainPageButtonPressed;

        });

        public IMvxAsyncCommand GoToSecondPageCommand => new MvxAsyncCommand(async () => 
        {
            var param = new Dictionary<string, string> { { Constants.Common.ButtonText, ButtonText } };

            await _navigationService.Navigate<SecondViewModel, Dictionary<string, string>>(param);
        });

        public IMvxAsyncCommand CallWebService => new MvxAsyncCommand(async () =>
        {
            if (IsBusy)
                return;

            IsBusy = true;
            var error = false;

            try
            {
                // calling to web service
                //var data = await _webService.Login(string.Empty);

                //Simulating Web Service Call
                await Task.Delay(5000);
                var localizedMessage = LocalizeService.Translate(Constants.Messages.SuccessGet);
                await UserDialogs.AlertAsync(localizedMessage);
            }
            catch (Exception)
            {
                error = true;
            }
            finally
            {
                IsBusy = false;
            }

            if (error)
            {
                var localizedMessage = LocalizeService.Translate(Constants.Messages.ErrorRetrieving);
                await UserDialogs.AlertAsync(localizedMessage);
            }
        });

        public IMvxCommand OpenUrlCommand => new MvxAsyncCommand<string>(async (url) =>
        {
            await Browser.OpenAsync(url, BrowserLaunchType.External);
        });

        public string ButtonText { get; set; }

        public string PersistentText
        {
            get => _settings.PersistentText;
            set => _settings.PersistentText = value;
        }
    }
}