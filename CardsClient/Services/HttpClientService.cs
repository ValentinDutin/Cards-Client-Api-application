using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
using Common.Models;
using System.Diagnostics;
using System.Windows;

namespace CardsClient.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;
        public HttpClientService(HttpClient? httpClient)
        {
            _httpClient = httpClient ?? new HttpClient();
            _url = ((App)Application.Current).ConfigData["BaseUrl"] ?? "http://localhost:5110/api/cards";
        }
        public async Task PostCardAsync(Card model)
        {
            var json = JsonConvert.SerializeObject(model);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await _httpClient.PostAsync(_url, httpContent);
            if (response.IsSuccessStatusCode)
                Debug.WriteLine("PostCardsAsync : Successed");
            else
                Debug.WriteLine($"PostCardsAsync : {response.StatusCode}");
        }
        public async Task<List<Card>> GetCardsAsync()
        {
            using HttpResponseMessage response = await _httpClient.GetAsync(_url);
            if (response.IsSuccessStatusCode)
                Debug.WriteLine("GetCardsAsync : Successed");
            else
                Debug.WriteLine($"GetCardsAsync : {response.StatusCode}");
            return JsonConvert.DeserializeObject<List<Card>>(await response.Content.ReadAsStringAsync()) ?? [];
        }
        public async Task DeleteCardByIdAsync(Guid id)
        {
            using HttpResponseMessage response = await _httpClient.DeleteAsync(_url + '/' + id);
            if (response.IsSuccessStatusCode)
                Debug.WriteLine("DeleteCardByIdAsync : Successed");
            else
                Debug.WriteLine($"DeleteCardByIdAsync : {response.StatusCode}");
        }
        public async Task DeleteAllCardsAsync()
        {
            using HttpResponseMessage response = await _httpClient.DeleteAsync(_url);
            if (response.IsSuccessStatusCode)
                Debug.WriteLine("DeleteAllCardsAsync : Successed");
            else
                Debug.WriteLine($"DeleteAllCardsAsync : {response.StatusCode}");
        }
    }
}
