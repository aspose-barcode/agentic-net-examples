// Title: Batch QR Code generation from Excel rows
// Description: Demonstrates reading QR code data from an Excel spreadsheet and generating individual PNG barcode images.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, illustrating how to combine Aspose.Cells for data extraction with Aspose.BarCode's BarcodeGenerator to create QR Code barcodes. Typical use cases include bulk barcode creation for inventory, marketing, or document tagging. Developers often need to read tabular data, loop through rows, and save each barcode as an image file.
// Prompt: Generate QR Code barcodes in batch from Excel spreadsheet rows and save each as PNG.
// Tags: qr code, batch processing, excel, png, aspose.barcode, aspose.cells, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Cells;

/// <summary>
/// Generates QR Code barcodes in batch from rows of an Excel file and saves each as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Reads data from an Excel file, creates QR codes, and writes them to disk.
    /// </summary>
    static void Main()
    {
        // Define paths for the input Excel file and the output folder that will hold the PNG images
        string excelPath = "input.xlsx";
        string outputFolder = "Barcodes";

        // Ensure the output directory exists; create it if it does not
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // If the Excel file is missing, create a sample workbook with a header and up to five data rows
        if (!File.Exists(excelPath))
        {
            using (var workbook = new Workbook())
            {
                var sheet = workbook.Worksheets[0];
                var cells = sheet.Cells;

                // Optional header row
                cells[0, 0].PutValue("CodeText");

                // Sample QR code texts – limited to five rows for safe batch processing in CI environments
                cells[1, 0].PutValue("HelloWorld1");
                cells[2, 0].PutValue("HelloWorld2");
                cells[3, 0].PutValue("HelloWorld3");
                cells[4, 0].PutValue("HelloWorld4");
                cells[5, 0].PutValue("HelloWorld5");

                workbook.Save(excelPath);
                Console.WriteLine($"Sample Excel file created at '{excelPath}'.");
            }
        }

        // Load the Excel workbook containing the QR code data
        using (var workbook = new Workbook(excelPath))
        {
            var sheet = workbook.Worksheets[0];
            var cells = sheet.Cells;

            // Determine the number of data rows to process (max 5 rows for CI safety)
            int maxDataRow = Math.Min(cells.MaxDataRow, 5); // rows are zero‑based; this gives up to 5 data rows

            // Iterate over each data row, starting after the header (row index 1)
            for (int row = 1; row <= maxDataRow; row++)
            {
                // Retrieve and trim the code text from the first column
                string codeText = cells[row, 0].StringValue?.Trim();

                // Skip empty cells
                if (string.IsNullOrEmpty(codeText))
                {
                    Console.WriteLine($"Row {row + 1}: empty code text – skipped.");
                    continue;
                }

                // Create a QR code generator for the current text
                using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
                {
                    // Optional: set a high error correction level for better resilience
                    generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                    // Build the output file name (e.g., qr_1.png, qr_2.png, ...)
                    string outputPath = Path.Combine(outputFolder, $"qr_{row}.png");

                    // Save the generated QR code as a PNG image
                    generator.Save(outputPath);
                    Console.WriteLine($"Row {row + 1}: QR code saved to '{outputPath}'.");
                }
            }
        }

        Console.WriteLine("Batch QR code generation completed.");
    }
}