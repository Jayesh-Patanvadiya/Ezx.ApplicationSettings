using Google.Cloud.Firestore;
using Newtonsoft.Json;

namespace Ezx.ApplicationSettings.Services
{
    public class EzxApplicationSettingsService : IEzxApplicationSettingsService
    {
        string projectId;
        FirestoreDb fireStoreDb;

        public EzxApplicationSettingsService()
        {
            //_configuration = configuration;
            string filepath = @"\test-2a07f-4688daf8c712.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filepath);

            projectId = "test-2a07f";
            fireStoreDb = FirestoreDb.Create(projectId);
        }



        public async Task<ApplicationSettings> CreateApplicationSetting(ApplicationSettings applicationSetting)
        {
            try
            {
                CollectionReference colRef = fireStoreDb.Collection("ezx.applicationsettings");
                var result = await colRef.AddAsync(applicationSetting);

                applicationSetting.Id = result.Id;
                return applicationSetting;

            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex.Message);
            }

        }

        public async Task<List<ApplicationSettings>> GetAllApplicationSettings()
        {
            Query ezPickupPointQuery = fireStoreDb.Collection("ezx.applicationsettings");
            QuerySnapshot ezPickupPointQuerySnapshot = await ezPickupPointQuery.GetSnapshotAsync();
            List<ApplicationSettings> applicationSettings = new List<ApplicationSettings>();

            foreach (DocumentSnapshot documentSnapshot in ezPickupPointQuerySnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    Dictionary<string, object> ezPickupPointDic = documentSnapshot.ToDictionary();
                    string json = JsonConvert.SerializeObject(ezPickupPointDic);
                    ApplicationSettings newApplicationSettings = JsonConvert.DeserializeObject<ApplicationSettings>(json);
                    newApplicationSettings.Id = documentSnapshot.Id;
                    applicationSettings.Add(newApplicationSettings);
                }
            }
            return applicationSettings;

        }

        public async Task<ApplicationSettings> UpdateApplicationSetting(ApplicationSettings applicationSettings)
        {
            DocumentReference ezApplicationSettings = fireStoreDb.Collection("ezx.applicationsettings").Document(applicationSettings.Id);
            await ezApplicationSettings.SetAsync(applicationSettings, SetOptions.Overwrite);
            return applicationSettings;

        }
        public async Task<ApplicationSettings> GetApplicationSettingId(string appsettingid)
        {
            try
            {
                DocumentReference docRef = fireStoreDb.Collection("ezx.applicationsettings").Document(appsettingid);
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

                if (snapshot.Exists)
                {
                    ApplicationSettings applicationSettings = snapshot.ConvertTo<ApplicationSettings>();
                    applicationSettings.Id = snapshot.Id;
                    return applicationSettings;
                }
                else
                {
                    return new ApplicationSettings();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex.Message);
            }

        }
        public async Task<string> DeleteApplicationSetting(string appSettingId)
        {
            try
            {
                DocumentReference ezApplicationSettings = fireStoreDb.Collection("ezx.applicationsettings").Document(appSettingId);
                await ezApplicationSettings.DeleteAsync();
                return "Deleted Successfully!";
            }
            catch (Exception ex)
            {
                throw new Exception("Error " + ex.Message);
            }
        }
    }
}
