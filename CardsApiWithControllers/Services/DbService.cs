using Common.Models;
using Newtonsoft.Json;
namespace CardsApiWithControllers.Services
{
    public class DbService : IDbService
    {
        private List<Card> _cards;
        private readonly string _fileName;
        private readonly IConfiguration _config;
        private readonly ILogger<DbService> _logger;
        public DbService(IConfiguration config, ILogger<DbService> logger)
        {
            _config = config;
            var directory = Path.Combine(Directory.GetCurrentDirectory(), _config["DirectoryName"] ?? "Default");
            _fileName = directory + '/' + _config["FileName"] ?? "default.json";
            _logger = logger;
            _cards = ReadFile(_fileName);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
        private List<Card> ReadFile(string filePath)
        {
            string json;
            if (File.Exists(_fileName))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    json = reader.ReadToEnd();
                    reader.Close();
                }
                try
                {
                    return JsonConvert.DeserializeObject<List<Card>>(json) ?? new();
                }
                catch (Exception ex)
                {
                    _logger.LogWarning($"File is not valid Json. {ex.Message}");
                }
            }
            else
                _logger.LogInformation($"File is not exist. File name = {_fileName}");
            return new();
        }
        public List<Card> GetData()
        {
            _logger.LogInformation("Get data");
            return _cards;
        }
        public bool InsertCard(Card item)
        {
            _cards.Add(item);
            try
            {
                using (StreamWriter writer = new StreamWriter(File.Create(_fileName)))
                {
                    writer.Write(JsonConvert.SerializeObject(_cards));
                    writer.Close();
                    _logger.LogInformation("Writing to the file was successful");
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"StreamWriter exception. {ex.Message}");
                return false;
            }
        }
    }
}
