// Title: Batch barcode generation from Excel to PNG files
// Description: Demonstrates reading an Excel file row‑by‑row, using each cell value as the barcode text, and saving each barcode as a PNG image.
// Category-Description: Shows how to use Aspose.Cells to read spreadsheet data together with Aspose.BarCode to generate barcodes. Typical use cases include bulk creation of product labels, inventory tags, or QR codes from a data source. Developers often work with Workbook, Worksheet, BarcodeGenerator, and image export APIs to automate barcode production.
// Prompt: Batch generate barcodes from an Excel spreadsheet, using each row’s value as CodeText and exporting PNG files.
// Tags: barcode, batch, excel, code128, png, aspose.barcode, aspose.cells, generation

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Cells;

/// <summary>
/// Example program that creates a temporary Excel file, reads each row,
/// generates a Code128 barcode for the cell value, and saves the barcode as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs the end‑to‑end barcode generation workflow.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the generated barcode images.
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Create a temporary Excel file containing sample barcode texts.
        string excelPath = Path.Combine(Path.GetTempPath(), "BarcodesInput_" + Guid.NewGuid().ToString("N") + ".xlsx");
        CreateSampleExcel(excelPath);

        // Open the Excel workbook for reading.
        using (Workbook workbook = new Workbook(excelPath))
        {
            var worksheet = workbook.Worksheets[0];
            int rowCount = worksheet.Cells.MaxDataRow + 1; // Total rows with data.

            // Iterate through each row and generate a barcode if the cell contains text.
            for (int row = 0; row < rowCount; row++)
            {
                var cell = worksheet.Cells[row, 0];
                string codeText = cell.StringValue?.Trim();

                // Skip empty cells.
                if (string.IsNullOrEmpty(codeText))
                    continue;

                // Build a safe file name for the PNG image.
                string safeFileName = MakeSafeFileName(codeText);
                string outputPath = Path.Combine(outputFolder, $"{safeFileName}_{row}.png");

                // Generate the barcode using Code128 symbology.
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
                {
                    // Adjust barcode dimensions for better readability.
                    generator.Parameters.Barcode.XDimension.Pixels = 2f;
                    // Save the barcode image as PNG.
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                }

                // Inform the user about the generated file.
                Console.WriteLine($"Generated barcode for '{codeText}' at '{outputPath}'");
            }
        }

        // Final summary of the output location.
        Console.WriteLine($"All barcodes saved to: {outputFolder}");
    }

    /// <summary>
    /// Creates a simple Excel workbook with a single column of sample barcode texts.
    /// </summary>
    /// <param name="path">Full path where the workbook will be saved.</param>
    static void CreateSampleExcel(string path)
    {
        using (Workbook wb = new Workbook())
        {
            var ws = wb.Worksheets[0];
            string[] samples = new string[] { "ABC123", "987654321", "HelloWorld", "2023-10-29", "Sample5" };
            for (int i = 0; i < samples.Length; i++)
            {
                ws.Cells[i, 0].PutValue(samples[i]);
            }
            wb.Save(path, SaveFormat.Xlsx);
        }
    }

    /// <summary>
    /// Sanitizes a string to be used safely as a file name by replacing invalid characters with underscores.
    /// </summary>
    /// <param name="name">Original string.</param>
    /// <returns>Sanitized file‑name‑compatible string.</returns>
    static string MakeSafeFileName(string name)
    {
        var sb = new StringBuilder();
        foreach (char c in name)
        {
            if (char.IsLetterOrDigit(c) || c == '_' || c == '-')
                sb.Append(c);
            else
                sb.Append('_');
        }
        return sb.ToString();
    }
}