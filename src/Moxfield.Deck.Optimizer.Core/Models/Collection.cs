namespace Moxfield.Deck.Optimizer.Core.Models;

/// <summary>
/// Represents a Moxfield collection with its cards and priority
/// </summary>
public class Collection
{
    public string Name { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public int Priority { get; set; }
    public List<CardInCollection> Cards { get; set; } = new();

    public override string ToString()
    {
        return $"{Name} (Priority: {Priority}, Cards: {Cards.Count})";
    }
}

/// <summary>
/// Represents a card within a collection with quantity information
/// </summary>
public class CardInCollection
{
    public Card Card { get; set; } = new();
    public int Quantity { get; set; }
    public string CollectionName { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"{Quantity}x {Card} from {CollectionName}";
    }
}
