// Title: Generate QR Code barcodes from Excel rows in batch
// Description: Demonstrates reading data from an Excel worksheet and creating a QR Code barcode for each non‑empty cell, saving the images as PNG files.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showing how to use BarcodeGenerator with EncodeTypes.QR, configure parameters, and export to common image formats. Typical use cases include bulk barcode creation from data sources such as spreadsheets, databases, or CSV files. Developers often need to automate barcode production for inventory, marketing, or authentication purposes.
// Prompt: Generate QR Code barcodes in batch from Excel spreadsheet rows and save each as PNG.
// Tags: qr code, barcode generation, batch processing, excel, png, aspose.barcode, aspose.cells

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Cells;
using Aspose.Drawing;

/// <summary>
/// Example program that reads text values from the first column of an Excel worksheet,
/// generates a QR Code barcode for each value, and saves the barcodes as PNG images
/// in a temporary output folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for output PNG files
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Create a temporary Excel file with sample data
        string excelPath = Path.Combine(Path.GetTempPath(), "SampleData_" + Guid.NewGuid().ToString("N") + ".xlsx");
        using (var workbook = new Workbook())
        {
            var sheet = workbook.Worksheets[0];
            sheet.Cells[0, 0].PutValue("Hello World");
            sheet.Cells[1, 0].PutValue("https://example.com");
            sheet.Cells[2, 0].PutValue("1234567890");
            workbook.Save(excelPath, SaveFormat.Xlsx);
        }

        // Verify the Excel file exists
        if (!File.Exists(excelPath))
        {
            Console.WriteLine("Failed to create the Excel file.");
            return;
        }

        // Read the Excel file and generate QR codes
        using (var workbook = new Workbook(excelPath))
        {
            var sheet = workbook.Worksheets[0];
            int maxRow = sheet.Cells.MaxDataRow;

            // Iterate through each used row in the first column
            for (int row = 0; row <= maxRow; row++)
            {
                var cell = sheet.Cells[row, 0];
                string codeText = cell.StringValue?.Trim();

                // Skip empty cells
                if (string.IsNullOrEmpty(codeText))
                {
                    continue;
                }

                // Generate QR Code barcode for the cell value
                using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
                {
                    // Optional: set module size (pixel dimension of a single QR element)
                    generator.Parameters.Barcode.XDimension.Pixels = 4f;

                    // Build a safe file name and save the barcode as PNG
                    string safeFileName = $"Barcode_{row + 1}.png";
                    string outputPath = Path.Combine(outputFolder, safeFileName);
                    generator.Save(outputPath, BarCodeImageFormat.Png);

                    Console.WriteLine($"Generated QR code for row {row + 1}: {outputPath}");
                }
            }
        }

        Console.WriteLine($"All barcodes saved to: {outputFolder}");
    }
}