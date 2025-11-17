using Moxfield.Deck.Optimizer.Core.Models;

namespace Moxfield.Deck.Optimizer.Core.Services;

/// <summary>
/// Interface for interacting with the Moxfield API
/// </summary>
public interface IMoxfieldClient
{
    /// <summary>
    /// Gets a deck by its public ID
    /// </summary>
    Task<DeckList> GetDeckAsync(string deckId);

    /// <summary>
    /// Gets collections for a user
    /// </summary>
    Task<List<Collection>> GetCollectionsAsync(string username);
}
