using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Common.Models;
using System;
using static System.Net.WebRequestMethods;


namespace CardsClient.Services
{
    public class ApiController
    {
        private readonly HttpClient _httpClient;
        private const string url = "http://localhost:5110/api/cards";
        public ApiController()
        {
            _httpClient = new HttpClient();
        }
        public async Task PostCardAsync(Card model)
        {
            var json = JsonConvert.SerializeObject(model);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(url, httpContent);
        }
        public async Task<List<Card>> GetCardsAsync()
        {
            var response = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<List<Card>>(response);
        }
        public async Task DeleteCardByIdAsync(Guid id)
        {
            await _httpClient.DeleteAsync(url + '/' + id);
        }
        public async Task DeleteAllCards()
        {
            await _httpClient.DeleteAsync(url);
        }
    }
}
