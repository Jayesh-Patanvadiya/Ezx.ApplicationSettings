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
        public async Task<IActionResult> CreateApplicationSettingst([FromBody] ApplicationSettings ApplicationSettingst)
        {

            var createResult = await _ezxApplicationSettings.CreateApplicationSettings(ApplicationSettingst);
            return Ok(createResult);

        }
        [HttpGet]
        public async Task<IActionResult> GetAllApplicationSettings()
        {
            var result = await _ezxApplicationSettings.GetAllApplicationSettings();
            return Ok(result);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetApplicationSettingstId(string id)
        {
            var result = await _ezxApplicationSettings.GetApplicationSettingsId(id);
            return Ok(result);
        }




        [HttpPut]
        public async Task<IActionResult> UpdateApplicationSettingst([FromBody] ApplicationSettings applicationSettings)
        {

            var result = await _ezxApplicationSettings.UpdateApplicationSettings(applicationSettings);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicationSettingst(string id)
        {
            await _ezxApplicationSettings.DeleteApplicationSettings(id);
            return NoContent();
        }
    }
}
