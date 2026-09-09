// Title: Generate OneCode Barcodes and Embed into Excel Worksheet
// Description: Demonstrates creating OneCode barcodes from a list of strings, converting them to PNG images, and inserting each image into an Excel file using Aspose.BarCode and Aspose.Cells.
// Category-Description: This example belongs to the Aspose.BarCode for .NET barcode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.OneCode to produce barcode images, and how to embed those images into an Excel workbook via Aspose.Cells. Typical use cases include batch barcode creation for inventory, shipping labels, or product catalogs, where developers need to programmatically generate barcodes and integrate them into spreadsheet reports.
// Prompt: Generate OneCode barcodes from a collection of strings and embed the images into an Excel worksheet.
// Tags: onecode, barcode, generation, excel, aspose.barcode, aspose.cells, png, image embedding

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Cells;
using Aspose.Cells.Drawing;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates OneCode barcodes from a set of strings
/// and embeds each barcode image into a new Excel worksheet.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates barcode images, inserts them into an Excel file, and saves the file to a temporary location.
    /// </summary>
    static void Main()
    {
        // Define a collection of sample OneCode barcode texts.
        // Valid lengths for OneCode are 20, 25, 29, or 31 digits with the second digit ranging from 0‑4.
        List<string> codes = new List<string>
        {
            "12345678901234567890",                     // 20 digits
            "1234567890123456789012345",                // 25 digits
            "12345678901234567890123456789",            // 29 digits
            "1234567890123456789012345678901"           // 31 digits
        };

        // Determine a temporary file path for the resulting Excel workbook.
        string excelPath = Path.Combine(Path.GetTempPath(), "OneCodeBarcodes.xlsx");

        // Create a new workbook using Aspose.Cells.
        using (Workbook workbook = new Workbook())
        {
            Worksheet sheet = workbook.Worksheets[0];

            // Iterate over each barcode text, generate an image, and insert it into the worksheet.
            for (int i = 0; i < codes.Count; i++)
            {
                string codeText = codes[i];

                // Generate the OneCode barcode image and store it in a memory stream.
                using (MemoryStream ms = new MemoryStream())
                {
                    using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.OneCode, codeText))
                    {
                        // Configure barcode appearance.
                        generator.Parameters.Barcode.XDimension.Pixels = 4f;
                        generator.Parameters.Barcode.BarHeight.Pixels = 50f;

                        // Save the barcode as a PNG image to the memory stream.
                        generator.Save(ms, BarCodeImageFormat.Png);
                    }

                    // Reset stream position before reading.
                    ms.Position = 0;

                    // Insert the image into the worksheet (one image per row).
                    int pictureIndex = sheet.Pictures.Add(i, 0, ms);
                    Picture picture = sheet.Pictures[pictureIndex];
                    picture.Placement = PlacementType.FreeFloating;
                }
            }

            // Save the workbook to the specified file path in XLSX format.
            workbook.Save(excelPath, SaveFormat.Xlsx);
        }

        // Inform the user where the Excel file was saved.
        Console.WriteLine($"Excel file with OneCode barcodes saved to: {excelPath}");
    }
}