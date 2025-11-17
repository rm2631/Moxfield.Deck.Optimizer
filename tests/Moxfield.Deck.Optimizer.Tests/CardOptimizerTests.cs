using Moxfield.Deck.Optimizer.Core.Models;
using Moxfield.Deck.Optimizer.Core.Services;

namespace Moxfield.Deck.Optimizer.Tests;

public class CardOptimizerTests
{
    [Fact]
    public void Optimize_WithAvailableCards_ReturnsCorrectMovements()
    {
        // Arrange
        var optimizer = new CardOptimizer();
        
        var deck = new DeckList
        {
            Name = "Test Deck",
            Cards = new List<DeckCard>
            {
                new DeckCard 
                { 
                    Card = new Card { Name = "Sol Ring" }, 
                    Quantity = 1 
                }
            }
        };

        var collections = new List<Collection>
        {
            new Collection
            {
                Name = "Collection A",
                Priority = 1,
                Cards = new List<CardInCollection>
                {
                    new CardInCollection
                    {
                        Card = new Card { Name = "Sol Ring" },
                        Quantity = 5
                    }
                }
            }
        };

        // Act
        var result = optimizer.Optimize(deck, collections);

        // Assert
        Assert.Single(result.Movements);
        Assert.Equal("Sol Ring", result.Movements[0].Card.Name);
        Assert.Equal(1, result.Movements[0].Quantity);
        Assert.Equal("Collection A", result.Movements[0].SourceCollection);
        Assert.Equal(1, result.Statistics.CardsFound);
        Assert.Equal(0, result.Statistics.CardsMissing);
    }

    [Fact]
    public void Optimize_WithMissingCards_ReportsMissingCards()
    {
        // Arrange
        var optimizer = new CardOptimizer();
        
        var deck = new DeckList
        {
            Name = "Test Deck",
            Cards = new List<DeckCard>
            {
                new DeckCard 
                { 
                    Card = new Card { Name = "Missing Card" }, 
                    Quantity = 1 
                }
            }
        };

        var collections = new List<Collection>
        {
            new Collection
            {
                Name = "Collection A",
                Priority = 1,
                Cards = new List<CardInCollection>()
            }
        };

        // Act
        var result = optimizer.Optimize(deck, collections);

        // Assert
        Assert.Empty(result.Movements);
        Assert.Single(result.MissingCards);
        Assert.Contains("Missing Card", result.MissingCards[0]);
        Assert.Equal(0, result.Statistics.CardsFound);
        Assert.Equal(1, result.Statistics.CardsMissing);
    }

    [Fact]
    public void Optimize_WithMultipleCollections_UsesHigherPriorityFirst()
    {
        // Arrange
        var optimizer = new CardOptimizer();
        
        var deck = new DeckList
        {
            Name = "Test Deck",
            Cards = new List<DeckCard>
            {
                new DeckCard 
                { 
                    Card = new Card { Name = "Command Tower" }, 
                    Quantity = 1 
                }
            }
        };

        var collections = new List<Collection>
        {
            new Collection
            {
                Name = "Low Priority Collection",
                Priority = 2,
                Cards = new List<CardInCollection>
                {
                    new CardInCollection
                    {
                        Card = new Card { Name = "Command Tower" },
                        Quantity = 10
                    }
                }
            },
            new Collection
            {
                Name = "High Priority Collection",
                Priority = 1,
                Cards = new List<CardInCollection>
                {
                    new CardInCollection
                    {
                        Card = new Card { Name = "Command Tower" },
                        Quantity = 5
                    }
                }
            }
        };

        // Act
        var result = optimizer.Optimize(deck, collections);

        // Assert
        Assert.Single(result.Movements);
        Assert.Equal("High Priority Collection", result.Movements[0].SourceCollection);
        Assert.Equal(1, result.Movements[0].Priority);
    }

    [Fact]
    public void Optimize_WithPartialQuantity_UsesMultipleSources()
    {
        // Arrange
        var optimizer = new CardOptimizer();
        
        var deck = new DeckList
        {
            Name = "Test Deck",
            Cards = new List<DeckCard>
            {
                new DeckCard 
                { 
                    Card = new Card { Name = "Lightning Bolt" }, 
                    Quantity = 5 
                }
            }
        };

        var collections = new List<Collection>
        {
            new Collection
            {
                Name = "Collection A",
                Priority = 1,
                Cards = new List<CardInCollection>
                {
                    new CardInCollection
                    {
                        Card = new Card { Name = "Lightning Bolt" },
                        Quantity = 2
                    }
                }
            },
            new Collection
            {
                Name = "Collection B",
                Priority = 2,
                Cards = new List<CardInCollection>
                {
                    new CardInCollection
                    {
                        Card = new Card { Name = "Lightning Bolt" },
                        Quantity = 3
                    }
                }
            }
        };

        // Act
        var result = optimizer.Optimize(deck, collections);

        // Assert
        Assert.Equal(2, result.Movements.Count);
        Assert.Equal(2, result.Movements[0].Quantity);
        Assert.Equal("Collection A", result.Movements[0].SourceCollection);
        Assert.Equal(3, result.Movements[1].Quantity);
        Assert.Equal("Collection B", result.Movements[1].SourceCollection);
        Assert.Equal(5, result.Statistics.CardsFound);
        Assert.Equal(2, result.Statistics.SourcesUsed);
    }
}
