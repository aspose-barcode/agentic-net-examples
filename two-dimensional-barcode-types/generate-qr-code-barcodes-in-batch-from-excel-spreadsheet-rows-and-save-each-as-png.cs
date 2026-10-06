// Title: Batch QR Code Generation from Excel to PNG
// Description: Demonstrates how to read QR code data from an Excel worksheet and generate individual QR code images saved as PNG files.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, illustrating how to combine Aspose.Cells for data extraction with Aspose.BarCode to create QR Code barcodes. It shows usage of BarcodeGenerator, EncodeTypes.QR, and image saving via BarCodeImageFormat.Png. Developers often need to generate multiple barcodes from tabular data sources such as spreadsheets, databases, or CSV files, and this pattern provides a reusable approach for bulk barcode creation.
// Prompt: Generate QR Code barcodes in batch from Excel spreadsheet rows and save each as PNG.
// Tags: qr code, batch generation, excel, png, aspose.barcode, aspose.cells, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Cells;
using Aspose.Drawing;

/// <summary>
/// Generates QR Code barcodes in batch from rows of an Excel file and saves each as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary Excel file, reads its rows, and generates QR code images.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all generated files
        string workFolder = Path.Combine(Path.GetTempPath(), "QrBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Define the path for the temporary Excel workbook
        string excelPath = Path.Combine(workFolder, "data.xlsx");

        // -----------------------------------------------------------------
        // Create a sample Excel workbook containing QR code texts
        // -----------------------------------------------------------------
        using (var workbook = new Workbook())
        {
            var sheet = workbook.Worksheets[0];
            sheet.Cells[0, 0].PutValue("HelloWorld");
            sheet.Cells[1, 0].PutValue("https://example.com");
            sheet.Cells[2, 0].PutValue("1234567890");
            sheet.Cells[3, 0].PutValue("Aspose.BarCode");
            sheet.Cells[4, 0].PutValue("QR Code Batch");
            workbook.Save(excelPath, SaveFormat.Xlsx);
        }

        // -----------------------------------------------------------------
        // Load the workbook and generate a QR code image for each non‑empty row
        // -----------------------------------------------------------------
        using (var workbook = new Workbook(excelPath))
        {
            var sheet = workbook.Worksheets[0];
            int maxRow = sheet.Cells.MaxDataRow;

            for (int row = 0; row <= maxRow; row++)
            {
                // Retrieve the text to encode from the first column of the current row
                string codeText = sheet.Cells[row, 0].StringValue;
                if (string.IsNullOrEmpty(codeText))
                    continue; // Skip empty rows

                // Build the output file path for the PNG image
                string outputPath = Path.Combine(workFolder, $"qr_{row + 1}.png");

                // Create a QR code generator with the desired text
                using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
                {
                    // Optional visual settings: pixel size and error correction level
                    generator.Parameters.Barcode.XDimension.Pixels = 4f;
                    generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                    // Save the generated QR code as a PNG file
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Generated QR code for row {row + 1}: {outputPath}");
            }
        }

        Console.WriteLine($"All QR codes saved to: {workFolder}");
    }
}