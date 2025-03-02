using Common.Models;
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
            if (!Directory.Exists(_directoryName))
            {
                Directory.CreateDirectory(_directoryName);
            }
        }
        public async Task<List<Card>> GetCardsAsync()
        {   
            if(!File.Exists(_filePath))
            {
                return [];
            }
            var json = await File.ReadAllTextAsync(_filePath);
            if(!String.IsNullOrEmpty(json))
            try
            {
                return JsonSerializer.Deserialize<List<Card>>(json) ?? new();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return [];
        }
        public async Task<Card> GetCardByIdAsync(Guid id)
        {
            var cards = await GetCardsAsync();
            return cards.Find(card => card.Id == id) ?? new();
        }
        public async Task AddCardAsync(Card item)
        {
            var cards = await GetCardsAsync();
            cards.Add(item);
            var json = JsonSerializer.Serialize(cards);
            await File.WriteAllTextAsync(_filePath, json);
        }
        public async Task<bool> DeleteCardByIdAsync(Guid id)
        {
            var cards = await GetCardsAsync();
            var item = cards.Find(card => card.Id == id);
            if (item != null)
            {
                cards.Remove(item);
                var json = JsonSerializer.Serialize(cards);
                await File.WriteAllTextAsync(_filePath, json);
                return true;
            }
            return false;
        }
        public async Task DeleteAllCards()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    await File.WriteAllTextAsync(_filePath, "");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
