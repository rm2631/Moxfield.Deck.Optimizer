import type { Collection, Movement, OptimizationResult } from '../types';

/**
 * Simplified optimization algorithm for MVP
 * In production, this would implement the full algorithm described in README
 */
export function optimizeCardMovements(collections: Collection[]): OptimizationResult {
  // Filter active collections
  const activeCollections = collections.filter(c => c.active);
  
  // Separate sources and targets
  const sources = activeCollections.filter(c => c.role === 'source' || c.role === 'both');
  const targets = activeCollections.filter(c => c.role === 'target' || c.role === 'both');
  
  if (sources.length === 0 || targets.length === 0) {
    throw new Error('Need at least one active source and one active target collection');
  }
  
  // Sort sources by priority (lower number = higher priority)
  const sortedSources = [...sources].sort((a, b) => a.priority - b.priority);
  
  // Mock movements for demo purposes
  // In production, this would:
  // 1. Fetch card lists from each collection
  // 2. Determine which cards are needed in targets
  // 3. Find optimal sources based on priority and availability
  // 4. Generate movement list
  
  const movements: Movement[] = [];
  
  // Generate some example movements
  for (let i = 0; i < Math.min(5, targets.length); i++) {
    const target = targets[i];
    const source = sortedSources[i % sortedSources.length];
    
    movements.push({
      cardName: `Example Card ${i + 1}`,
      source: source.name,
      target: target.name,
      quantity: 1,
      setCode: 'EXM',
      collectorNumber: `${i + 1}`,
      foil: false,
    });
  }
  
  return {
    movements,
    sourcesUsed: new Set(movements.map(m => m.source)).size,
    targetsUsed: new Set(movements.map(m => m.target)).size,
    totalCards: movements.reduce((sum, m) => sum + m.quantity, 0),
  };
}
