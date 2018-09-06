using PCLAppConfig;

namespace XamarinBasecode.Core.Utilities
{
    public static class Constants
    {
        public static class Common
        {
            public const string LoadingKey = "IsBusy";
            public const string JsonHeaderValue = "application/json";
            public const string ButtonText = "ButtonText";
            public const string Service = "Service";
            public const string Repository = "Repository";
            public const string AppDelegate = "AppDelegate";
            public const string Bearer = "Bearer";
            public const string LoginGrant = "grant_type=password&client_id={0}&client_secret={1}";
            public const string GrantTypeRefreshToken = "grant_type=refresh_token&refresh_token={0}&client_id={1}&client_secret={2}";
        }

        public static class AppSettings
        {
            public const string AppConfig = "XamarinBasecode.Core.App.config";
            public const string WebUrl = "WebUrl";
            public const string ClientId = "ClientId";
            public const string SecretId = "SecretId";
            public const string PersistentTextKey = "PersistentTextKey";
            public const string PersistentTextDefaultValue = "Default Value";
            public const string AccessToken = "AccessToken";
            public const string RefreshToken = "RefreshToken";
        }
        
        public static class SpecialCharacters
        {
            public const string Colon = ":";
            public const string Underscore = "_";
            public const string Dash = "-";
            public const string Space = " ";
            public const string EmptyString = "";
            public const char CharSpace = ' ';
        }

        public static class Messages
        {
            public const string ErrorRetrieving = "There is an error while retrieving the data. Please contact administrator.";
            public const string ErrorProcessing = "An error occured while processing the request. Please contact administrator.";
            public const string SuccessGet = "Data Retrieval Successful!";
        }
        
        public static class Http
        {
            public static string WebUrl => ConfigurationManager.AppSettings[AppSettings.WebUrl];
            public static string ClientId => ConfigurationManager.AppSettings[AppSettings.ClientId];
            public static string SecretId => ConfigurationManager.AppSettings[AppSettings.SecretId];

            public const string Get = "GET";
            public const string Post = "POST";
            public const string Put = "PUT";
            public const string Delete = "DELETE";
            public const int MaxResponseContentBufferSize = 500000;
        }

        public static class Api
        {
            public const string Token = "token";
        }

        public static class Behavior
        {
            public const string EventName = "EventName";
            public const string Command = "Command";
            public const string CommandParameter = "CommandParameter";
            public const string Converter = "Converter";
            public const string OnEvent = "OnEvent";
        }

        public static class CultureInfo
        {
            public const string English = "en";
            public const string EnglishUs = "en-US";
        }
    }
}
