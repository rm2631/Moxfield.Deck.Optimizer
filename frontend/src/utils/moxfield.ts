/**
 * Utilities for interacting with Moxfield
 */

/**
 * Extracts collection name from Moxfield URL
 * @param url - Moxfield URL (e.g., https://www.moxfield.com/decks/abc123)
 * @returns Collection name or null if invalid
 */
export function extractCollectionName(url: string): string | null {
  try {
    const urlObj = new URL(url);
    if (!urlObj.hostname.includes('moxfield.com')) {
      return null;
    }
    
    const pathParts = urlObj.pathname.split('/').filter(Boolean);
    if (pathParts.length < 2) {
      return null;
    }
    
    // For now, use the ID as the name - in production, we'd fetch the actual name
    const collectionId = pathParts[pathParts.length - 1];
    return collectionId;
  } catch {
    return null;
  }
}

/**
 * Validates if a URL is a valid Moxfield URL
 * @param url - URL to validate
 * @returns true if valid, false otherwise
 */
export function isValidMoxfieldUrl(url: string): boolean {
  try {
    const urlObj = new URL(url);
    return urlObj.hostname.includes('moxfield.com') && 
           urlObj.pathname.includes('/decks/');
  } catch {
    return false;
  }
}

/**
 * Fetches collection data from Moxfield
 * Note: This is a placeholder. In production, we'd need to:
 * 1. Use Moxfield's public API if available
 * 2. Or use a serverless function to proxy requests
 * @param url - Moxfield collection URL
 * @returns Collection data (mocked for now)
 */
export async function fetchCollectionData(url: string): Promise<{
  name: string;
  cards: Array<{ name: string; quantity: number }>;
}> {
  // Simulate API call delay
  await new Promise(resolve => setTimeout(resolve, 500));
  
  // Extract collection name
  const name = extractCollectionName(url);
  
  if (!name) {
    throw new Error('Invalid Moxfield URL');
  }
  
  // Return mock data
  // In production, this would fetch real data from Moxfield
  return {
    name: `Collection: ${name}`,
    cards: [] // Would contain actual card data
  };
}
