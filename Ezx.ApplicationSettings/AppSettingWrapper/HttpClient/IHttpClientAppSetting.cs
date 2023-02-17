using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSettingWrapper.HttpClient
{
    public interface IHttpClientAppSetting
    {
        Task<T> GetAsyncForId<T>(string endpoint, string appsettingid);
        Task<T> GetAsync<T>(string endpoint, object args);
        Task<T> PostAsync<T>(string endpoint, object data, object args = null);
        Task<T> DeleteAsync<T>(string endpoint, string appsettingid);

        Task<T> PutAsync<T>(string endpoint, object data, string args = null);
    }
}
