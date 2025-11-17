# EDH Deck Optimizer - Frontend

A TypeScript Single Page Application (SPA) built with React and Vite for optimizing card movements between Moxfield collections.

## Features

- 📦 **Collection Management**: Add and configure your Moxfield collections
- 🎯 **Priority System**: Set priorities (1-5) for source collections
- 🔄 **Role Assignment**: Configure collections as Source, Target, or Both
- ⚡ **Optimization Algorithm**: Minimize collection usage and card movements
- 📊 **Excel Export**: Download detailed movement plans in Excel format
- 💾 **Local Storage**: Save and load your collection configurations
- 📱 **Responsive Design**: Works on desktop, tablet, and mobile devices

## Tech Stack

- **Framework**: React 19 with TypeScript
- **Build Tool**: Vite 7 (with Rolldown)
- **Styling**: Tailwind CSS 4
- **Icons**: Lucide React
- **Excel Generation**: SheetJS (xlsx)

## Getting Started

### Prerequisites

- Node.js 18+ (20+ recommended)
- npm or yarn

### Installation

```bash
npm install
```

### Development

Start the development server:

```bash
npm run dev
```

The app will be available at `http://localhost:5173`

### Building

Build for production:

```bash
npm run build
```

The optimized output will be in the `dist/` directory.

### Preview Production Build

Preview the production build locally:

```bash
npm run preview
```

## Project Structure

```
frontend/
├── src/
│   ├── components/          # React components
│   │   ├── Header.tsx
│   │   ├── CollectionCard.tsx
│   │   ├── AddCollectionModal.tsx
│   │   ├── EditCollectionModal.tsx
│   │   └── ResultsModal.tsx
│   ├── utils/              # Utility functions
│   │   ├── moxfield.ts     # Moxfield API utilities
│   │   ├── optimizer.ts    # Optimization algorithm
│   │   └── excel.ts        # Excel export functionality
│   ├── types.ts            # TypeScript type definitions
│   ├── App.tsx             # Main application component
│   ├── main.tsx            # Application entry point
│   └── index.css           # Global styles (Tailwind)
├── public/                 # Static assets
├── index.html              # HTML template
├── vite.config.ts          # Vite configuration
├── vercel.json             # Vercel deployment config
└── package.json            # Dependencies and scripts
```

## Key Components

### Header
Navigation bar with save/reset functionality

### CollectionCard
Displays individual collection information with edit/delete actions

### AddCollectionModal
Modal for adding new Moxfield collections via URL

### EditCollectionModal
Modal for editing collection priority, role, and active status

### ResultsModal
Displays optimization results and provides Excel download

## Utilities

### Moxfield Utils
- URL validation
- Collection name extraction
- Data fetching (placeholder for future API integration)

### Optimizer
- Collection validation
- Priority-based source selection
- Movement calculation algorithm

### Excel Export
- Generates formatted Excel files
- Includes headers and column sizing
- Uses SheetJS library

## Deployment

See [../DEPLOYMENT.md](../DEPLOYMENT.md) for detailed deployment instructions.

Quick deploy to Vercel:

1. Push to GitHub
2. Import project in Vercel
3. Set root directory to `frontend`
4. Deploy

## Future Enhancements

- [ ] Real Moxfield API integration
- [ ] Advanced filtering options
- [ ] Collection templates
- [ ] History tracking
- [ ] Dark mode
- [ ] Bulk URL import
- [ ] Card visualization
- [ ] Export to CSV/PDF
