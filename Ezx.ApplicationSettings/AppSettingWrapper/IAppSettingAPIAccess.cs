using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSettingWrapper
{
    public interface IAppSettingAPIAccess
    {
        Task<ApplicationSetting> CreateApplicationSetting(ApplicationSetting applicationSettings);
        Task<List<ApplicationSetting>> GetAllApplicationSettings();
        Task<ApplicationSetting> UpdateApplicationSetting(ApplicationSetting applicationSettings);
        Task<string> DeleteApplicationSetting(string appSettingId);
        Task<ApplicationSetting> GetApplicationSettingId(string appSettingId);
    }
}
