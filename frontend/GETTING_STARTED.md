# Getting Started with EDH Deck Optimizer

Welcome! This guide will help you get the EDH Deck Optimizer running on your machine and deployed to the web.

## Quick Start (5 minutes)

### Prerequisites

- **Node.js 18+** (20+ recommended) - [Download here](https://nodejs.org/)
- **npm** (comes with Node.js) or **yarn**
- A code editor (VS Code recommended)

### Installation

1. **Clone the repository** (if not already done):
   ```bash
   git clone https://github.com/rm2631/Moxfield.Deck.Optimizer.git
   cd Moxfield.Deck.Optimizer/frontend
   ```

2. **Install dependencies**:
   ```bash
   npm install
   ```

3. **Start the development server**:
   ```bash
   npm run dev
   ```

4. **Open your browser** to `http://localhost:5173`

That's it! You should see the application running.

## Development Workflow

### Running the App

```bash
npm run dev
```

The app will automatically reload when you make changes to the code.

### Building for Production

```bash
npm run build
```

This creates an optimized build in the `dist/` folder.

### Preview Production Build

```bash
npm run preview
```

### Linting

```bash
npm run lint
```

## Project Structure

```
frontend/
├── src/
│   ├── components/          # React components
│   │   ├── Header.tsx       # Top navigation bar
│   │   ├── CollectionCard.tsx    # Display individual collections
│   │   ├── AddCollectionModal.tsx # Modal for adding collections
│   │   ├── EditCollectionModal.tsx # Modal for editing collections
│   │   └── ResultsModal.tsx      # Modal for showing results
│   ├── utils/              # Helper functions
│   │   ├── moxfield.ts     # Moxfield API utilities
│   │   ├── optimizer.ts    # Optimization algorithm
│   │   └── excel.ts        # Excel file generation
│   ├── types.ts            # TypeScript types
│   ├── App.tsx             # Main app component
│   ├── main.tsx            # Entry point
│   └── index.css           # Global styles (Tailwind)
├── public/                 # Static assets
├── index.html              # HTML template
└── package.json            # Dependencies
```

## Using the Application

### Step 1: Add Collections

1. Click the **"Add Collection"** button
2. Paste a Moxfield URL (e.g., `https://www.moxfield.com/decks/abc123`)
3. Click **"Add Collection"**

### Step 2: Configure Collections

1. Click the **edit icon** (pencil) on a collection
2. Set the **priority** (1 = highest, 5 = lowest)
3. Choose the **role**:
   - **Source**: Cards can be taken from this collection
   - **Target**: Cards should be placed into this collection
   - **Both**: Acts as both source and target
4. Toggle **active/inactive** if needed
5. Click **"Save Changes"**

### Step 3: Run Optimization

1. Ensure you have at least one **source** and one **target** collection
2. Click **"Optimize Card Movements"**
3. View the results in the modal

### Step 4: Download Results

1. Click **"Download Excel File"** in the results modal
2. Open the Excel file to see your card movement plan
3. Use it as a checklist while organizing your physical cards!

## Making Changes

### Adding a New Component

1. Create a new file in `src/components/`
2. Export your component:
   ```tsx
   export function MyComponent() {
     return <div>Hello!</div>;
   }
   ```
3. Import and use it in `App.tsx` or another component

### Modifying Styles

The app uses Tailwind CSS. You can:

- Add utility classes directly to JSX elements:
  ```tsx
  <div className="bg-blue-500 text-white p-4 rounded-lg">
    Content
  </div>
  ```

- Modify global styles in `src/index.css`

### Adding New Features

1. Create the UI component
2. Add any necessary types to `src/types.ts`
3. Add utility functions to `src/utils/`
4. Update the main app logic in `src/App.tsx`

## Common Tasks

### Changing Colors

Edit the Tailwind classes in components. Main colors:
- Primary blue: `bg-blue-600`, `text-blue-600`
- Success green: `bg-green-600`, `text-green-600`
- Error red: `bg-red-600`, `text-red-600`

### Adding a New Modal

1. Create a component similar to `AddCollectionModal.tsx`
2. Use the same modal structure with overlay
3. Add state in `App.tsx` to control visibility

### Modifying the Optimization Algorithm

Edit `src/utils/optimizer.ts`. The current implementation is a simple example. You can:
- Add more sophisticated logic
- Fetch real data from Moxfield
- Implement additional constraints

## Troubleshooting

### Port 5173 is already in use

Kill the existing process or use a different port:
```bash
npm run dev -- --port 3000
```

### Build errors with Tailwind

The warnings about `@theme` directives are normal and don't affect functionality. They come from Tailwind CSS v4's new syntax.

### TypeScript errors

Make sure you're using Node.js 18+ and TypeScript 5.9+:
```bash
node --version
npx tsc --version
```

### Module not found errors

Delete `node_modules` and reinstall:
```bash
rm -rf node_modules package-lock.json
npm install
```

## Deploying to Production

### Option 1: Vercel (Recommended)

1. Sign up at [vercel.com](https://vercel.com)
2. Click "New Project"
3. Import your GitHub repository
4. Set root directory to `frontend`
5. Click "Deploy"

Done! Your app is live.

### Option 2: Netlify

1. Sign up at [netlify.com](https://netlify.com)
2. Click "Add new site" → "Import from Git"
3. Select your repository
4. Set base directory to `frontend`
5. Set build command to `npm run build`
6. Set publish directory to `frontend/dist`
7. Click "Deploy"

### Option 3: GitHub Pages

See [../DEPLOYMENT.md](../DEPLOYMENT.md) for detailed instructions.

## Learning Resources

### React
- [React Documentation](https://react.dev)
- [React TypeScript Cheatsheet](https://react-typescript-cheatsheet.netlify.app/)

### TypeScript
- [TypeScript Documentation](https://www.typescriptlang.org/docs/)
- [TypeScript Handbook](https://www.typescriptlang.org/docs/handbook/intro.html)

### Tailwind CSS
- [Tailwind Documentation](https://tailwindcss.com/docs)
- [Tailwind UI Components](https://tailwindui.com/)

### Vite
- [Vite Documentation](https://vitejs.dev/)
- [Vite Guide](https://vitejs.dev/guide/)

## Need Help?

- Check the [main README](../README.md) for project overview
- Review [UX_DESIGN.md](../UX_DESIGN.md) for design decisions
- See [DEPLOYMENT.md](../DEPLOYMENT.md) for deployment options
- Read [SECURITY.md](../SECURITY.md) for security considerations

## Contributing

We welcome contributions! Please:

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Test thoroughly
5. Submit a pull request

## Next Steps

Now that you have the app running:

1. ✅ Try adding some collections
2. ✅ Experiment with different priorities
3. ✅ Run an optimization
4. ✅ Download the Excel file
5. ✅ Make some code changes
6. ✅ Deploy to Vercel!

Happy coding! 🎉
