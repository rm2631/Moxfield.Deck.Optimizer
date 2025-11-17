namespace Moxfield.Deck.Optimizer.Core.Models;

/// <summary>
/// Represents the result of the deck optimization process
/// </summary>
public class OptimizationResult
{
    public DeckList Deck { get; set; } = new();
    public List<CardMovement> Movements { get; set; } = new();
    public List<string> MissingCards { get; set; } = new();
    public OptimizationStatistics Statistics { get; set; } = new();

    public override string ToString()
    {
        return $"Deck: {Deck.Name}, Movements: {Movements.Count}, Missing: {MissingCards.Count}, Sources Used: {Statistics.SourcesUsed}";
    }
}

/// <summary>
/// Represents a single card movement from a collection to the deck
/// </summary>
public class CardMovement
{
    public Card Card { get; set; } = new();
    public string SourceCollection { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int Priority { get; set; }
    public string Board { get; set; } = "mainboard";

    public override string ToString()
    {
        return $"Move {Quantity}x {Card} from {SourceCollection} (Priority: {Priority})";
    }
}

/// <summary>
/// Statistics about the optimization process
/// </summary>
public class OptimizationStatistics
{
    public int TotalCardsNeeded { get; set; }
    public int CardsFound { get; set; }
    public int CardsMissing { get; set; }
    public int SourcesUsed { get; set; }
    public Dictionary<string, int> CardsPerSource { get; set; } = new();

    public override string ToString()
    {
        return $"Found: {CardsFound}/{TotalCardsNeeded}, Missing: {CardsMissing}, Sources: {SourcesUsed}";
    }
}
