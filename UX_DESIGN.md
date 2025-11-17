# UX Design Document
## EDH Deck Builder Optimizer

### 1. Overview

This document outlines the User Experience (UX) design for the EDH Deck Builder Optimizer web application. The application is a TypeScript Single Page Application (SPA) designed to help users optimize card movement between their Moxfield collections when building EDH decks.

### 2. Design Principles

1. **Simplicity First**: The interface should be intuitive and require minimal learning
2. **Progressive Disclosure**: Show basic options first, advanced features as needed
3. **Immediate Feedback**: Provide clear status updates during data fetching and processing
4. **Mobile-Responsive**: Support both desktop and mobile devices
5. **Performance**: Fast load times and responsive interactions

### 3. Technology Stack

#### Frontend
- **Framework**: React 18+ with TypeScript
- **Build Tool**: Vite (fast builds, excellent DX)
- **UI Library**: Tailwind CSS (utility-first, small bundle)
- **Component Library**: shadcn/ui (accessible, customizable components)
- **State Management**: React Context + Hooks (for simplicity)
- **Data Fetching**: Fetch API with custom hooks

#### Deployment
- **Platform**: Vercel (free tier, zero config, excellent DX)
- **Alternative**: Netlify or GitHub Pages
- **Cost**: $0 (free tier sufficient for this use case)
- **Setup**: None required (deploy via Git integration)

#### Backend Strategy
- **Phase 1 (MVP)**: Client-side only processing
  - Use Moxfield public API if available
  - Process optimization algorithm in the browser
  - Generate Excel files client-side using SheetJS/xlsx library
  
- **Phase 2 (Optional)**: Serverless functions
  - Vercel Edge Functions or Netlify Functions for API proxying
  - Still free tier compatible
  - Provides CORS bypass and caching if needed

### 4. User Flows

#### 4.1 Primary User Flow

```
1. Landing Page
   ↓
2. Add Collections (Input Moxfield URLs)
   ↓
3. Configure Collections (Priority, Role, Active/Inactive)
   ↓
4. Run Optimization
   ↓
5. View Results & Download Excel
```

#### 4.2 Alternative Flows

- **Quick Start Flow**: Import bulk URLs via textarea/file
- **Edit Flow**: Modify existing collections before re-running
- **Save/Load Flow**: Save configuration to browser localStorage

### 5. Page Structure

#### 5.1 Main Application (Single Page)

The application uses a single-page layout with distinct sections:

##### Header
- App logo/title: "EDH Deck Optimizer"
- Subtitle: "Optimize your Moxfield card movements"
- Quick actions: Save Config | Load Config | Reset

##### Collection Management Panel (Left/Top)
- **Add Collection Button**: Prominent CTA
- **URL Input Modal**:
  - Input field for Moxfield URL
  - "Add" and "Cancel" buttons
  - Auto-fetch collection name on URL paste
  - Validation feedback
  
- **Collection List**:
  - Cards showing each collection
  - Display: Name, URL (truncated), Priority, Role, Active status
  - Actions per collection:
    - Edit (pencil icon)
    - Delete (trash icon)
    - Toggle Active (checkbox)
  - Drag handles for reordering (nice-to-have)

- **Bulk Import** (collapsed by default):
  - Textarea for multiple URLs (one per line)
  - "Import All" button

##### Configuration Panel (Center)
- **Per-Collection Settings** (shown when collection selected):
  - Priority Slider: 1 (highest) - 5 (lowest)
  - Role Radio Buttons: Source / Target / Both
  - Active Toggle: On/Off
  - Save/Cancel buttons

##### Action Panel (Bottom/Right)
- **Run Optimization Button**: Large, prominent
- **Status Display**:
  - Progress indicator during processing
  - Success/error messages
  - Summary stats (collections processed, cards analyzed, movements found)

##### Results Panel (Modal or Slide-out)
- **Movement Summary**:
  - Total cards to move
  - Number of source collections used
  - Number of target collections
  - Optimization metrics
  
- **Preview Table**:
  - First 10-20 movements
  - Columns: Card Name, Source, Target, Quantity
  
- **Download Button**: "Download Excel File"
- **Actions**: 
  - View Full Report (expand table)
  - Run Again with Different Settings
  - Reset and Start Over

### 6. Component Architecture

```
App
├── Header
│   ├── Logo
│   ├── Title
│   └── QuickActions
│
├── MainLayout
│   ├── CollectionPanel
│   │   ├── AddCollectionButton
│   │   ├── CollectionList
│   │   │   └── CollectionCard (multiple)
│   │   │       ├── CollectionInfo
│   │   │       ├── QuickActions
│   │   │       └── ConfigControls
│   │   └── BulkImport (collapsible)
│   │
│   ├── ActionPanel
│   │   ├── OptimizeButton
│   │   └── StatusDisplay
│   │
│   └── ResultsModal
│       ├── SummaryStats
│       ├── MovementPreview
│       └── DownloadButton
│
└── Footer
    ├── About Link
    ├── GitHub Link
    └── Version Info
```

### 7. Visual Design

#### 7.1 Color Palette

- **Primary**: Blue (#3B82F6) - action buttons, links
- **Secondary**: Purple (#8B5CF6) - accents, highlights
- **Success**: Green (#10B981) - success states
- **Warning**: Yellow (#F59E0B) - warnings
- **Error**: Red (#EF4444) - errors
- **Neutral**: Gray scale (#F3F4F6 to #1F2937) - backgrounds, text

#### 7.2 Typography

- **Font**: Inter or System UI stack
- **Headings**: Bold, larger sizes (text-2xl, text-xl, text-lg)
- **Body**: Regular weight (text-base, text-sm)
- **Monospace**: For URLs, codes

#### 7.3 Spacing & Layout

- **Container**: Max-width 1280px, centered
- **Padding**: Consistent 4px base (p-4, p-6, p-8)
- **Gaps**: Use gap utilities (gap-4, gap-6)
- **Cards**: Rounded corners (rounded-lg), subtle shadows

### 8. User Interactions

#### 8.1 Adding a Collection

1. User clicks "Add Collection" button
2. Modal opens with URL input field focused
3. User pastes Moxfield URL
4. System validates URL format
5. System fetches collection name (loading indicator)
6. System shows preview: "Add [Collection Name]?"
7. User confirms or cancels
8. Collection added to list with default settings (Priority 3, Active, needs role assignment)
9. User sets role (Source/Target/Both) - required before optimization

#### 8.2 Configuring Collections

1. User clicks collection card to expand configuration
2. Inline or side panel shows:
   - Priority slider with labels
   - Role selection (radio buttons or toggle)
   - Active checkbox
3. Changes auto-save (or "Save" button for explicit control)
4. Visual feedback on save

#### 8.3 Running Optimization

1. User clicks "Optimize" button
2. System validates:
   - At least one source collection
   - At least one target collection
   - All active collections have roles assigned
3. If validation fails, show error messages
4. If validation passes:
   - Show loading spinner/progress bar
   - Process optimization algorithm
   - Display results modal on completion
5. User can download Excel file or modify and re-run

#### 8.4 Error Handling

- **Invalid URL**: "Please enter a valid Moxfield URL"
- **Network Error**: "Could not fetch collection. Please check the URL and try again."
- **No Collections**: "Please add at least one source and one target collection"
- **No Movement Needed**: "All target decks already have the required cards!"

### 9. Responsive Design

#### Desktop (≥1024px)
- Side-by-side panels
- Full table views
- Expanded controls

#### Tablet (768px - 1023px)
- Stacked panels
- Collapsible sections
- Scrollable tables

#### Mobile (≤767px)
- Single column layout
- Bottom sheet modals
- Simplified tables (key columns only)
- Hamburger menu for actions

### 10. Accessibility

- **Keyboard Navigation**: All interactive elements keyboard accessible
- **Screen Readers**: Proper ARIA labels and roles
- **Focus Management**: Clear focus indicators
- **Color Contrast**: WCAG AA compliance minimum
- **Alt Text**: Meaningful descriptions for icons

### 11. Performance Considerations

- **Lazy Loading**: Code split by route/feature
- **Optimistic UI**: Immediate feedback before API calls
- **Caching**: Store fetched collections in memory/localStorage
- **Debouncing**: For search and filter inputs
- **Bundle Size**: Target <100KB initial JS bundle

### 12. Data Persistence

- **localStorage**: Save/load collection configurations
- **Session Storage**: Temporary optimization results
- **Export/Import**: JSON configuration files for sharing setups

### 13. Future Enhancements

- **Collection Templates**: Pre-configured collection sets
- **Comparison Mode**: Side-by-side deck comparison
- **History**: View past optimization runs
- **Sharing**: Share configurations via URL
- **Dark Mode**: Toggle theme
- **Advanced Filters**: Filter cards by type, color, CMC
- **Visualization**: Graph view of card movements

### 14. Deployment Configuration

#### Vercel Deployment
- Repository: Connect GitHub repo to Vercel
- Build Command: `npm run build`
- Output Directory: `dist`
- Framework Preset: Vite
- Environment Variables: None needed for MVP
- Custom Domain: Optional (free with Vercel)

#### Alternatives
- **Netlify**: Same configuration, drag-and-drop or Git integration
- **GitHub Pages**: Requires gh-pages branch, manual workflow setup
- **Cloudflare Pages**: Similar to Vercel, good CDN

### 15. Analytics & Monitoring (Optional)

- Vercel Analytics (free tier)
- Simple usage tracking:
  - Collections added
  - Optimizations run
  - Downloads completed
- Error monitoring via Vercel or Sentry free tier

### 16. Success Metrics

- **Usability**: User can complete full flow in <2 minutes
- **Performance**: Initial load <2 seconds, optimization <5 seconds
- **Reliability**: 99% uptime (Vercel SLA)
- **Cost**: $0 monthly operating cost

---

## Implementation Priority

### Phase 1: MVP (Week 1)
✅ Project setup with Vite + React + TypeScript
✅ Basic collection management (add, edit, delete)
✅ Moxfield URL parsing and validation
✅ Simple optimization algorithm (client-side)
✅ Excel export functionality
✅ Deploy to Vercel

### Phase 2: Polish (Week 2)
- Improved UI with Tailwind + shadcn/ui
- Better error handling
- Save/load configurations
- Responsive design refinements

### Phase 3: Enhancements (Week 3+)
- Bulk import
- Advanced filtering
- Optimization history
- Performance optimizations

---

## Conclusion

This UX design prioritizes simplicity, low cost, and ease of deployment while providing a solid foundation for future enhancements. The TypeScript SPA approach with Vercel deployment meets all requirements: zero deployment cost, no server setup, and a modern developer experience.
