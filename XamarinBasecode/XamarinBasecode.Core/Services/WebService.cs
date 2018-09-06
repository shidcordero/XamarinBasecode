using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Acr.UserDialogs;
using Newtonsoft.Json;
using Xamarin.Forms;
using XamarinBasecode.Core.Models;
using XamarinBasecode.Core.Utilities;

namespace XamarinBasecode.Core.Services
{
    public class WebService : IWebService
    {
        private readonly HttpClient _client;
        private readonly IAppSettings _settings;
        private readonly ILocalizeService _localizeService;
        private readonly IUserDialogs _userDialogs;
        private HttpResponseMessage _response;

        /// <summary>
        /// Constructor for Web Service Class
        /// </summary>
        public WebService(IAppSettings settings, ILocalizeService localizeService, IUserDialogs userDialogs)
        {
            _settings = settings;
            _localizeService = localizeService;
            _userDialogs = userDialogs;
            // Initialize if needed Authentication
            _client = new HttpClient { MaxResponseContentBufferSize = Constants.Http.MaxResponseContentBufferSize };
        }

        /// <summary>
        /// HTTP Request function wrapper for Http Calls
        /// </summary>
        /// <param name="url">URL of the API</param>
        /// <param name="action">Action of the API</param>
        /// <param name="method">Method Type</param>
        /// <param name="contentObject">Data to pass</param>
        /// <returns>String data in JSON format</returns>
        private async Task<string> HttpRequest(string url, string action, string method, string contentObject)
        {
            var returnContent = string.Empty;
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.Common.JsonHeaderValue));
            if (!string.IsNullOrEmpty(_settings.AccessToken))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Common.Bearer, _settings.AccessToken);
            }

            var uri = string.Format(url, action);
            var content = (contentObject != null) ? new StringContent(contentObject, Encoding.UTF8, Constants.Common.JsonHeaderValue) : null;
            switch (method)
            {
                case Constants.Http.Get:
                    _response = await _client.GetAsync(uri);
                    break;

                case Constants.Http.Post:
                    _response = await _client.PostAsync(uri, content);
                    break;

                case Constants.Http.Put:
                    _response = await _client.PutAsync(uri, content);
                    break;

                case Constants.Http.Delete:
                    _response = await _client.DeleteAsync(uri);
                    break;
            }

            if (_response.IsSuccessStatusCode)
            {
                returnContent = await _response.Content.ReadAsStringAsync();
            }
            else
            {
                _response.EnsureSuccessStatusCode();
            }

            return returnContent;
        }

        private async Task<bool> ExceptionHandler()
        {
            var appCloser = DependencyService.Get<ICloseApplication>();
            string localizedText;
            var isNoException = false;

            if (_response.StatusCode == HttpStatusCode.Unauthorized)
            {
                isNoException = await RefreshToken();
                if (!isNoException)
                {
                    isNoException = await ReLogin();
                }
            }
            else switch (_response.StatusCode)
            {
                case HttpStatusCode.InternalServerError:
                    localizedText = _localizeService.Translate(Constants.Messages.ErrorRetrieving);

                    await _userDialogs.AlertAsync(localizedText);
                    appCloser?.ExitApplication();
                    break;
                case HttpStatusCode.NotFound:
                    localizedText = _localizeService.Translate(Constants.Messages.ErrorProcessing);

                    await _userDialogs.AlertAsync(localizedText);
                    appCloser?.ExitApplication();
                    break;
            }
            return isNoException;
        }

        public async Task<bool> Login(string data)
        {
            try
            {
                var accessToken = await HttpRequest(Constants.Http.WebUrl, Constants.Api.Token, Constants.Http.Post, data);

                var tokenData = JsonConvert.DeserializeObject<Token>(accessToken);
                _settings.AccessToken = tokenData.AccessToken;
                _settings.RefreshToken = tokenData.RefreshToken;

                return true;
            }
            catch (Exception)
            {
                if (string.IsNullOrEmpty(_settings.AccessToken)) return false;

                return await RefreshToken();
            }
        }

        private async Task<bool> ReLogin()
        {
            try
            {
                _settings.AccessToken = string.Empty;

                var data =  string.Format(Constants.Common.LoginGrant, HttpUtility.UrlEncode(Constants.Http.ClientId), HttpUtility.UrlEncode(Constants.Http.SecretId));

                var accessToken = await HttpRequest(Constants.Http.WebUrl, Constants.Api.Token, Constants.Http.Post, data);

                var tokenData = JsonConvert.DeserializeObject<Token>(accessToken);
                _settings.AccessToken = tokenData.AccessToken;
                _settings.RefreshToken = tokenData.RefreshToken;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private async Task<bool> RefreshToken()
        {
            try
            {
                var data = string.Format(Constants.Common.GrantTypeRefreshToken, _settings.RefreshToken,
                    HttpUtility.UrlEncode(Constants.Http.ClientId), HttpUtility.UrlEncode(Constants.Http.SecretId));

                var accessToken = await HttpRequest(Constants.Http.WebUrl, Constants.Api.Token, Constants.Http.Post, data);

                var tokenData = JsonConvert.DeserializeObject<Token>(accessToken);
                _settings.AccessToken = tokenData.AccessToken;
                _settings.RefreshToken = tokenData.RefreshToken;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
