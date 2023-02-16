using Google.Cloud.Firestore;
using System.ComponentModel.DataAnnotations;

namespace Ezx.ApplicationSettings.Services
{
    [FirestoreData]

    public class ApplicationSettings
    {
        public string Id { get; set; } // firebase unique id

        [FirestoreProperty]
        [Required]
        public string? SettingName { get; set; }


        [FirestoreProperty]
        [Required]
        public string SettingValue { get; set; }


    }
}
