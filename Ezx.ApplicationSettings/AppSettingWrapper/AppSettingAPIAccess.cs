using AppSettingWrapper.HttpClient;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;

namespace AppSettingWrapper
{
    public class AppSettingAPIAccess : IAppSettingAPIAccess
    {

        private const string appSettingCacheKey = "appSetting";
        private readonly IHttpClientAppSetting _httpClientAppSetting;
        private IMemoryCache _cache;
        private ILogger<AppSettingAPIAccess> _logger;

        public AppSettingAPIAccess(IHttpClientAppSetting httpClientAppSetting, IMemoryCache cache, ILogger<AppSettingAPIAccess> logger)
        {
            _httpClientAppSetting = httpClientAppSetting;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }

        public async Task<ApplicationSetting> CreateApplicationSetting(ApplicationSetting applicationSetting)
        {
            try
            {
                applicationSetting.Id = "";
                var result = await _httpClientAppSetting.PostAsync<ApplicationSetting>("AppSetting", applicationSetting);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex.Message);
            }

        }
        public async Task<List<ApplicationSetting>> GetAllApplicationSettings()
        {
            try
            {
                //if cache data is available
                var checkCache = _cache.TryGetValue(appSettingCacheKey, out List<ApplicationSetting> appSetting);
                if (checkCache && appSetting != null)
                {
                    _logger.Log(LogLevel.Information, "App Settings  found in cache.");
                    return appSetting;
                }
                else
                {
                    _logger.Log(LogLevel.Information, "App Setting not found in cache. Fetching from API.");
                    var result = await _httpClientAppSetting.GetAsync<List<ApplicationSetting>>("AppSetting", null);
                    //setup ready for cache
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromMinutes(60))
                            .SetAbsoluteExpiration(TimeSpan.FromMinutes(60))
                            .SetPriority(CacheItemPriority.Normal)
                            .SetSize(1024);
                    //set cache with App Settings data
                    _cache.Set(appSettingCacheKey, result, cacheEntryOptions);
                    return result;

                }


            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex.Message);
            }

        }

        public async Task<ApplicationSetting> UpdateApplicationSetting(ApplicationSetting applicationSetting)
        {
            try
            {
                var result = await _httpClientAppSetting.PutAsync<ApplicationSetting>("AppSetting", applicationSetting);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex.Message);
            }

        }

        public async Task<ApplicationSetting> GetApplicationSettingId(string appsettingid)
        {
            try
            {
                //if cache data is available
                var cacheData = _cache.TryGetValue(appSettingCacheKey, out List<ApplicationSetting> appSetting);
                if (cacheData && appSetting != null)
                {
                    _logger.Log(LogLevel.Information, "App Settings  found in cache.");
                    var app = appSetting.Where(x => x.Id == appsettingid).FirstOrDefault();
                    if (app is not null)
                    {
                        return app;
                    }
                }
                else
                {
                    var result = await _httpClientAppSetting.GetAsyncForId<ApplicationSetting>("AppSetting/appsettingid", appsettingid);
                    return result;
                }
                return new ApplicationSetting();

            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex.Message);
            }

        }

        public async Task<string> DeleteApplicationSetting(string appsettingid)
        {
            try
            {
                var result = await _httpClientAppSetting.DeleteAsync<string>("AppSetting", appsettingid);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex.Message);
            }

        }

    }
}