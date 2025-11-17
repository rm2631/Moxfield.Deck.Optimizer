namespace Moxfield.Deck.Optimizer.Core.Models;

/// <summary>
/// Represents a Magic: The Gathering card with its metadata
/// </summary>
public class Card
{
    public string Name { get; set; } = string.Empty;
    public string ScryfallId { get; set; } = string.Empty;
    public string SetCode { get; set; } = string.Empty;
    public string CollectorNumber { get; set; } = string.Empty;
    public bool IsFoil { get; set; }
    public string? CardType { get; set; }
    public decimal? ManaCost { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is not Card other) return false;
        return Name == other.Name && 
               SetCode == other.SetCode && 
               CollectorNumber == other.CollectorNumber && 
               IsFoil == other.IsFoil;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, SetCode, CollectorNumber, IsFoil);
    }

    public override string ToString()
    {
        var foilIndicator = IsFoil ? " (Foil)" : "";
        return $"{Name} [{SetCode}] #{CollectorNumber}{foilIndicator}";
    }
}
