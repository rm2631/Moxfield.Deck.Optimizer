using Moxfield.Deck.Optimizer.Core.Models;

namespace Moxfield.Deck.Optimizer.Core.Services;

/// <summary>
/// Interface for exporting optimization results to Excel
/// </summary>
public interface IExcelExporter
{
    /// <summary>
    /// Exports optimization result to an Excel file
    /// </summary>
    /// <param name="result">The optimization result to export</param>
    /// <param name="outputPath">Path where the Excel file should be saved</param>
    void Export(OptimizationResult result, string outputPath);
}
