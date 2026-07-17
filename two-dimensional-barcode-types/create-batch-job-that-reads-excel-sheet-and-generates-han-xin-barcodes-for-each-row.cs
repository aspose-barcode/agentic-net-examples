// Title: Generate Han Xin barcodes from Excel rows
// Description: Reads an Excel file and creates a Han Xin barcode image for each non‑empty row.
// Category-Description: Demonstrates batch barcode generation using Aspose.BarCode and Aspose.Cells. The example shows how to load data from an Excel worksheet, iterate through rows, and produce Han Xin (Chinese 2‑D) barcodes with customizable parameters. Developers working with bulk barcode creation, data‑driven workflows, or QR‑like symbologies can use this pattern as a reference.
// Prompt: Create a batch job that reads an Excel sheet and generates Han Xin barcodes for each row.
// Tags: hanxin, barcode, batch, excel, aspose.barcode, aspose.cells, png, generation

using System;
using System.IO;
using Aspose.Cells;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Batch job that reads an Excel sheet and generates Han Xin barcodes for each row.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Loads the Excel file, iterates rows, and saves barcode images.
    /// </summary>
    static void Main()
    {
        // Path to the Excel file (adjust as needed)
        string excelPath = "Data.xlsx";

        // Verify the Excel file exists
        if (!File.Exists(excelPath))
        {
            Console.WriteLine($"Excel file not found: {excelPath}");
            return;
        }

        // Output directory for generated barcode images
        string outputDir = "Barcodes";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Load the workbook
        using (var workbook = new Workbook(excelPath))
        {
            // Use the first worksheet
            var sheet = workbook.Worksheets[0];

            // Determine the last row with data
            int maxRow = sheet.Cells.MaxDataRow;

            // Iterate through each row (starting at row 0)
            for (int row = 0; row <= maxRow; row++)
            {
                // Assume the code text is in the first column (A)
                string codeText = sheet.Cells[row, 0].StringValue?.Trim();

                // Skip empty rows
                if (string.IsNullOrEmpty(codeText))
                {
                    continue;
                }

                // Create a Han Xin barcode generator for the current code text
                using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
                {
                    // Set barcode colors (optional)
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                    generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                    // Configure Han Xin specific parameters
                    generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L2; // Medium error correction
                    generator.Parameters.Barcode.HanXin.Version = HanXinVersion.Auto;   // Auto-select version based on payload

                    // Optional: adjust module size and padding if desired
                    generator.Parameters.Barcode.XDimension.Point = 2f;
                    generator.Parameters.Barcode.Padding.Left.Point = 5f;
                    generator.Parameters.Barcode.Padding.Top.Point = 5f;
                    generator.Parameters.Barcode.Padding.Right.Point = 5f;
                    generator.Parameters.Barcode.Padding.Bottom.Point = 5f;

                    // Build output file name (e.g., barcode_0.png, barcode_1.png, ...)
                    string outputPath = Path.Combine(outputDir, $"barcode_{row}.png");

                    // Save the barcode image as PNG
                    generator.Save(outputPath);
                }

                Console.WriteLine($"Generated barcode for row {row}: {codeText}");
            }
        }

        Console.WriteLine("Barcode generation completed.");
    }
}