using Newtonsoft.Json;
using System.Text;

namespace WebClient.Services
{
    public class ClientService
    {
        private readonly HttpClient _client;
        private string ApiDomain = "http://localhost:5227/";

        public ClientService(HttpClient? client = null)
        {
            _client = client ?? new HttpClient();
            _client.DefaultRequestHeaders.Clear();
            _client.BaseAddress = new Uri(ApiDomain);
        }

        public async Task<T?> Get<T>(string relativeUrl)
        {
            try
            {
                var res = await _client.GetAsync(relativeUrl);
                var content = await res.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch
            {
                return default;
            }
        }

        public async Task<T?> Post<T>(string relativeUrl, object? data)
        {
            try
            {
                var res = await _client.PostAsync(relativeUrl, GetBody(data));
                var content = await res.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch
            {
                return default;
            }
        }
        public async Task<T?> Put<T>(string relativeUrl, object? data)
        {
            try
            {
                var res = await _client.PutAsync(relativeUrl, GetBody(data));
                var content = await res.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch
            {
                return default;
            }
        }

        public async Task<T?> Delete<T>(string relativeUrl)
        {
            try
            {
                var res = await _client.DeleteAsync(relativeUrl);
                //if ((int)res.StatusCode == 401) await _httpContextAccessor.HttpContext.SignOutAsync("CookieAuthentication");
                var content = await res.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch
            {
                return default;
            }
        }

        public async Task<string?> Get(string relativeUrl)
        {
            try
            {
                var res = await _client.GetAsync(relativeUrl);
                return await res.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
            }
            catch
            {
                return default;
            }
        }

        public async Task<string?> Post(string relativeUrl, object? data)
        {
            try
            {
                var res = await _client.PostAsync(relativeUrl, GetBody(data));
                return await res.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
            }
            catch
            {
                return default;
            }
        }

        public async Task<string?> Delete(string relativeUrl)
        {
            try
            {
                var res = await _client.DeleteAsync(relativeUrl);
                return await res.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
            }
            catch
            {
                return default;
            }
        }

        private static StringContent? GetBody(object? data)
        {
            if (data == null) return null;
            var body = JsonConvert.SerializeObject(data);
            return new StringContent(body, Encoding.UTF8, "application/json");
        }

    }
}

