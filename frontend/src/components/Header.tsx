import { Save, RotateCcw } from 'lucide-react';

interface HeaderProps {
  onSaveConfig: () => void;
  onReset: () => void;
}

export function Header({ onSaveConfig, onReset }: HeaderProps) {
  return (
    <header className="bg-blue-600 text-white shadow-lg">
      <div className="container mx-auto px-4 py-6">
        <div className="flex items-center justify-between">
          <div>
            <h1 className="text-3xl font-bold">EDH Deck Optimizer</h1>
            <p className="text-blue-100 mt-1">Optimize your Moxfield card movements</p>
          </div>
          <div className="flex gap-3">
            <button
              onClick={onSaveConfig}
              className="flex items-center gap-2 px-4 py-2 bg-blue-700 hover:bg-blue-800 rounded-lg transition-colors"
              title="Save Configuration"
            >
              <Save size={18} />
              <span className="hidden sm:inline">Save Config</span>
            </button>
            <button
              onClick={onReset}
              className="flex items-center gap-2 px-4 py-2 bg-red-600 hover:bg-red-700 rounded-lg transition-colors"
              title="Reset All"
            >
              <RotateCcw size={18} />
              <span className="hidden sm:inline">Reset</span>
            </button>
          </div>
        </div>
      </div>
    </header>
  );
}
