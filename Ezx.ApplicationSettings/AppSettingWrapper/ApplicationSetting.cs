using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSettingWrapper
{
    public class ApplicationSetting
    {
        public string Id { get; set; } 

        public string? SettingName { get; set; }

        public string SettingValue { get; set; }


    }
}
