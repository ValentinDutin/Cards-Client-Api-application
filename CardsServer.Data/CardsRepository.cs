using Common.Models;
using System.IO;
using System.Text.Json;

namespace CardsServer.Data
{
    public class CardsRepository
    {
        private readonly string _directoryName = "../DB";
        private readonly string _filePath = "Cards.json";

        public CardsRepository()
        {
            _filePath = Path.Combine(_directoryName, _filePath);
        }
        public async Task<List<Card>> GetCardsAsync()
        {   
            if(!File.Exists(_filePath))
            {
                return new();
            }
            var json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<Card>>(json) ?? new();
        }
        public async Task AddCardAsync(Card item)
        {
            if (!Directory.Exists(_directoryName))
            {
                Directory.CreateDirectory(_directoryName);
            }
            var cards = await GetCardsAsync();
            cards.Add(item);
            var json = JsonSerializer.Serialize(cards);
            await File.WriteAllTextAsync(_filePath, json);
        }
    }
}
