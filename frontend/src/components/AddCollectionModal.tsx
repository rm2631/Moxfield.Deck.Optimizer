import { useState } from 'react';
import { X, Loader2 } from 'lucide-react';
import { isValidMoxfieldUrl, extractCollectionName } from '../utils/moxfield';

interface AddCollectionModalProps {
  isOpen: boolean;
  onClose: () => void;
  onAdd: (url: string, name: string) => void;
}

export function AddCollectionModal({ isOpen, onClose, onAdd }: AddCollectionModalProps) {
  const [url, setUrl] = useState('');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);

  if (!isOpen) return null;

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');
    
    if (!url.trim()) {
      setError('Please enter a URL');
      return;
    }
    
    if (!isValidMoxfieldUrl(url)) {
      setError('Please enter a valid Moxfield deck URL');
      return;
    }
    
    const name = extractCollectionName(url);
    if (!name) {
      setError('Could not extract collection name from URL');
      return;
    }
    
    setLoading(true);
    try {
      // Simulate fetching collection data
      await new Promise(resolve => setTimeout(resolve, 500));
      onAdd(url, `Collection: ${name}`);
      setUrl('');
      setError('');
    } catch (err) {
      setError('Failed to fetch collection data');
    } finally {
      setLoading(false);
    }
  };

  const handleClose = () => {
    setUrl('');
    setError('');
    onClose();
  };

  return (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50 p-4">
      <div className="bg-white rounded-lg shadow-xl max-w-md w-full">
        <div className="flex items-center justify-between p-4 border-b">
          <h2 className="text-xl font-semibold">Add Collection</h2>
          <button
            onClick={handleClose}
            className="p-1 hover:bg-gray-100 rounded-lg transition-colors"
          >
            <X size={20} />
          </button>
        </div>
        
        <form onSubmit={handleSubmit} className="p-4">
          <div className="mb-4">
            <label htmlFor="url" className="block text-sm font-medium text-gray-700 mb-2">
              Moxfield URL
            </label>
            <input
              type="text"
              id="url"
              value={url}
              onChange={(e) => setUrl(e.target.value)}
              placeholder="https://www.moxfield.com/decks/..."
              className="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
              disabled={loading}
            />
            {error && (
              <p className="mt-2 text-sm text-red-600">{error}</p>
            )}
          </div>
          
          <div className="flex gap-3 justify-end">
            <button
              type="button"
              onClick={handleClose}
              className="px-4 py-2 text-gray-700 bg-gray-100 hover:bg-gray-200 rounded-lg transition-colors"
              disabled={loading}
            >
              Cancel
            </button>
            <button
              type="submit"
              className="px-4 py-2 bg-blue-600 text-white hover:bg-blue-700 rounded-lg transition-colors flex items-center gap-2"
              disabled={loading}
            >
              {loading && <Loader2 size={16} className="animate-spin" />}
              Add Collection
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
