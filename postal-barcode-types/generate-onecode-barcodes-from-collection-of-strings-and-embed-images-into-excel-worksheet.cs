// Title: Generate OneCode barcodes and embed into Excel
// Description: Demonstrates creating OneCode barcodes from a list of strings, saving them as PNG images in memory, and inserting them into an Excel worksheet using Aspose.Cells.
// Category-Description: This example belongs to the Aspose.BarCode for .NET barcode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.OneCode to produce barcode images, and how to embed those images into a spreadsheet via Aspose.Cells. Typical use cases include batch creation of product identifiers, inventory tags, or shipping labels that need to be stored alongside data in Excel. Developers often need to generate barcodes programmatically and place them into documents without writing temporary files.
// Prompt: Generate OneCode barcodes from a collection of strings and embed the images into an Excel worksheet.
// Tags: onecode, barcode, generation, excel, aspose.barcode, aspose.cells, png, image embedding

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.Cells;
using Aspose.Cells.Drawing;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates OneCode barcodes from a predefined list of strings
/// and embeds each barcode image into an Excel worksheet.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcodes, inserts them into a workbook,
    /// and saves the workbook to the output folder.
    /// </summary>
    static void Main()
    {
        // Define a collection of OneCode codetexts (20 digits, second digit 0‑4)
        List<string> codes = new List<string>
        {
            "12045678901234567890",
            "13012345678901234567",
            "14098765432109876543"
        };

        // Prepare the output directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Path for the resulting Excel file
        string excelPath = Path.Combine(outputDir, "OneCodeBarcodes.xlsx");

        // Create a new workbook and get the first worksheet
        Workbook workbook = new Workbook();
        Worksheet sheet = workbook.Worksheets[0];

        // Starting cell coordinates for barcode insertion
        int row = 0;
        int col = 0;

        // Iterate over each codetext, generate a barcode, and embed it into the sheet
        foreach (string code in codes)
        {
            // Initialize the barcode generator for OneCode symbology
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.OneCode, code))
            {
                // Configure barcode appearance
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Parameters.Barcode.BarHeight.Pixels = 50f;

                // Save the barcode image to a memory stream in PNG format
                using (MemoryStream ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0; // Reset stream position for reading

                    // Add the image to the worksheet at the specified cell
                    int pictureIndex = sheet.Pictures.Add(row, col, ms);
                    Picture picture = sheet.Pictures[pictureIndex];
                    picture.Placement = PlacementType.FreeFloating;

                    // Move down a few rows before inserting the next barcode
                    row += 5; // leave some rows between barcodes
                }
            }
        }

        // Save the workbook to the designated file
        workbook.Save(excelPath, SaveFormat.Xlsx);
        Console.WriteLine($"Excel file with OneCode barcodes saved to: {excelPath}");
    }
}