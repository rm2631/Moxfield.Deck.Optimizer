using Moxfield.Deck.Optimizer.Core.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Moxfield.Deck.Optimizer.Core.Services;

/// <summary>
/// Client for interacting with Moxfield's public API
/// </summary>
public class MoxfieldClient : IMoxfieldClient
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://api2.moxfield.com";

    public MoxfieldClient()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(BaseUrl)
        };
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "Moxfield.Deck.Optimizer/1.0");
    }

    public MoxfieldClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<DeckList> GetDeckAsync(string deckId)
    {
        var response = await _httpClient.GetAsync($"/v2/decks/all/{deckId}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var json = JObject.Parse(content);

        var deck = new DeckList
        {
            Id = deckId,
            Name = json["name"]?.ToString() ?? "Unknown Deck"
        };

        // Parse commanders
        var commanders = json["boards"]?["commanders"]?["cards"] as JObject;
        if (commanders != null)
        {
            foreach (var cardProp in commanders.Properties())
            {
                var cardData = cardProp.Value as JObject;
                if (cardData != null)
                {
                    var card = ParseCard(cardData);
                    deck.Cards.Add(new DeckCard
                    {
                        Card = card,
                        Quantity = cardData["quantity"]?.Value<int>() ?? 1,
                        Board = "commanders"
                    });
                }
            }
        }

        // Parse mainboard
        var mainboard = json["boards"]?["mainboard"]?["cards"] as JObject;
        if (mainboard != null)
        {
            foreach (var cardProp in mainboard.Properties())
            {
                var cardData = cardProp.Value as JObject;
                if (cardData != null)
                {
                    var card = ParseCard(cardData);
                    deck.Cards.Add(new DeckCard
                    {
                        Card = card,
                        Quantity = cardData["quantity"]?.Value<int>() ?? 1,
                        Board = "mainboard"
                    });
                }
            }
        }

        return deck;
    }

    public async Task<List<Collection>> GetCollectionsAsync(string username)
    {
        // Note: Moxfield's collection API requires authentication
        // For demonstration purposes, this returns an empty list
        // In production, you would need to implement proper authentication
        
        // Placeholder for actual API call
        await Task.CompletedTask;
        return new List<Collection>();
    }

    private Card ParseCard(JObject cardData)
    {
        var card = cardData["card"] as JObject;
        
        return new Card
        {
            Name = card?["name"]?.ToString() ?? "Unknown Card",
            ScryfallId = card?["scryfall_id"]?.ToString() ?? "",
            SetCode = card?["set"]?.ToString()?.ToUpper() ?? "",
            CollectorNumber = card?["cn"]?.ToString() ?? "",
            IsFoil = cardData["finish"]?.ToString() == "foil",
            CardType = card?["type_line"]?.ToString()
        };
    }
}
