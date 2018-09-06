using System.Threading.Tasks;

namespace XamarinBasecode.Core.Services
{
    public interface IWebService
    {
        Task<bool> Login(string data);
    }
}
