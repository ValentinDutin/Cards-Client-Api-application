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
            await _httpClient.PostAsync(_url, httpContent);
        }
        public async Task<List<Card>> GetCardsAsync()
        {
            var response = await _httpClient.GetStringAsync(_url);
            Debug.WriteLine(response);
            return JsonConvert.DeserializeObject<List<Card>>(response) ?? [];
        }
        public async Task DeleteCardByIdAsync(Guid id)
        {
            await _httpClient.DeleteAsync(_url + '/' + id);
        }
        public async Task DeleteAllCardsAsync()
        {
            await _httpClient.DeleteAsync(_url);
        }
    }
}
