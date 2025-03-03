using CommonFiles.Models;
using CommonFiles.Services;
using System.Text.Json;

namespace CardsServer.Data
{
    public class CardsRepository
    {
        private readonly string _directoryName;
        private readonly string _filePath;
        public CardsRepository(IConfigDataService configDataService)
        {
            try
            {
                _directoryName = configDataService.GetData("DbDirectoryRelativePath");
                _filePath = configDataService.GetData("DbFileName");
                _filePath = Path.Combine(_directoryName, _filePath);
                if (!Directory.Exists(_directoryName))
                {
                    Directory.CreateDirectory(_directoryName);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("CardsRepository : " + ex.Message);
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
                return JsonSerializer.Deserialize<List<Card>>(json) ?? [];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                throw new Exception(ex.Message);
            }
        }
    }
}
