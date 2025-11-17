import type { Collection } from '../types';
import { Pencil, Trash2, ExternalLink } from 'lucide-react';

interface CollectionCardProps {
  collection: Collection;
  onEdit: (collection: Collection) => void;
  onDelete: (id: string) => void;
  onToggleActive: (id: string) => void;
}

export function CollectionCard({ collection, onEdit, onDelete, onToggleActive }: CollectionCardProps) {
  const priorityColors = {
    1: 'bg-red-100 text-red-800 border-red-300',
    2: 'bg-orange-100 text-orange-800 border-orange-300',
    3: 'bg-yellow-100 text-yellow-800 border-yellow-300',
    4: 'bg-green-100 text-green-800 border-green-300',
    5: 'bg-blue-100 text-blue-800 border-blue-300',
  };

  const roleColors = {
    source: 'bg-purple-100 text-purple-800',
    target: 'bg-teal-100 text-teal-800',
    both: 'bg-indigo-100 text-indigo-800',
  };

  return (
    <div className={`border-2 rounded-lg p-4 transition-all ${
      collection.active 
        ? 'bg-white border-gray-200 shadow-sm hover:shadow-md' 
        : 'bg-gray-50 border-gray-300 opacity-60'
    }`}>
      <div className="flex items-start justify-between gap-3">
        <div className="flex-1 min-w-0">
          <div className="flex items-center gap-2 mb-2">
            <input
              type="checkbox"
              checked={collection.active}
              onChange={() => onToggleActive(collection.id)}
              className="w-4 h-4 text-blue-600 rounded focus:ring-2 focus:ring-blue-500"
            />
            <h3 className="font-semibold text-gray-900 truncate">{collection.name}</h3>
          </div>
          
          <a
            href={collection.url}
            target="_blank"
            rel="noopener noreferrer"
            className="text-sm text-blue-600 hover:text-blue-800 flex items-center gap-1 mb-3 truncate"
          >
            <span className="truncate">{collection.url}</span>
            <ExternalLink size={12} className="flex-shrink-0" />
          </a>
          
          <div className="flex flex-wrap gap-2">
            <span className={`px-3 py-1 rounded-full text-xs font-medium border ${priorityColors[collection.priority as keyof typeof priorityColors]}`}>
              Priority {collection.priority}
            </span>
            <span className={`px-3 py-1 rounded-full text-xs font-medium ${roleColors[collection.role]}`}>
              {collection.role.charAt(0).toUpperCase() + collection.role.slice(1)}
            </span>
          </div>
        </div>
        
        <div className="flex gap-2">
          <button
            onClick={() => onEdit(collection)}
            className="p-2 text-gray-600 hover:text-blue-600 hover:bg-blue-50 rounded-lg transition-colors"
            title="Edit Collection"
          >
            <Pencil size={18} />
          </button>
          <button
            onClick={() => onDelete(collection.id)}
            className="p-2 text-gray-600 hover:text-red-600 hover:bg-red-50 rounded-lg transition-colors"
            title="Delete Collection"
          >
            <Trash2 size={18} />
          </button>
        </div>
      </div>
    </div>
  );
}
