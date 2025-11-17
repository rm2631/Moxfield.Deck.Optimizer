import { useState } from 'react';
import { Plus, Play, Loader2, AlertCircle } from 'lucide-react';
import type { Collection, OptimizationResult } from './types';
import { Header } from './components/Header';
import { CollectionCard } from './components/CollectionCard';
import { AddCollectionModal } from './components/AddCollectionModal';
import { EditCollectionModal } from './components/EditCollectionModal';
import { ResultsModal } from './components/ResultsModal';
import { optimizeCardMovements } from './utils/optimizer';

function App() {
  const [collections, setCollections] = useState<Collection[]>([]);
  const [isAddModalOpen, setIsAddModalOpen] = useState(false);
  const [isEditModalOpen, setIsEditModalOpen] = useState(false);
  const [editingCollection, setEditingCollection] = useState<Collection | null>(null);
  const [isResultsModalOpen, setIsResultsModalOpen] = useState(false);
  const [optimizationResult, setOptimizationResult] = useState<OptimizationResult | null>(null);
  const [isOptimizing, setIsOptimizing] = useState(false);
  const [error, setError] = useState('');

  const handleAddCollection = (url: string, name: string) => {
    const newCollection: Collection = {
      id: Date.now().toString(),
      url,
      name,
      priority: 3,
      role: 'source',
      active: true,
    };
    setCollections([...collections, newCollection]);
    setIsAddModalOpen(false);
  };

  const handleEditCollection = (collection: Collection) => {
    setEditingCollection(collection);
    setIsEditModalOpen(true);
  };

  const handleSaveCollection = (updatedCollection: Collection) => {
    setCollections(collections.map(c => 
      c.id === updatedCollection.id ? updatedCollection : c
    ));
    setIsEditModalOpen(false);
    setEditingCollection(null);
  };

  const handleDeleteCollection = (id: string) => {
    if (confirm('Are you sure you want to delete this collection?')) {
      setCollections(collections.filter(c => c.id !== id));
    }
  };

  const handleToggleActive = (id: string) => {
    setCollections(collections.map(c => 
      c.id === id ? { ...c, active: !c.active } : c
    ));
  };

  const handleOptimize = async () => {
    setError('');
    setIsOptimizing(true);
    
    try {
      // Validate we have at least one source and one target
      const activeCollections = collections.filter(c => c.active);
      const sources = activeCollections.filter(c => c.role === 'source' || c.role === 'both');
      const targets = activeCollections.filter(c => c.role === 'target' || c.role === 'both');
      
      if (sources.length === 0) {
        setError('Please add at least one active source collection');
        return;
      }
      
      if (targets.length === 0) {
        setError('Please add at least one active target collection');
        return;
      }
      
      // Simulate processing time
      await new Promise(resolve => setTimeout(resolve, 1000));
      
      const result = optimizeCardMovements(collections);
      setOptimizationResult(result);
      setIsResultsModalOpen(true);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'An error occurred during optimization');
    } finally {
      setIsOptimizing(false);
    }
  };

  const handleSaveConfig = () => {
    localStorage.setItem('collections', JSON.stringify(collections));
    alert('Configuration saved!');
  };

  const handleReset = () => {
    if (confirm('Are you sure you want to reset all collections?')) {
      setCollections([]);
      setError('');
      localStorage.removeItem('collections');
    }
  };

  // Load saved configuration on mount
  useState(() => {
    const saved = localStorage.getItem('collections');
    if (saved) {
      try {
        setCollections(JSON.parse(saved));
      } catch (e) {
        console.error('Failed to load saved configuration', e);
      }
    }
  });

  return (
    <div className="min-h-screen bg-gray-50">
      <Header onSaveConfig={handleSaveConfig} onReset={handleReset} />
      
      <main className="container mx-auto px-4 py-8 max-w-6xl">
        {/* Collections Section */}
        <div className="mb-8">
          <div className="flex items-center justify-between mb-4">
            <h2 className="text-2xl font-bold text-gray-900">Collections</h2>
            <button
              onClick={() => setIsAddModalOpen(true)}
              className="flex items-center gap-2 px-4 py-2 bg-blue-600 text-white hover:bg-blue-700 rounded-lg transition-colors shadow-sm"
            >
              <Plus size={20} />
              Add Collection
            </button>
          </div>
          
          {collections.length === 0 ? (
            <div className="bg-white border-2 border-dashed border-gray-300 rounded-lg p-12 text-center">
              <p className="text-gray-500 mb-4">No collections added yet</p>
              <button
                onClick={() => setIsAddModalOpen(true)}
                className="text-blue-600 hover:text-blue-700 font-medium"
              >
                Add your first collection
              </button>
            </div>
          ) : (
            <div className="grid grid-cols-1 lg:grid-cols-2 gap-4">
              {collections.map(collection => (
                <CollectionCard
                  key={collection.id}
                  collection={collection}
                  onEdit={handleEditCollection}
                  onDelete={handleDeleteCollection}
                  onToggleActive={handleToggleActive}
                />
              ))}
            </div>
          )}
        </div>
        
        {/* Action Section */}
        <div className="bg-white rounded-lg shadow-sm border border-gray-200 p-6">
          <h2 className="text-xl font-bold text-gray-900 mb-4">Run Optimization</h2>
          
          {error && (
            <div className="mb-4 p-4 bg-red-50 border border-red-200 rounded-lg flex items-start gap-3">
              <AlertCircle className="text-red-600 flex-shrink-0 mt-0.5" size={20} />
              <p className="text-red-800">{error}</p>
            </div>
          )}
          
          <p className="text-gray-600 mb-6">
            Click the button below to analyze your collections and generate an optimized card movement plan.
          </p>
          
          <button
            onClick={handleOptimize}
            disabled={isOptimizing || collections.length === 0}
            className="w-full sm:w-auto flex items-center justify-center gap-2 px-6 py-3 bg-green-600 text-white hover:bg-green-700 disabled:bg-gray-400 disabled:cursor-not-allowed rounded-lg transition-colors shadow-sm font-medium"
          >
            {isOptimizing ? (
              <>
                <Loader2 size={20} className="animate-spin" />
                Optimizing...
              </>
            ) : (
              <>
                <Play size={20} />
                Optimize Card Movements
              </>
            )}
          </button>
        </div>
      </main>
      
      {/* Modals */}
      <AddCollectionModal
        isOpen={isAddModalOpen}
        onClose={() => setIsAddModalOpen(false)}
        onAdd={handleAddCollection}
      />
      
      <EditCollectionModal
        isOpen={isEditModalOpen}
        collection={editingCollection}
        onClose={() => {
          setIsEditModalOpen(false);
          setEditingCollection(null);
        }}
        onSave={handleSaveCollection}
      />
      
      <ResultsModal
        isOpen={isResultsModalOpen}
        result={optimizationResult}
        onClose={() => setIsResultsModalOpen(false)}
      />
      
      {/* Footer */}
      <footer className="bg-white border-t border-gray-200 mt-12">
        <div className="container mx-auto px-4 py-6 text-center text-sm text-gray-600">
          <p>EDH Deck Optimizer - Built with React + TypeScript</p>
          <p className="mt-1">
            <a href="https://github.com/rm2631/Moxfield.Deck.Optimizer" className="text-blue-600 hover:text-blue-700">
              View on GitHub
            </a>
          </p>
        </div>
      </footer>
    </div>
  );
}

export default App;
