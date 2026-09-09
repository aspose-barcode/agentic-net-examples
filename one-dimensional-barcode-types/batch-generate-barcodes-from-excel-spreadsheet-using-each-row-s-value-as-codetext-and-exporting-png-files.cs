// Title: Batch generate Code128 barcodes from Excel rows to PNG files
// Description: Demonstrates reading code text values from an Excel spreadsheet and creating a PNG barcode image for each row using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showing how to combine Aspose.Cells for data extraction with Aspose.BarCode to produce barcodes. It highlights the use of BarcodeGenerator, EncodeTypes, and image export settings, a common requirement for bulk barcode creation in inventory, shipping, or labeling applications.
// Prompt: Batch generate barcodes from an Excel spreadsheet, using each row’s value as CodeText and exporting PNG files.
// Tags: barcode symbology, batch generation, excel, png, aspose.barcode, aspose.cells, code128

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Cells;
using Aspose.Cells.Drawing;
using Aspose.Drawing;

/// <summary>
/// Example program that reads code texts from an Excel file and generates a PNG barcode for each row.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a temporary Excel file, reads its rows, and saves a barcode image per row.
    /// </summary>
    static void Main()
    {
        // ----------------------------------------------------------------------
        // 1. Create a temporary Excel workbook with sample barcode values.
        // ----------------------------------------------------------------------
        string excelPath = Path.Combine(Path.GetTempPath(), "BarcodesSample_" + Guid.NewGuid().ToString("N") + ".xlsx");
        using (var workbook = new Workbook())
        {
            var sheet = workbook.Worksheets[0];

            // Sample code texts that will become barcode values.
            var codeTexts = new List<string> { "123456789012", "ABC-1234", "9876543210", "CODE128TEST", "A1B2C3D4" };
            for (int i = 0; i < codeTexts.Count; i++)
            {
                sheet.Cells[i, 0].PutValue(codeTexts[i]);
            }

            workbook.Save(excelPath);
        }

        // Verify that the Excel file was created successfully.
        if (!File.Exists(excelPath))
        {
            Console.WriteLine("Failed to create the Excel file.");
            return;
        }

        // ----------------------------------------------------------------------
        // 2. Prepare an output folder where PNG barcode images will be stored.
        // ----------------------------------------------------------------------
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodesOutput_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // ----------------------------------------------------------------------
        // 3. Open the Excel file, read each row, and generate a barcode image.
        // ----------------------------------------------------------------------
        using (var workbook = new Workbook(excelPath))
        {
            var sheet = workbook.Worksheets[0];
            int rowCount = sheet.Cells.MaxDataRow + 1; // Include the last row that contains data.

            for (int row = 0; row < rowCount; row++)
            {
                // Retrieve the code text from the first column of the current row.
                string codeText = sheet.Cells[row, 0].StringValue;
                if (string.IsNullOrWhiteSpace(codeText))
                {
                    // Skip empty rows.
                    continue;
                }

                // Build the full path for the PNG file that will hold the barcode.
                string pngPath = Path.Combine(outputFolder, $"Barcode_{row + 1}.png");

                // Generate the barcode using Code128 symbology.
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
                {
                    // Optional visual settings for better readability.
                    generator.Parameters.Barcode.XDimension.Pixels = 2f;
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                    generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                    generator.Parameters.Resolution = 300f;

                    // Save the barcode as a PNG image.
                    generator.Save(pngPath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Generated barcode for row {row + 1}: {pngPath}");
            }
        }

        Console.WriteLine("Barcode generation completed.");
    }
}