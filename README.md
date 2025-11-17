# Moxfield Deck Optimizer

A streamlined tool for assembling EDH (Elder Dragon Highlander/Commander) decks by optimizing card movement between your Moxfield collections. Through priority-based selection, source minimization, and intelligent grouping, it outputs a clean, actionable Excel file that saves time and reduces physical card handling.

## Features

✨ **Priority-Based Selection** - Define collection priorities to pull cards from preferred sources first  
📊 **Source Minimization** - Intelligently groups cards to minimize the number of collections you need to access  
📄 **Excel Output** - Clean, organized Excel spreadsheet with card movements, statistics, and missing cards  
🎯 **Smart Matching** - Finds cards across all your collections with flexible matching  
🚀 **Easy to Use** - Simple command-line interface with demo mode  
🏗️ **Easy to Host** - Self-contained .NET application that runs anywhere

## Quick Start

### Prerequisites

- [.NET SDK 8.0 or later](https://dotnet.microsoft.com/download)

### Installation

1. Clone the repository:
```bash
git clone https://github.com/rm2631/Moxfield.Deck.Optimizer.git
cd Moxfield.Deck.Optimizer
```

2. Build the project:
```bash
dotnet build
```

### Try the Demo

See the optimizer in action with sample data:

```bash
cd src/Moxfield.Deck.Optimizer.CLI
dotnet run demo
```

This will:
- Generate sample collections with common EDH cards
- Create a sample deck
- Run the optimization algorithm
- Export results to `demo-optimization.xlsx`
- Save a sample collections file for reference

## Usage

### Basic Command

```bash
dotnet run optimize <deck-id> <collections-file> [output-file]
```

**Arguments:**
- `deck-id` - The public ID of your Moxfield deck (from the deck URL)
- `collections-file` - Path to your collections JSON file
- `output-file` - (Optional) Output Excel file path (default: `deck-optimization.xlsx`)

### Example

```bash
dotnet run optimize abc123def456 my-collections.json output.xlsx
```

## Collections File Format

Create a JSON file describing your card collections:

```json
[
  {
    "Name": "Main Collection",
    "Id": "1",
    "Priority": 1,
    "Cards": [
      {
        "Card": {
          "Name": "Sol Ring",
          "SetCode": "2XM",
          "CollectorNumber": "331",
          "IsFoil": false
        },
        "Quantity": 3,
        "CollectionName": "Main Collection"
      }
    ]
  },
  {
    "Name": "Budget Box",
    "Id": "2",
    "Priority": 2,
    "Cards": [
      {
        "Card": {
          "Name": "Command Tower",
          "SetCode": "CMR",
          "CollectorNumber": "350"
        },
        "Quantity": 5,
        "CollectionName": "Budget Box"
      }
    ]
  }
]
```

**Priority System:** Lower numbers = higher priority. The optimizer will try to pull cards from Priority 1 collections first, then Priority 2, etc.

## Output

The tool generates an Excel file with three sheets:

### 1. Card Movements
Lists all cards to pull from your collections, organized by source and priority:
- Card name
- Quantity needed
- Source collection
- Priority level
- Board location (mainboard, sideboard, commander)
- Set code and collector number
- Foil status

### 2. Statistics
Summary of the optimization:
- Total cards needed vs found
- Number of missing cards
- Number of sources used
- Cards per source breakdown

### 3. Missing Cards
Lists any cards that couldn't be found in your collections

## How It Works

1. **Load Collections** - Reads your collection data from the JSON file
2. **Fetch Deck** - Retrieves the deck list from Moxfield's public API
3. **Optimize** - For each card in the deck:
   - Searches collections in priority order
   - Allocates cards from highest priority source first
   - Moves to next priority source if needed
   - Tracks missing cards
4. **Group & Export** - Groups movements by source and exports to Excel

## Project Structure

```
Moxfield.Deck.Optimizer/
├── src/
│   ├── Moxfield.Deck.Optimizer.Core/    # Core business logic
│   │   ├── Models/                       # Domain models
│   │   ├── Services/                     # Optimization and export services
│   │   └── Configuration/                # Configuration models
│   └── Moxfield.Deck.Optimizer.CLI/     # Command-line interface
└── tests/
    └── Moxfield.Deck.Optimizer.Tests/   # Unit tests
```

## Development

### Building

```bash
dotnet build
```

### Running Tests

```bash
dotnet test
```

### Running from Source

```bash
cd src/Moxfield.Deck.Optimizer.CLI
dotnet run -- demo
dotnet run -- optimize <deck-id> <collections-file>
```

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is provided as-is for personal, non-commercial use.

## Acknowledgments

- Built with [EPPlus](https://epplussoftware.com/en/Developers) for Excel generation
- Integrates with [Moxfield](https://www.moxfield.com/) public API
- Designed for the Magic: The Gathering EDH/Commander community

## Support

For issues, questions, or suggestions, please open an issue on GitHub.

---

**Note:** This tool works with Moxfield's public deck API. For collection data, you'll need to manually export or create your collections file. The tool does not require API authentication and works entirely with public deck data.