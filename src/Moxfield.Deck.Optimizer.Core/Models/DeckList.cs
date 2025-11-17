namespace Moxfield.Deck.Optimizer.Core.Models;

/// <summary>
/// Represents a deck list from Moxfield
/// </summary>
public class DeckList
{
    public string Name { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public List<DeckCard> Cards { get; set; } = new();
    public string? Commander { get; set; }

    public override string ToString()
    {
        return $"{Name} ({Cards.Count} cards)";
    }
}

/// <summary>
/// Represents a card in a deck with its board location
/// </summary>
public class DeckCard
{
    public Card Card { get; set; } = new();
    public int Quantity { get; set; }
    public string Board { get; set; } = "mainboard"; // mainboard, sideboard, commander, etc.

    public override string ToString()
    {
        return $"{Quantity}x {Card} ({Board})";
    }
}
