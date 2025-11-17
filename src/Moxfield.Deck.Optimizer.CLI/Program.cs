using Moxfield.Deck.Optimizer.Core.Configuration;
using Moxfield.Deck.Optimizer.Core.Models;
using Moxfield.Deck.Optimizer.Core.Services;
using Newtonsoft.Json;

namespace Moxfield.Deck.Optimizer.CLI;

class Program
{
    static async Task<int> Main(string[] args)
    {
        Console.WriteLine("🎯 Moxfield Deck Optimizer");
        Console.WriteLine("═══════════════════════════");
        Console.WriteLine();

        if (args.Length == 0 || args[0] == "demo")
        {
            var outputFile = args.Length > 1 ? args[1] : "demo-optimization.xlsx";
            RunDemo(outputFile);
            return 0;
        }
        else if (args[0] == "optimize")
        {
            if (args.Length < 3)
            {
                ShowUsage();
                return 1;
            }

            var deckId = args[1];
            var collectionsFile = args[2];
            var outputFile = args.Length > 3 ? args[3] : "deck-optimization.xlsx";

            await OptimizeDeckAsync(deckId, collectionsFile, outputFile);
            return 0;
        }
        else if (args[0] == "help" || args[0] == "--help" || args[0] == "-h")
        {
            ShowUsage();
            return 0;
        }
        else
        {
            Console.WriteLine($"Unknown command: {args[0]}");
            Console.WriteLine();
            ShowUsage();
            return 1;
        }
    }

    static void ShowUsage()
    {
        Console.WriteLine("Usage:");
        Console.WriteLine("  demo [output-file]");
        Console.WriteLine("    Run with sample data to see how the tool works");
        Console.WriteLine("    Default output: demo-optimization.xlsx");
        Console.WriteLine();
        Console.WriteLine("  optimize <deck-id> <collections-file> [output-file]");
        Console.WriteLine("    Optimize card selection for a Moxfield deck");
        Console.WriteLine("    deck-id: Moxfield deck ID from the URL");
        Console.WriteLine("    collections-file: Path to JSON file with your collections");
        Console.WriteLine("    output-file: Optional, default is deck-optimization.xlsx");
        Console.WriteLine();
        Console.WriteLine("  help");
        Console.WriteLine("    Show this help message");
        Console.WriteLine();
        Console.WriteLine("Examples:");
        Console.WriteLine("  dotnet run demo");
        Console.WriteLine("  dotnet run optimize abc123 my-collections.json");
        Console.WriteLine("  dotnet run optimize abc123 my-collections.json output.xlsx");
    }

    static async Task OptimizeDeckAsync(string deckId, string collectionsFile, string outputFile)
    {
        try
        {

            // Load collections from file
            Console.WriteLine($"📂 Loading collections from {collectionsFile}...");
            if (!File.Exists(collectionsFile))
            {
                Console.WriteLine($"❌ Error: Collections file not found: {collectionsFile}");
                Console.WriteLine("💡 Tip: Run 'demo' command to see sample data format");
                return;
            }

            var collectionsJson = await File.ReadAllTextAsync(collectionsFile);
            var collections = JsonConvert.DeserializeObject<List<Collection>>(collectionsJson);
            
            if (collections == null || collections.Count == 0)
            {
                Console.WriteLine("❌ Error: No collections found in file");
                return;
            }

            Console.WriteLine($"✓ Loaded {collections.Count} collection(s)");
            Console.WriteLine();

            // Fetch deck from Moxfield
            Console.WriteLine($"🌐 Fetching deck {deckId} from Moxfield...");
            var client = new MoxfieldClient();
            var deck = await client.GetDeckAsync(deckId);
            Console.WriteLine($"✓ Loaded deck: {deck.Name}");
            Console.WriteLine($"  Cards in deck: {deck.Cards.Count}");
            Console.WriteLine();

            // Optimize
            Console.WriteLine("⚙️  Optimizing card selection...");
            var optimizer = new CardOptimizer();
            var result = optimizer.Optimize(deck, collections);
            
            Console.WriteLine($"✓ Optimization complete!");
            Console.WriteLine();

            // Display results
            Console.WriteLine("📊 Results:");
            Console.WriteLine($"  Total cards needed: {result.Statistics.TotalCardsNeeded}");
            Console.WriteLine($"  Cards found: {result.Statistics.CardsFound}");
            Console.WriteLine($"  Cards missing: {result.Statistics.CardsMissing}");
            Console.WriteLine($"  Sources used: {result.Statistics.SourcesUsed}");
            Console.WriteLine();

            if (result.Statistics.CardsPerSource.Any())
            {
                Console.WriteLine("  Cards per source:");
                foreach (var kvp in result.Statistics.CardsPerSource.OrderByDescending(x => x.Value))
                {
                    Console.WriteLine($"    • {kvp.Key}: {kvp.Value} cards");
                }
                Console.WriteLine();
            }

            if (result.MissingCards.Any())
            {
                Console.WriteLine("⚠️  Missing cards:");
                foreach (var missing in result.MissingCards)
                {
                    Console.WriteLine($"    • {missing}");
                }
                Console.WriteLine();
            }

            // Export to Excel
            Console.WriteLine($"📄 Exporting to {outputFile}...");
            var exporter = new ExcelExporter();
            exporter.Export(result, outputFile);
            
            Console.WriteLine($"✓ Export complete!");
            Console.WriteLine();
            Console.WriteLine($"🎉 Done! Open {outputFile} to view your optimized card list.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"   Details: {ex.InnerException.Message}");
            }
        }
    }

    static void RunDemo(string outputFile)
    {
        Console.WriteLine("Demo Mode");
        Console.WriteLine("─────────");
        Console.WriteLine("Generating sample data...");
        Console.WriteLine();

        // Create sample collections
        var collections = new List<Collection>
        {
            new Collection
            {
                Id = "1",
                Name = "Main Collection",
                Priority = 1,
                Cards = new List<CardInCollection>
                {
                    new CardInCollection 
                    { 
                        Card = new Card { Name = "Sol Ring", SetCode = "2XM", CollectorNumber = "331" },
                        Quantity = 3,
                        CollectionName = "Main Collection"
                    },
                    new CardInCollection 
                    { 
                        Card = new Card { Name = "Command Tower", SetCode = "CMR", CollectorNumber = "350" },
                        Quantity = 5,
                        CollectionName = "Main Collection"
                    },
                    new CardInCollection 
                    { 
                        Card = new Card { Name = "Lightning Bolt", SetCode = "M11", CollectorNumber = "149" },
                        Quantity = 10,
                        CollectionName = "Main Collection"
                    }
                }
            },
            new Collection
            {
                Id = "2",
                Name = "Budget Box",
                Priority = 2,
                Cards = new List<CardInCollection>
                {
                    new CardInCollection 
                    { 
                        Card = new Card { Name = "Evolving Wilds", SetCode = "C21", CollectorNumber = "291" },
                        Quantity = 15,
                        CollectionName = "Budget Box"
                    },
                    new CardInCollection 
                    { 
                        Card = new Card { Name = "Terramorphic Expanse", SetCode = "C21", CollectorNumber = "324" },
                        Quantity = 12,
                        CollectionName = "Budget Box"
                    }
                }
            },
            new Collection
            {
                Id = "3",
                Name = "Premium Collection",
                Priority = 3,
                Cards = new List<CardInCollection>
                {
                    new CardInCollection 
                    { 
                        Card = new Card { Name = "Mana Crypt", SetCode = "2X2", CollectorNumber = "308", IsFoil = true },
                        Quantity = 1,
                        CollectionName = "Premium Collection"
                    },
                    new CardInCollection 
                    { 
                        Card = new Card { Name = "Vampiric Tutor", SetCode = "CMR", CollectorNumber = "156" },
                        Quantity = 1,
                        CollectionName = "Premium Collection"
                    }
                }
            }
        };

        // Create sample deck
        var deck = new DeckList
        {
            Id = "demo-deck",
            Name = "Demo Commander Deck",
            Cards = new List<DeckCard>
            {
                new DeckCard { Card = new Card { Name = "Sol Ring" }, Quantity = 1, Board = "mainboard" },
                new DeckCard { Card = new Card { Name = "Command Tower" }, Quantity = 1, Board = "mainboard" },
                new DeckCard { Card = new Card { Name = "Lightning Bolt" }, Quantity = 1, Board = "mainboard" },
                new DeckCard { Card = new Card { Name = "Evolving Wilds" }, Quantity = 1, Board = "mainboard" },
                new DeckCard { Card = new Card { Name = "Terramorphic Expanse" }, Quantity = 1, Board = "mainboard" },
                new DeckCard { Card = new Card { Name = "Mana Crypt" }, Quantity = 1, Board = "mainboard" },
                new DeckCard { Card = new Card { Name = "Force of Will" }, Quantity = 1, Board = "mainboard" }
            }
        };

        // Optimize
        Console.WriteLine("⚙️  Running optimization...");
        var optimizer = new CardOptimizer();
        var result = optimizer.Optimize(deck, collections);
        
        Console.WriteLine("✓ Optimization complete!");
        Console.WriteLine();

        // Display results
        Console.WriteLine("📊 Results:");
        Console.WriteLine($"  Total cards needed: {result.Statistics.TotalCardsNeeded}");
        Console.WriteLine($"  Cards found: {result.Statistics.CardsFound}");
        Console.WriteLine($"  Cards missing: {result.Statistics.CardsMissing}");
        Console.WriteLine($"  Sources used: {result.Statistics.SourcesUsed}");
        Console.WriteLine();

        if (result.Statistics.CardsPerSource.Any())
        {
            Console.WriteLine("  Cards per source:");
            foreach (var kvp in result.Statistics.CardsPerSource.OrderByDescending(x => x.Value))
            {
                Console.WriteLine($"    • {kvp.Key}: {kvp.Value} cards");
            }
            Console.WriteLine();
        }

        if (result.MissingCards.Any())
        {
            Console.WriteLine("⚠️  Missing cards:");
            foreach (var missing in result.MissingCards)
            {
                Console.WriteLine($"    • {missing}");
            }
            Console.WriteLine();
        }

        // Export to Excel
        Console.WriteLine($"📄 Exporting to {outputFile}...");
        var exporter = new ExcelExporter();
        exporter.Export(result, outputFile);
        
        Console.WriteLine("✓ Export complete!");
        Console.WriteLine();

        // Save sample collections file
        var sampleCollectionsFile = "sample-collections.json";
        Console.WriteLine($"💾 Saving sample collections to {sampleCollectionsFile}...");
        var json = JsonConvert.SerializeObject(collections, Formatting.Indented);
        File.WriteAllText(sampleCollectionsFile, json);
        Console.WriteLine("✓ Sample file saved!");
        Console.WriteLine();

        Console.WriteLine($"🎉 Demo complete!");
        Console.WriteLine();
        Console.WriteLine($"📖 Next steps:");
        Console.WriteLine($"  1. Open {outputFile} to see the optimization results");
        Console.WriteLine($"  2. Review {sampleCollectionsFile} to understand the collections format");
        Console.WriteLine($"  3. Create your own collections file and use 'optimize' command");
    }
}
