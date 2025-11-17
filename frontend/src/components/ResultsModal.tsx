import { X, Download } from 'lucide-react';
import type { OptimizationResult } from '../types';
import { exportToExcel } from '../utils/excel';

interface ResultsModalProps {
  isOpen: boolean;
  result: OptimizationResult | null;
  onClose: () => void;
}

export function ResultsModal({ isOpen, result, onClose }: ResultsModalProps) {
  if (!isOpen || !result) return null;

  const handleDownload = () => {
    exportToExcel(result.movements, 'card-movements.xlsx');
  };

  return (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50 p-4">
      <div className="bg-white rounded-lg shadow-xl max-w-4xl w-full max-h-[90vh] overflow-hidden flex flex-col">
        <div className="flex items-center justify-between p-4 border-b">
          <h2 className="text-xl font-semibold">Optimization Results</h2>
          <button
            onClick={onClose}
            className="p-1 hover:bg-gray-100 rounded-lg transition-colors"
          >
            <X size={20} />
          </button>
        </div>
        
        <div className="p-6 overflow-y-auto flex-1">
          {/* Summary Stats */}
          <div className="grid grid-cols-1 sm:grid-cols-3 gap-4 mb-6">
            <div className="bg-blue-50 border border-blue-200 rounded-lg p-4">
              <div className="text-2xl font-bold text-blue-900">{result.totalCards}</div>
              <div className="text-sm text-blue-700">Total Cards to Move</div>
            </div>
            <div className="bg-purple-50 border border-purple-200 rounded-lg p-4">
              <div className="text-2xl font-bold text-purple-900">{result.sourcesUsed}</div>
              <div className="text-sm text-purple-700">Source Collections</div>
            </div>
            <div className="bg-teal-50 border border-teal-200 rounded-lg p-4">
              <div className="text-2xl font-bold text-teal-900">{result.targetsUsed}</div>
              <div className="text-sm text-teal-700">Target Collections</div>
            </div>
          </div>
          
          {/* Movements Preview */}
          <div>
            <h3 className="text-lg font-semibold mb-3">Card Movements</h3>
            <div className="overflow-x-auto border rounded-lg">
              <table className="min-w-full divide-y divide-gray-200">
                <thead className="bg-gray-50">
                  <tr>
                    <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                      Card Name
                    </th>
                    <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                      Source
                    </th>
                    <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                      Target
                    </th>
                    <th className="px-4 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                      Qty
                    </th>
                  </tr>
                </thead>
                <tbody className="bg-white divide-y divide-gray-200">
                  {result.movements.slice(0, 20).map((movement, index) => (
                    <tr key={index} className="hover:bg-gray-50">
                      <td className="px-4 py-3 text-sm text-gray-900">{movement.cardName}</td>
                      <td className="px-4 py-3 text-sm text-gray-600">{movement.source}</td>
                      <td className="px-4 py-3 text-sm text-gray-600">{movement.target}</td>
                      <td className="px-4 py-3 text-sm text-gray-900">{movement.quantity}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
            {result.movements.length > 20 && (
              <p className="text-sm text-gray-500 mt-2 text-center">
                Showing first 20 of {result.movements.length} movements. Download Excel file for complete list.
              </p>
            )}
          </div>
        </div>
        
        <div className="p-4 border-t bg-gray-50 flex justify-end gap-3">
          <button
            onClick={onClose}
            className="px-4 py-2 text-gray-700 bg-white border border-gray-300 hover:bg-gray-100 rounded-lg transition-colors"
          >
            Close
          </button>
          <button
            onClick={handleDownload}
            className="px-4 py-2 bg-green-600 text-white hover:bg-green-700 rounded-lg transition-colors flex items-center gap-2"
          >
            <Download size={18} />
            Download Excel File
          </button>
        </div>
      </div>
    </div>
  );
}
