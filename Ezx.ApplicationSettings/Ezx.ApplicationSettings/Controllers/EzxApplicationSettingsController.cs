using Ezx.ApplicationSettings.Services;
using Microsoft.AspNetCore.Mvc;

namespace EzxApplicationSettings.Controllers
{
    [Route("api/Ezx.ApplicationSettings")]
    [ApiController]
    public class EzxApplicationSettings : ControllerBase
    {
        IEzxApplicationSettingsService _ezxApplicationSettings;
        public EzxApplicationSettings(IEzxApplicationSettingsService ezxApplicationSettings)
        {
            _ezxApplicationSettings = ezxApplicationSettings;
        }
        [HttpPost]
        public async Task<ApplicationSettings> CreateApplicationSettingst([FromBody] ApplicationSettings applicationSettingst)
        {

            var createResult = await _ezxApplicationSettings.CreateApplicationSettings(applicationSettingst);
            return createResult;

        }
        [HttpGet]
        public async Task<List<ApplicationSettings>> GetAllApplicationSettings()
        {
            return await _ezxApplicationSettings.GetAllApplicationSettings();
        }

        [HttpGet("id")]
        public async Task<ApplicationSettings> GetApplicationSettingstId(string id)
        {
            return await _ezxApplicationSettings.GetApplicationSettingsId(id);
        }


        [HttpPut]
        public async Task<ApplicationSettings> UpdateApplicationSettingst([FromBody] ApplicationSettings applicationSettings)
        {

            return await _ezxApplicationSettings.UpdateApplicationSettings(applicationSettings);
        }

        [HttpDelete("{id}")]
        public async Task<string> DeleteApplicationSettingst(string id)
        {
            return await _ezxApplicationSettings.DeleteApplicationSettings(id);
        }
    }
}
