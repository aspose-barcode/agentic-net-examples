using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch generation of QR codes, optionally loading data from an Excel file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates QR codes based on data from an Excel file
    /// or fallback sample data and saves them as PNG images.
    /// </summary>
    static void Main()
    {
        // Path to the Excel file (if available)
        string excelPath = "data.xlsx";

        // List to hold the text values that will be encoded into QR codes
        List<string> codeTexts = new List<string>();

        // Attempt to read QR code data from the Excel file
        if (File.Exists(excelPath))
        {
            // NOTE: Reading Excel requires Aspose.Cells, which is not available in the snippet runner.
            // The following commented block shows how it would be done in a real environment.
            /*
            // using Aspose.Cells;
            using (var workbook = new Aspose.Cells.Workbook(excelPath))
            {
                var sheet = workbook.Worksheets[0];
                // Limit to a maximum of 5 rows for safety
                int maxRows = Math.Min(5, sheet.Cells.MaxDataRow + 1);
                for (int row = 0; row < maxRows; row++)
                {
                    var cell = sheet.Cells[row, 0]; // assuming codes are in the first column
                    if (cell != null && cell.Value != null)
                    {
                        codeTexts.Add(cell.StringValue);
                    }
                }
            }
            */
            Console.WriteLine("Aspose.Cells is not available; falling back to sample data.");
        }

        // If no data was loaded from Excel, use predefined sample data
        if (codeTexts.Count == 0)
        {
            codeTexts.AddRange(new[]
            {
                "https://example.com/1",
                "https://example.com/2",
                "https://example.com/3",
                "https://example.com/4",
                "https://example.com/5"
            });
        }

        // Ensure the output directory exists before saving images
        string outputDir = "Barcodes";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define the barcode type (QR) for generation
        BaseEncodeType qrEncodeType = EncodeTypes.QR;
        int index = 1;

        // Iterate over each text entry and generate a corresponding QR code image
        foreach (string text in codeTexts)
        {
            // Build the full file path for the output PNG
            string outputPath = Path.Combine(outputDir, $"qr_{index}.png");

            // Create a barcode generator for the current text
            using (var generator = new BarcodeGenerator(qrEncodeType, text))
            {
                // Set optional error correction level (Level M)
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Save the generated QR code as a PNG file
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Saved QR code {index} to '{outputPath}'.");
            index++;
        }

        Console.WriteLine("Batch QR code generation completed.");
    }
}