﻿using MvvmCross;
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinBasecode.Core.Resources
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        private readonly CultureInfo _cultureInfo;

        public TranslateExtension()
        {
            _cultureInfo = Mvx.Resolve<Services.ILocalizeService>().GetCurrentCultureInfo();
        }

        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
            {
                return null;
            }

            var translation = AppResources.ResourceManager.GetString(Text, _cultureInfo);

            #if DEBUG
                if (translation == null)
                {
                    throw new ArgumentException($"Key {Text} was not found for culture {_cultureInfo}.");
                }
            #endif

            return translation;
        }
    }
}
