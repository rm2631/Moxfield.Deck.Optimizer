using Moxfield.Deck.Optimizer.Core.Models;

namespace Moxfield.Deck.Optimizer.Core.Services;

/// <summary>
/// Interface for optimizing card selection from collections
/// </summary>
public interface ICardOptimizer
{
    /// <summary>
    /// Optimizes card selection for a deck from available collections
    /// </summary>
    /// <param name="deck">The deck to build</param>
    /// <param name="collections">Available collections to pull from</param>
    /// <returns>Optimization result with card movements</returns>
    OptimizationResult Optimize(DeckList deck, List<Collection> collections);
}
