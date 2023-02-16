namespace Ezx.ApplicationSettings.Services
{
    public interface IEzxApplicationSettingsService
    {
        Task<ApplicationSettings> CreateApplicationSettings(ApplicationSettings applicationSettings);
        Task<List<ApplicationSettings>> GetAllApplicationSettings();
        Task<ApplicationSettings> UpdateApplicationSettings(ApplicationSettings applicationSettings);
        Task<string> DeleteApplicationSettings(string id);
        Task<ApplicationSettings> GetApplicationSettingsId(string id);
    }
}
