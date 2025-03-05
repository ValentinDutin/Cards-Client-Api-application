using System.Net.Http;
using CommonFiles.Models;
using CommonFiles.Services;
using System.Diagnostics;
using System.Net.Http.Json;

namespace CardsClient.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;
        public HttpClientService(HttpClient httpClient, IConfigDataService configData)
        {
            try
            {
                _httpClient = httpClient;
                _url = configData.GetData("BaseUrl");
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task PostCardAsync(Card model)
        {
            try
            {
                using HttpResponseMessage response = await _httpClient.PostAsJsonAsync<Card>(_url, model);
                if (response.IsSuccessStatusCode)
                    Debug.WriteLine("PostCardsAsync : Successed");
                else
                {
                    Debug.WriteLine($"PostCardsAsync : {response.StatusCode}");
                    throw new Exception($"PostCardsAsync : {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<Card>> GetCardsAsync()
        {
            try
            {
                using HttpResponseMessage response = await _httpClient.GetAsync(_url);
                if (response.IsSuccessStatusCode)
                    Debug.WriteLine("GetCardsAsync : Successed");
                else
                {
                    Debug.WriteLine($"GetCardsAsync : {response.StatusCode}");
                    throw new Exception($"GetCardsAsync : {response.StatusCode}");
                }
                return await response.Content.ReadFromJsonAsync<List<Card>>() ?? [];
            }
            catch (Exception ex)
            {
                Debug.WriteLine("HttpClient class : " + ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public async Task DeleteCardByIdAsync(Guid id)
        {
            try
            {
                using HttpResponseMessage response = await _httpClient.DeleteAsync(_url + '/' + id);
                if (response.IsSuccessStatusCode)
                    Debug.WriteLine("DeleteCardByIdAsync : Successed");
                else
                {
                    Debug.WriteLine($"DeleteCardByIdAsync : {response.StatusCode}");
                    throw new Exception($"DeleteCardByIdAsync : {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task DeleteAllCardsAsync()
        {
            try
            {
                using HttpResponseMessage response = await _httpClient.DeleteAsync(_url);
                if (response.IsSuccessStatusCode)
                    Debug.WriteLine("DeleteAllCardsAsync : Successed");
                else
                {
                    Debug.WriteLine($"DeleteAllCardsAsync : {response.StatusCode}");
                    throw new Exception($"DeleteAllCardsAsync : {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
