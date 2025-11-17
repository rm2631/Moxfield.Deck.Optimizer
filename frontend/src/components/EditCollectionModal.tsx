import { useState } from 'react';
import { X } from 'lucide-react';
import type { Collection } from '../types';

interface EditCollectionModalProps {
  isOpen: boolean;
  collection: Collection | null;
  onClose: () => void;
  onSave: (collection: Collection) => void;
}

function EditForm({ collection, onClose, onSave }: { collection: Collection; onClose: () => void; onSave: (collection: Collection) => void }) {
  const [priority, setPriority] = useState(collection.priority);
  const [role, setRole] = useState<'source' | 'target' | 'both'>(collection.role);
  const [active, setActive] = useState(collection.active);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSave({
      ...collection,
      priority,
      role,
      active,
    });
  };

  return (
    <form onSubmit={handleSubmit} className="p-4 space-y-4">
          <div>
            <h3 className="font-medium text-gray-900 mb-1">{collection.name}</h3>
            <p className="text-sm text-gray-500 truncate">{collection.url}</p>
          </div>
          
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-2">
              Priority: {priority} {priority === 1 ? '(Highest)' : priority === 5 ? '(Lowest)' : ''}
            </label>
            <input
              type="range"
              min="1"
              max="5"
              step="1"
              value={priority}
              onChange={(e) => setPriority(Number(e.target.value))}
              className="w-full h-2 bg-gray-200 rounded-lg appearance-none cursor-pointer"
            />
            <div className="flex justify-between text-xs text-gray-500 mt-1">
              <span>1</span>
              <span>2</span>
              <span>3</span>
              <span>4</span>
              <span>5</span>
            </div>
          </div>
          
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-2">
              Role
            </label>
            <div className="space-y-2">
              <label className="flex items-center">
                <input
                  type="radio"
                  value="source"
                  checked={role === 'source'}
                  onChange={(e) => setRole(e.target.value as 'source')}
                  className="mr-2"
                />
                <span>Source (cards can be taken from it)</span>
              </label>
              <label className="flex items-center">
                <input
                  type="radio"
                  value="target"
                  checked={role === 'target'}
                  onChange={(e) => setRole(e.target.value as 'target')}
                  className="mr-2"
                />
                <span>Target (cards should be placed into it)</span>
              </label>
              <label className="flex items-center">
                <input
                  type="radio"
                  value="both"
                  checked={role === 'both'}
                  onChange={(e) => setRole(e.target.value as 'both')}
                  className="mr-2"
                />
                <span>Both (source and target)</span>
              </label>
            </div>
          </div>
          
          <div>
            <label className="flex items-center">
              <input
                type="checkbox"
                checked={active}
                onChange={(e) => setActive(e.target.checked)}
                className="mr-2 w-4 h-4 text-blue-600 rounded focus:ring-2 focus:ring-blue-500"
              />
              <span className="text-sm font-medium text-gray-700">Active</span>
            </label>
          </div>
          
          <div className="flex gap-3 justify-end pt-4 border-t">
            <button
              type="button"
              onClick={onClose}
              className="px-4 py-2 text-gray-700 bg-gray-100 hover:bg-gray-200 rounded-lg transition-colors"
            >
              Cancel
            </button>
            <button
              type="submit"
              className="px-4 py-2 bg-blue-600 text-white hover:bg-blue-700 rounded-lg transition-colors"
            >
              Save Changes
            </button>
          </div>
        </form>
    );
}

export function EditCollectionModal({ isOpen, collection, onClose, onSave }: EditCollectionModalProps) {
  if (!isOpen || !collection) return null;

  return (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50 p-4">
      <div className="bg-white rounded-lg shadow-xl max-w-md w-full">
        <div className="flex items-center justify-between p-4 border-b">
          <h2 className="text-xl font-semibold">Edit Collection</h2>
          <button
            onClick={onClose}
            className="p-1 hover:bg-gray-100 rounded-lg transition-colors"
          >
            <X size={20} />
          </button>
        </div>
        
        <EditForm key={collection.id} collection={collection} onClose={onClose} onSave={onSave} />
      </div>
    </div>
  );
}
