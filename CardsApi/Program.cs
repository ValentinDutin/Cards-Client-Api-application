using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using Common.Models;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();
List<Card> cards = new List<Card>();
string storagePath = Path.Combine(Directory.GetCurrentDirectory(), "Cards");

// Configure the HTTP request pipeline.

app.MapGet("api/cards", () =>
{
    try
    {
        StreamReader reader = new StreamReader(storagePath + "\\cards.txt");
        string buffer = "";
        string cardsJson = "";
        while ((buffer = reader.ReadLine()) != null)
        {
            cardsJson += buffer;
        }
        cards.Clear();
        cards.AddRange(JsonConvert.DeserializeObject<List<Card>>(cardsJson));
        reader.Close();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    return cards;
});

app.MapPost("api/cards", (Card item) =>
{
    cards.Add(item);
    if (!Directory.Exists(storagePath))
    {
        Directory.CreateDirectory(storagePath);
    }
    var json = JsonConvert.SerializeObject(cards);
    try
    {
        StreamWriter writer = new StreamWriter(storagePath + "\\cards.txt");
        writer.Write(json);
        writer.Close();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }
    return item;
});

app.Run();