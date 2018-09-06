using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Acr.UserDialogs;
using MvvmCross.ViewModels;
using XamarinBasecode.Core.Services;
using XamarinBasecode.Core.Utilities;

namespace XamarinBasecode.Core.Base
{
    public class BaseViewModel : MvxViewModel<Dictionary<string,string>>, INotifyPropertyChanged
    {
        public override void Prepare(Dictionary<string, string> parameter)
        {
            base.Prepare();
        }

        public new event PropertyChangedEventHandler PropertyChanged;
        public readonly IUserDialogs UserDialogs;
        public readonly ILocalizeService LocalizeService;
        private bool _isBusy;
        private bool _isNotBusy = true;
        private bool _canLoadMore = true;


        public BaseViewModel(IUserDialogs userDialogs, ILocalizeService localizeService)
        {
            UserDialogs = userDialogs;
            LocalizeService = localizeService;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (SetProperty(ref _isBusy, value))
                    IsNotBusy = !_isBusy;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is not busy.
        /// </summary>
        /// <value><c>true</c> if this instance is not busy; otherwise, <c>false</c>.</value>
        public bool IsNotBusy
        {
            get => _isNotBusy;
            private set => SetProperty(ref _isNotBusy, value);
        }

        /// <summary>
        /// Gets or sets if we can load more.
        /// </summary>
        public bool CanLoadMore
        {
            get => _canLoadMore;
            set => SetProperty(ref _canLoadMore, value);
        }

        public async void ShowErrorPage(string message)
        {
            var localizedMessage = LocalizeService.Translate(message);

            await UserDialogs.AlertAsync(localizedMessage);
        }

        protected bool SetProperty<T>(
            ref T backingStore, T value,
            [CallerMemberName]string propertyName = Constants.SpecialCharacters.EmptyString,
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;

            onChanged?.Invoke();

            OnPropertyChanged(propertyName);
            return true;
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = Constants.SpecialCharacters.EmptyString)
        {
            var changed = PropertyChanged;

            changed?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
