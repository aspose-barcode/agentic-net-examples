// Title: Batch QR Code Generation from Excel to PNG
// Description: Demonstrates how to read rows from an Excel worksheet and generate a QR Code barcode for each entry, saving the images as PNG files.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, illustrating the use of BarcodeGenerator with EncodeTypes.QR and Aspose.Cells to read data from spreadsheets. Typical use cases include creating QR codes for product lists, URLs, or inventory items in bulk. Developers often need to combine Aspose.Cells for data extraction with Aspose.BarCode for barcode creation, handling error correction levels and output formats.
// Prompt: Generate QR Code barcodes in batch from Excel spreadsheet rows and save each as PNG.
// Tags: qr code, batch, png, aspose.barcode, aspose.cells, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Cells;

/// <summary>
/// Example program that reads text values from an Excel file and generates a QR Code
/// image for each row, saving the results as PNG files in a temporary directory.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a sample Excel workbook, iterates through its rows,
    /// generates QR Code barcodes, and writes each image to disk.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary root folder for all generated files
        string tempRoot = Path.Combine(Path.GetTempPath(), "AsposeBarcodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempRoot);

        // Define paths for the sample Excel file and the output barcode folder
        string excelPath = Path.Combine(tempRoot, "data.xlsx");
        string outputDir = Path.Combine(tempRoot, "Barcodes");
        Directory.CreateDirectory(outputDir);

        // Generate a sample Excel workbook containing QR code texts
        CreateSampleExcel(excelPath);

        // Load the workbook and select the first worksheet
        var workbook = new Workbook(excelPath);
        var sheet = workbook.Worksheets[0];

        // Determine the last used row (column A holds the code text)
        int maxRow = sheet.Cells.MaxDataRow;
        for (int row = 0; row <= maxRow; row++)
        {
            var cell = sheet.Cells[row, 0];
            if (cell == null || cell.Value == null)
                continue; // Skip empty rows

            string codeText = cell.StringValue?.Trim();
            if (string.IsNullOrEmpty(codeText))
                continue; // Skip rows with no text

            // Build the output file name and full path
            string fileName = $"qr_{row + 1}.png";
            string outputPath = Path.Combine(outputDir, fileName);

            try
            {
                // Initialize the QR Code generator
                using (var generator = new BarcodeGenerator(EncodeTypes.QR))
                {
                    generator.CodeText = codeText;
                    // Optional: set error correction level to Medium
                    generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
                    // Save the barcode directly as PNG (file extension determines format)
                    generator.Save(outputPath);
                }

                Console.WriteLine($"Generated QR for '{codeText}' -> {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to generate QR for row {row + 1}: {ex.Message}");
            }
        }

        Console.WriteLine($"All barcodes saved to: {outputDir}");
    }

    // Helper method to create a simple Excel file with sample QR code texts
    static void CreateSampleExcel(string path)
    {
        var wb = new Workbook();
        var ws = wb.Worksheets[0];

        string[] samples = {
            "HelloWorld",
            "1234567890",
            "https://example.com",
            "Aspose.BarCode",
            "QR_Code_5"
        };

        // Populate column A with sample values
        for (int i = 0; i < samples.Length; i++)
        {
            ws.Cells[i, 0].PutValue(samples[i]);
        }

        // Save the workbook in XLSX format
        wb.Save(path, SaveFormat.Xlsx);
    }
}