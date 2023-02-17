using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppSettingWrapper.HttpClient
{
    public class HttpClientAppSetting : IHttpClientAppSetting
    {
        public string BaseUrl { get; set; }
        private readonly RestClient _httpClient;

        public HttpClientAppSetting(string BaseURL = "https://localhost:7282/api/")
        {
            this.BaseUrl = BaseURL;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            _httpClient = new RestClient(new Uri(BaseURL));
        }
        public async Task<T> GetAsyncForId<T>(string endpoint, string appsettingid)
        {

            var request = new RestRequest($"{endpoint}", Method.Get);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            request.AddParameter("appsettingid", appsettingid);

            var response = await _httpClient.ExecuteAsync<T>(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.Content);
            }
            return response.Data;
        }
        public async Task<T> GetAsync<T>(string endpoint, object args = null)
        {
            var request = new RestRequest($"{endpoint}", Method.Get);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            if (args != null)
            {
                var argsDic = args.ToDictionary();
                foreach (var item in argsDic)
                {
                    string key = item.Key;
                    string value = Convert.ToString(item.Value);
                    request.AddOrUpdateParameter(key, value);
                }
            }
            var response = await _httpClient.ExecuteGetAsync<T>(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine(response.Content);
                if (args != null)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(args));
                }

                var oErr = JsonConvert.DeserializeObject<dynamic>(response.Content);
                if (oErr != null)
                {
                    throw new Exception(response.Content);
                }
            }
            return response.Data;
        }
        public async Task<T> PostAsync<T>(string endpoint, object data, object args = null)
        {

            var request = new RestRequest($"{endpoint}", Method.Post);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            request.RequestFormat = DataFormat.Json;
            request.AddParameter("application/json", JsonConvert.SerializeObject(data), ParameterType.RequestBody);
            Console.WriteLine(JsonConvert.SerializeObject(data));
            if (args != null)
            {
                var argsDic = args.ToDictionary();
                foreach (var item in argsDic)
                {
                    string key = item.Key;
                    string value = Convert.ToString(item.Value);
                    request.AddOrUpdateParameter(key, value);
                }
            }
            var response = await _httpClient.ExecuteAsync<T>(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Data;
            }
            else
            {
                throw new Exception(response.Content);

            }
            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        public async Task<T> DeleteAsync<T>(string endpoint, string id)
        {
            var request = new RestRequest($"{endpoint}?{id}", Method.Delete);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            request.AddQueryParameter("appsettingid", id);
            var response = await _httpClient.ExecuteAsync<dynamic>(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {

            }
            else
            {
                Console.WriteLine(response.Content);
                var oErr = JsonConvert.DeserializeObject<dynamic>(response.Content);
                throw new Exception(oErr.Status + " " + oErr.Message);
            }
            //return response.Data; 
            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        public async Task<T> PutAsync<T>(string endpoint, object data, string args = null)
        {

            string argClean = WebUtility.UrlEncode(args);

            var request = new RestRequest($"{endpoint}?{argClean}", Method.Put);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(data);
            var response = await _httpClient.ExecuteAsync<dynamic>(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {

            }
            else
            {
                Console.WriteLine(response.Content);
                Console.WriteLine(JsonConvert.SerializeObject(data));
                try
                {
                    var oErr = JsonConvert.DeserializeObject<dynamic>(response.Content);
                    if (oErr != null)
                    {
                        throw new Exception(oErr.Status + " " + oErr.Message);
                    }
                }
                catch (Exception)
                {
                    throw new Exception(response.Content);
                }


            }
            return JsonConvert.DeserializeObject<T>(response.Content);
        }
    }

    public static class ExtensionMethods
    {
        public static IDictionary<string, object> ToDictionary(this object data)
        {
            BindingFlags publicAttributes = BindingFlags.Public | BindingFlags.Instance;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();

            foreach (PropertyInfo property in data.GetType().GetProperties(publicAttributes))
            {
                if (property.CanRead)
                    dictionary.Add(property.Name, property.GetValue(data, null));
            }

            return dictionary;
        }
    }


}
