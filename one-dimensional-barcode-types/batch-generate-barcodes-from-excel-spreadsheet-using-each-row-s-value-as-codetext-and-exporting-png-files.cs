// Title: Batch barcode generation from Excel rows
// Description: Demonstrates reading an Excel spreadsheet, using each row's value as the barcode text, and exporting PNG images.
// Category-Description: This example belongs to the Aspose.BarCode for .NET batch processing category. It shows how to combine Aspose.Cells to read spreadsheet data with Aspose.BarCode's BarcodeGenerator to create Code128 barcodes. Typical use cases include mass‑producing product labels, inventory tags, or QR codes from data sources. Developers often need to iterate rows, configure barcode parameters, and save images in various formats.
// Prompt: Batch generate barcodes from an Excel spreadsheet, using each row’s value as CodeText and exporting PNG files.
// Tags: barcode, batch, excel, code128, png, generation, aspose.barcode, aspose.cells

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Cells;
using Aspose.Cells.Drawing;

/// <summary>
/// Demonstrates batch generation of Code128 barcodes from an Excel file, saving each as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a sample Excel file, reads each data row, generates a barcode, and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Create a temporary Excel file with sample data
        string excelPath = Path.Combine(Path.GetTempPath(), "SampleData_" + Guid.NewGuid().ToString("N") + ".xlsx");
        using (Workbook wbCreate = new Workbook())
        {
            Worksheet wsCreate = wbCreate.Worksheets[0];
            wsCreate.Cells[0, 0].PutValue("CodeText");   // Header
            wsCreate.Cells[1, 0].PutValue("ABC123");     // Sample row 1
            wsCreate.Cells[2, 0].PutValue("XYZ789");     // Sample row 2
            wsCreate.Cells[3, 0].PutValue("1234567890"); // Sample row 3
            wbCreate.Save(excelPath, SaveFormat.Xlsx);
        }

        // Prepare output folder for generated PNG files
        string outputDir = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Load the Excel file and generate barcodes for each non‑header row
        using (Workbook wb = new Workbook(excelPath))
        {
            Worksheet ws = wb.Worksheets[0];
            int maxRow = ws.Cells.MaxDataRow;

            for (int row = 0; row <= maxRow; row++)
            {
                string codeText = ws.Cells[row, 0].StringValue;

                // Skip header row and any empty rows
                if (string.IsNullOrWhiteSpace(codeText) || row == 0)
                    continue;

                // Build file name and path for the PNG image
                string fileName = $"Barcode_{row}.png";
                string filePath = Path.Combine(outputDir, fileName);

                // Generate Code128 barcode and save as PNG
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
                {
                    generator.Parameters.Barcode.XDimension.Pixels = 2f;
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Generated: {filePath}");
            }
        }

        Console.WriteLine("Barcode generation completed.");
    }
}