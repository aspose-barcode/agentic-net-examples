// Title: Generate QR Code barcodes in batch from Excel rows
// Description: Demonstrates reading data from an Excel worksheet and creating a QR Code image for each row, saving the results as PNG files.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, showing how to combine Aspose.Cells for data extraction with Aspose.BarCode to generate QR Code barcodes. It covers key classes such as Workbook, Worksheet, BarcodeGenerator, and BarCodeImageFormat, typical for scenarios like bulk label creation, inventory tagging, or data export where each record needs its own barcode image. Developers often need to automate barcode generation from tabular sources, and this snippet provides a concise reference.
// Prompt: Generate QR Code barcodes in batch from Excel spreadsheet rows and save each as PNG.
// Tags: qr code, batch generation, excel, png, aspose.barcode, aspose.cells, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Cells;

/// <summary>
/// Program that reads an Excel file and generates QR Code PNG images for each data row.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates sample Excel data, reads it, and generates QR Code images.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for output PNG files
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Create a temporary Excel file with sample data
        string excelPath = Path.Combine(Path.GetTempPath(), "SampleData_" + Guid.NewGuid().ToString("N") + ".xlsx");
        using (Workbook workbook = new Workbook())
        {
            Worksheet sheet = workbook.Worksheets[0];

            // Write header row
            sheet.Cells[0, 0].PutValue("ID");
            sheet.Cells[0, 1].PutValue("Text");

            // Populate sample rows
            for (int i = 1; i <= 5; i++)
            {
                sheet.Cells[i, 0].PutValue(i);
                sheet.Cells[i, 1].PutValue($"Sample QR Text {i}");
            }

            // Save the workbook to the temporary file
            workbook.Save(excelPath);
        }

        // Read the Excel file and generate QR codes for each data row
        using (Workbook wb = new Workbook(excelPath))
        {
            Worksheet ws = wb.Worksheets[0];
            int maxRow = ws.Cells.MaxDataRow;

            for (int row = 1; row <= maxRow; row++)
            {
                // Retrieve the text to encode from column B (index 1)
                string codeText = ws.Cells[row, 1].StringValue;
                if (string.IsNullOrEmpty(codeText))
                {
                    // Skip empty rows
                    continue;
                }

                // Define the output PNG file path for the current row
                string pngPath = Path.Combine(outputFolder, $"QR_{row}.png");

                // Generate QR code using Aspose.BarCode
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
                {
                    // Optional: set module size and error correction level
                    generator.Parameters.Barcode.XDimension.Pixels = 4f;
                    generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                    // Save the generated barcode as a PNG image
                    generator.Save(pngPath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Generated QR code for row {row}: {pngPath}");
            }
        }

        Console.WriteLine("Batch QR code generation completed.");
    }
}