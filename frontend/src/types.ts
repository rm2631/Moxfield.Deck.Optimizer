export interface Collection {
  id: string;
  url: string;
  name: string;
  priority: number; // 1 (highest) to 5 (lowest)
  role: 'source' | 'target' | 'both';
  active: boolean;
}

export interface Card {
  name: string;
  set?: string;
  collectorNumber?: string;
  foil?: boolean;
  quantity: number;
}

export interface Movement {
  cardName: string;
  source: string;
  target: string;
  quantity: number;
  setCode?: string;
  collectorNumber?: string;
  foil?: boolean;
}

export interface OptimizationResult {
  movements: Movement[];
  sourcesUsed: number;
  targetsUsed: number;
  totalCards: number;
}
