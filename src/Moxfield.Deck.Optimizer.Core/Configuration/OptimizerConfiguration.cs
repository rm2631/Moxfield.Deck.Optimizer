namespace Moxfield.Deck.Optimizer.Core.Configuration;

/// <summary>
/// Configuration for the deck optimizer
/// </summary>
public class OptimizerConfiguration
{
    public string MoxfieldUsername { get; set; } = string.Empty;
    public List<CollectionPriority> CollectionPriorities { get; set; } = new();
    public bool PreferNonFoils { get; set; } = true;
    public bool MinimizeSources { get; set; } = true;

    public static OptimizerConfiguration Default => new()
    {
        PreferNonFoils = true,
        MinimizeSources = true
    };
}

/// <summary>
/// Defines the priority for a specific collection
/// </summary>
public class CollectionPriority
{
    public string CollectionName { get; set; } = string.Empty;
    public int Priority { get; set; }

    public CollectionPriority() { }

    public CollectionPriority(string name, int priority)
    {
        CollectionName = name;
        Priority = priority;
    }
}
