// Title: Generate OneCode Barcodes and Embed into Excel Worksheet
// Description: Demonstrates creating OneCode barcodes from a collection of strings and inserting the generated PNG images into an Excel workbook using Aspose.BarCode and Aspose.Cells.
// Category-Description: This example belongs to the Aspose.BarCode generation and Aspose.Cells integration category. It showcases how to use BarcodeGenerator (EncodeTypes.OneCode) to produce barcode images, configure barcode dimensions, and embed those images into an Excel worksheet via Workbook, Worksheet, and Pictures APIs. Typical use cases include automating label creation, inventory tracking, and reporting where barcodes need to be part of spreadsheet documents.
// Prompt: Generate OneCode barcodes from a collection of strings and embed the images into an Excel worksheet.
// Tags: onecode, barcode, generation, excel, aspose.barcode, aspose.cells, png, embedding, image, csharp

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Cells;
using Aspose.Cells.Drawing;

/// <summary>
/// Example program that creates OneCode barcodes from a list of strings
/// and inserts the resulting images into an Excel worksheet.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates barcode images, embeds them into a workbook, and saves the file.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for the generated Excel file
        string outputDir = Path.Combine(Path.GetTempPath(), "OneCodeExcelDemo");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define the full path for the Excel workbook to be created
        string excelPath = Path.Combine(outputDir, "OneCodeBarcodes.xlsx");

        // Sample OneCode strings (valid lengths and second digit 0-4)
        List<string> oneCodeValues = new List<string>
        {
            "12345678901234567890",                     // 20 digits
            "1234567890123456789012345",                // 25 digits
            "12345678901234567890123456789",            // 29 digits
            "1234567890123456789012345678901"           // 31 digits
        };

        // Create a new workbook and obtain the first worksheet
        using (var workbook = new Workbook())
        {
            var worksheet = workbook.Worksheets[0];

            // Iterate over each OneCode value, generate a barcode, and insert it into the sheet
            for (int i = 0; i < oneCodeValues.Count; i++)
            {
                string codeText = oneCodeValues[i];

                // Generate OneCode barcode image in memory
                using (var generator = new BarcodeGenerator(EncodeTypes.OneCode, codeText))
                {
                    // Configure barcode appearance
                    generator.Parameters.Barcode.XDimension.Pixels = 4;
                    generator.Parameters.Barcode.BarHeight.Pixels = 50;

                    // Save the barcode to a memory stream as PNG
                    using (var ms = new MemoryStream())
                    {
                        generator.Save(ms, BarCodeImageFormat.Png);
                        ms.Position = 0; // Reset stream position for reading

                        // Insert the image into the worksheet at row i, column 0
                        int pictureIndex = worksheet.Pictures.Add(i, 0, ms);
                        var picture = worksheet.Pictures[pictureIndex];
                        picture.Placement = PlacementType.FreeFloating;
                    }
                }
            }

            // Save the workbook to the specified file path
            workbook.Save(excelPath, SaveFormat.Xlsx);
        }

        Console.WriteLine($"Excel file with OneCode barcodes created at: {excelPath}");
    }
}