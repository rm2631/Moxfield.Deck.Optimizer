# Examples

This directory contains example files to help you get started with the Moxfield Deck Optimizer.

## Files

### example-collections.json

A sample collections file showing the proper format for defining your card collections.

**Structure:**
- Each collection has a name, ID, and priority level
- Lower priority numbers are checked first (Priority 1 before Priority 2)
- Cards within each collection include:
  - Card name
  - Set code
  - Collector number
  - Foil status
  - Quantity available

**Usage:**
```bash
dotnet run --project src/Moxfield.Deck.Optimizer.CLI optimize <deck-id> examples/example-collections.json
```

## Creating Your Own Collections File

1. **Start with the example:** Copy `example-collections.json` as a template
2. **Set priorities:** Assign priority numbers based on which collections you want to pull from first
   - Priority 1: Most accessible cards (e.g., cards already sorted and ready)
   - Priority 2: Semi-organized cards (e.g., bulk boxes)
   - Priority 3: Premium cards (e.g., expensive cards you use sparingly)
3. **Add your cards:** For each collection, list the cards you have available
4. **Run the optimizer:** Use your collections file with a Moxfield deck ID

## Tips

- **Priority Strategy:** Lower priority numbers are better. Set your most accessible collection to Priority 1.
- **Organization:** Match your collections to your physical storage (e.g., "Main Binder", "Budget Box", "Trade Binder")
- **Quantities:** Be accurate with quantities to avoid situations where the optimizer suggests cards you don't actually have
- **Multiple Copies:** If you have the same card in multiple collections, list it in each one - the optimizer will pull from the highest priority source first

## Example Workflow

1. Export or manually create your collections JSON file
2. Find a deck on Moxfield you want to build
3. Copy the deck ID from the URL (e.g., `https://www.moxfield.com/decks/abc123` → deck ID is `abc123`)
4. Run the optimizer:
   ```bash
   dotnet run --project src/Moxfield.Deck.Optimizer.CLI optimize abc123 my-collections.json output.xlsx
   ```
5. Open `output.xlsx` to see exactly which cards to pull from which collections
6. Build your deck efficiently!
