namespace Ezx.ApplicationSettings.Services
{
    public interface IEzxApplicationSettingsService
    {
        Task<ApplicationSettings> CreateApplicationSetting(ApplicationSettings applicationSettings);
        Task<List<ApplicationSettings>> GetAllApplicationSettings();
        Task<ApplicationSettings> UpdateApplicationSetting(ApplicationSettings applicationSettings);
        Task<string> DeleteApplicationSetting(string appSettingId);
        Task<ApplicationSettings> GetApplicationSettingId(string appSettingId);
    }
}
