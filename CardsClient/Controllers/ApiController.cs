using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Common.Models;
using System;
using System.Configuration;


namespace CardsClient.Controllers
{
    public class ApiController
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;
        public ApiController()
        {
            _httpClient = new HttpClient();
            _url = ConfigurationManager.AppSettings["StartUpHttpUrl"].ToString() ?? "http://localhost:5110/api/cards";
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
            return JsonConvert.DeserializeObject<List<Card>>(response);
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
