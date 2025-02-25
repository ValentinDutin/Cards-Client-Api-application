//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Common.Models;
using Newtonsoft.Json;
//using System.Runtime.InteropServices.Marshalling;
//using System;

namespace CardsApiWithControllers.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : Controller
    {
        private readonly string _storagePath;

        public CardsController()
        {
            _storagePath = Path.Combine(Directory.GetCurrentDirectory(), "Cards");
            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Cards")))
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Cards"));
            }
            _storagePath += "\\cards.txt";
            using (FileStream fs = new FileStream(_storagePath, FileMode.OpenOrCreate)) { fs.Close(); }
        }

        // GET: /cards
        [HttpGet]
        public async Task<List<Card>> Get()
        {
            string json = "";
            List<Card> cards = [];
            try
            {
                using (StreamReader reader = new StreamReader(_storagePath))
                {
                    json = await reader.ReadToEndAsync();
                    reader.Close();
                }
                if (!String.IsNullOrEmpty(json))
                {
                    if (json.Contains('[') && json.Contains(']'))
                    {
                        cards.AddRange(JsonConvert.DeserializeObject<List<Card>>(json));
                    }
                    else cards.Add(JsonConvert.DeserializeObject<Card>(json));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return cards;
        }

        // POST: /cards
        [HttpPost]
        public ActionResult Add(Card item)
        {
            string json;   
            try
            {
                using (StreamReader reader = new StreamReader(_storagePath))
                {
                    json = reader.ReadToEnd();
                    reader.Close();
                }
                string itemJson = JsonConvert.SerializeObject(item);
                if (!String.IsNullOrEmpty(json))
                {
                    if (json.Contains(']'))
                    {
                        json = json.Substring(0, json.Length - 1) + ',';
                    }
                    else
                    {
                        json = '[' + json + ',';
                    }
                    json += itemJson + ']';
                }
                else
                {
                    json = itemJson;
                }
                using (StreamWriter writer = new StreamWriter(_storagePath))
                {
                    writer.Write(json);
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return Ok();
        }
    }
}
