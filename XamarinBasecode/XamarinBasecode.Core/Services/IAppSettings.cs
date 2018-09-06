namespace XamarinBasecode.Core.Services
{
    public interface IAppSettings
    {
        string PersistentText { get; set; }
        string AccessToken { get; set; }
        string RefreshToken { get; set; }
    }
}