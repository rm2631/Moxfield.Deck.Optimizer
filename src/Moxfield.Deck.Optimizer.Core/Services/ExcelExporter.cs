using Moxfield.Deck.Optimizer.Core.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace Moxfield.Deck.Optimizer.Core.Services;

/// <summary>
/// Exports optimization results to Excel format using EPPlus
/// </summary>
public class ExcelExporter : IExcelExporter
{
    static ExcelExporter()
    {
        // Set EPPlus license for version 8+
        OfficeOpenXml.ExcelPackage.License.SetNonCommercialPersonal("Moxfield Deck Optimizer");
    }

    public ExcelExporter()
    {
    }

    public void Export(OptimizationResult result, string outputPath)
    {
        using var package = new ExcelPackage();

        // Create the main movements sheet
        CreateMovementsSheet(package, result);

        // Create the statistics sheet
        CreateStatisticsSheet(package, result);

        // Create the missing cards sheet if there are any
        if (result.MissingCards.Any())
        {
            CreateMissingCardsSheet(package, result);
        }

        // Save the file
        package.SaveAs(new FileInfo(outputPath));
    }

    private void CreateMovementsSheet(ExcelPackage package, OptimizationResult result)
    {
        var worksheet = package.Workbook.Worksheets.Add("Card Movements");

        // Add headers
        worksheet.Cells[1, 1].Value = "Card Name";
        worksheet.Cells[1, 2].Value = "Quantity";
        worksheet.Cells[1, 3].Value = "Source Collection";
        worksheet.Cells[1, 4].Value = "Priority";
        worksheet.Cells[1, 5].Value = "Board";
        worksheet.Cells[1, 6].Value = "Set";
        worksheet.Cells[1, 7].Value = "Collector #";
        worksheet.Cells[1, 8].Value = "Foil";

        // Style headers
        using (var range = worksheet.Cells[1, 1, 1, 8])
        {
            range.Style.Font.Bold = true;
            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
            range.Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            range.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
        }

        // Group movements by source collection for better organization
        var groupedMovements = result.Movements
            .OrderBy(m => m.Priority)
            .ThenBy(m => m.SourceCollection)
            .ThenBy(m => m.Card.Name)
            .ToList();

        // Add data rows
        int row = 2;
        string? currentCollection = null;
        foreach (var movement in groupedMovements)
        {
            // Add separator row when collection changes
            if (currentCollection != null && currentCollection != movement.SourceCollection)
            {
                worksheet.Cells[row, 1, row, 8].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            }
            currentCollection = movement.SourceCollection;

            worksheet.Cells[row, 1].Value = movement.Card.Name;
            worksheet.Cells[row, 2].Value = movement.Quantity;
            worksheet.Cells[row, 3].Value = movement.SourceCollection;
            worksheet.Cells[row, 4].Value = movement.Priority;
            worksheet.Cells[row, 5].Value = movement.Board;
            worksheet.Cells[row, 6].Value = movement.Card.SetCode;
            worksheet.Cells[row, 7].Value = movement.Card.CollectorNumber;
            worksheet.Cells[row, 8].Value = movement.Card.IsFoil ? "Yes" : "No";

            row++;
        }

        // Auto-fit columns
        worksheet.Cells.AutoFitColumns();
    }

    private void CreateStatisticsSheet(ExcelPackage package, OptimizationResult result)
    {
        var worksheet = package.Workbook.Worksheets.Add("Statistics");

        int row = 1;

        // Deck info
        worksheet.Cells[row, 1].Value = "Deck Name:";
        worksheet.Cells[row, 2].Value = result.Deck.Name;
        worksheet.Cells[row, 1].Style.Font.Bold = true;
        row += 2;

        // Overall statistics
        worksheet.Cells[row, 1].Value = "Total Cards Needed:";
        worksheet.Cells[row, 2].Value = result.Statistics.TotalCardsNeeded;
        worksheet.Cells[row, 1].Style.Font.Bold = true;
        row++;

        worksheet.Cells[row, 1].Value = "Cards Found:";
        worksheet.Cells[row, 2].Value = result.Statistics.CardsFound;
        worksheet.Cells[row, 1].Style.Font.Bold = true;
        row++;

        worksheet.Cells[row, 1].Value = "Cards Missing:";
        worksheet.Cells[row, 2].Value = result.Statistics.CardsMissing;
        worksheet.Cells[row, 1].Style.Font.Bold = true;
        if (result.Statistics.CardsMissing > 0)
        {
            worksheet.Cells[row, 2].Style.Font.Color.SetColor(Color.Red);
        }
        row++;

        worksheet.Cells[row, 1].Value = "Sources Used:";
        worksheet.Cells[row, 2].Value = result.Statistics.SourcesUsed;
        worksheet.Cells[row, 1].Style.Font.Bold = true;
        row += 2;

        // Cards per source
        if (result.Statistics.CardsPerSource.Any())
        {
            worksheet.Cells[row, 1].Value = "Cards Per Source:";
            worksheet.Cells[row, 1].Style.Font.Bold = true;
            row++;

            foreach (var kvp in result.Statistics.CardsPerSource.OrderByDescending(x => x.Value))
            {
                worksheet.Cells[row, 1].Value = kvp.Key;
                worksheet.Cells[row, 2].Value = kvp.Value;
                row++;
            }
        }

        worksheet.Cells.AutoFitColumns();
    }

    private void CreateMissingCardsSheet(ExcelPackage package, OptimizationResult result)
    {
        var worksheet = package.Workbook.Worksheets.Add("Missing Cards");

        // Add header
        worksheet.Cells[1, 1].Value = "Missing Cards";
        worksheet.Cells[1, 1].Style.Font.Bold = true;
        worksheet.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
        worksheet.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(Color.LightCoral);

        // Add missing cards
        int row = 2;
        foreach (var missingCard in result.MissingCards.OrderBy(c => c))
        {
            worksheet.Cells[row, 1].Value = missingCard;
            row++;
        }

        worksheet.Cells.AutoFitColumns();
    }
}
