import * as XLSX from 'xlsx';
import type { Movement } from '../types';

/**
 * Generates and downloads an Excel file with card movements
 * @param movements - Array of card movements
 * @param filename - Name of the file to download (default: 'card-movements.xlsx')
 */
export function exportToExcel(movements: Movement[], filename = 'card-movements.xlsx'): void {
  // Create worksheet data
  const wsData = [
    // Header row
    ['Card Name', 'Source Collection', 'Target Collection', 'Quantity', 'Set Code', 'Collector Number', 'Foil'],
    // Data rows
    ...movements.map(m => [
      m.cardName,
      m.source,
      m.target,
      m.quantity,
      m.setCode || '',
      m.collectorNumber || '',
      m.foil ? 'Yes' : 'No',
    ]),
  ];
  
  // Create workbook and worksheet
  const wb = XLSX.utils.book_new();
  const ws = XLSX.utils.aoa_to_sheet(wsData);
  
  // Set column widths
  ws['!cols'] = [
    { wch: 30 }, // Card Name
    { wch: 25 }, // Source
    { wch: 25 }, // Target
    { wch: 10 }, // Quantity
    { wch: 10 }, // Set Code
    { wch: 15 }, // Collector Number
    { wch: 8 },  // Foil
  ];
  
  // Add worksheet to workbook
  XLSX.utils.book_append_sheet(wb, ws, 'Card Movements');
  
  // Generate and download file
  XLSX.writeFile(wb, filename);
}
