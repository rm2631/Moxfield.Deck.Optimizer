using Moxfield.Deck.Optimizer.Core.Models;

namespace Moxfield.Deck.Optimizer.Core.Services;

/// <summary>
/// Optimizes card selection from collections using priority-based selection and source minimization
/// </summary>
public class CardOptimizer : ICardOptimizer
{
    public OptimizationResult Optimize(DeckList deck, List<Collection> collections)
    {
        var result = new OptimizationResult
        {
            Deck = deck
        };

        // Sort collections by priority (lower number = higher priority)
        var sortedCollections = collections.OrderBy(c => c.Priority).ToList();

        // Track which cards we still need
        var cardsNeeded = new Dictionary<Card, (int quantity, string board)>();
        foreach (var deckCard in deck.Cards)
        {
            cardsNeeded[deckCard.Card] = (deckCard.Quantity, deckCard.Board);
        }

        // Track sources used
        var sourcesUsed = new HashSet<string>();

        // Process each card needed
        foreach (var (card, (quantityNeeded, board)) in cardsNeeded.ToList())
        {
            var remainingQuantity = quantityNeeded;

            // Try to fulfill from collections in priority order
            foreach (var collection in sortedCollections)
            {
                if (remainingQuantity <= 0) break;

                // Find matching card in collection
                var cardInCollection = collection.Cards
                    .FirstOrDefault(c => CardsMatch(c.Card, card));

                if (cardInCollection != null && cardInCollection.Quantity > 0)
                {
                    var quantityToTake = Math.Min(remainingQuantity, cardInCollection.Quantity);

                    result.Movements.Add(new CardMovement
                    {
                        Card = card,
                        SourceCollection = collection.Name,
                        Quantity = quantityToTake,
                        Priority = collection.Priority,
                        Board = board
                    });

                    remainingQuantity -= quantityToTake;
                    sourcesUsed.Add(collection.Name);

                    if (!result.Statistics.CardsPerSource.ContainsKey(collection.Name))
                    {
                        result.Statistics.CardsPerSource[collection.Name] = 0;
                    }
                    result.Statistics.CardsPerSource[collection.Name]++;
                }
            }

            // If we couldn't find all copies, add to missing list
            if (remainingQuantity > 0)
            {
                result.MissingCards.Add($"{remainingQuantity}x {card}");
                result.Statistics.CardsMissing += remainingQuantity;
            }
        }

        // Calculate statistics
        result.Statistics.TotalCardsNeeded = deck.Cards.Sum(c => c.Quantity);
        result.Statistics.CardsFound = result.Movements.Sum(m => m.Quantity);
        result.Statistics.SourcesUsed = sourcesUsed.Count;

        return result;
    }

    private bool CardsMatch(Card card1, Card card2)
    {
        // Match on name and prefer exact set/collector number match
        // For simplicity, we'll match by name for now
        return card1.Name.Equals(card2.Name, StringComparison.OrdinalIgnoreCase);
    }
}
