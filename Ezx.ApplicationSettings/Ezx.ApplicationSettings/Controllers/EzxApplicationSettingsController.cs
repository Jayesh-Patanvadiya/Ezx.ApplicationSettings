using Ezx.ApplicationSettings.Services;
using Microsoft.AspNetCore.Mvc;

namespace EzxApplicationSettings.Controllers
{
    [Route("api/AppSetting")]
    [ApiController]
    public class EzxApplicationSettings : ControllerBase
    {
        IEzxApplicationSettingsService _ezxApplicationSettings;
        public EzxApplicationSettings(IEzxApplicationSettingsService ezxApplicationSettings)
        {
            _ezxApplicationSettings = ezxApplicationSettings;
        }
        [HttpPost]
        public async Task<ApplicationSettings> CreateApplicationSettings([FromBody] ApplicationSettings applicationSetting)
        {
            var createResult = await _ezxApplicationSettings.CreateApplicationSetting(applicationSetting);
            return createResult;

        }
        [HttpGet]
        public async Task<List<ApplicationSettings>> GetAllApplicationSettings()
        {
            return await _ezxApplicationSettings.GetAllApplicationSettings();
        }

        [HttpGet("appsettingid")]
        public async Task<ApplicationSettings> GetApplicationSettingId(string appsettingid)
        {
            return await _ezxApplicationSettings.GetApplicationSettingId(appsettingid);
        }


        [HttpPut]
        public async Task<ApplicationSettings> UpdateApplicationSetting([FromBody] ApplicationSettings applicationSetting)
        {

            return await _ezxApplicationSettings.UpdateApplicationSetting(applicationSetting);
        }

        [HttpDelete]
        public async Task<string> DeleteApplicationSettingst(string appsettingid)
        {
            return await _ezxApplicationSettings.DeleteApplicationSetting(appsettingid);
        }
    }
}
