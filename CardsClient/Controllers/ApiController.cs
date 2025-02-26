using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Common.Models;


namespace CardsClient.Services
{
    public class ApiController
    {
        private readonly HttpClient _httpClient;
        public ApiController()
        {
            _httpClient = new HttpClient();
        }
        public async Task PostCardAsync(Card model)
        {
            var json = JsonConvert.SerializeObject(model);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync("http://localhost:5110/api/cards", httpContent);
        }
        public async Task<List<Card>> GetCardsAsync()
        {
            var response = await _httpClient.GetStringAsync("http://localhost:5110/api/cards");
            return JsonConvert.DeserializeObject<List<Card>>(response);
        }
    }
}
