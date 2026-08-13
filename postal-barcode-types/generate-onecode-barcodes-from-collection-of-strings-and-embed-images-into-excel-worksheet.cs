// Title: Generate OneCode barcodes and embed into Excel
// Description: Demonstrates creating OneCode barcodes from numeric strings, converting them to PNG images, and inserting those images into an Excel worksheet using Aspose.BarCode and Aspose.Cells.
// Category-Description: This example belongs to the Aspose.BarCode for .NET barcode generation category, focusing on image rendering and integration with spreadsheet documents. It showcases the use of BarcodeGenerator, EncodeTypes.OneCode, and Aspose.Cells workbook manipulation to embed barcode images. Developers working on inventory, tracking, or labeling solutions often need to generate barcodes and place them into Excel reports or templates, making this pattern a common requirement.
// Prompt: Generate OneCode barcodes from a collection of strings and embed the images into an Excel worksheet.
// Tags: onecode, barcode, generation, excel, aspose.barcode, aspose.cells, png, image embedding

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Cells;
using Aspose.Cells.Drawing;

/// <summary>
/// Example program that generates OneCode barcodes from a list of numeric strings
/// and embeds the resulting PNG images into an Excel worksheet.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates barcodes, adds them to a workbook, and saves the file.
    /// </summary>
    static void Main()
    {
        // Define a collection of OneCode numeric strings (20, 25, 29, 31 digits)
        List<string> codes = new List<string>
        {
            "12345678901234567890",               // 20 digits
            "1234567890123456789012345",          // 25 digits
            "12345678901234567890123456789",      // 29 digits
            "1234567890123456789012345678901"     // 31 digits
        };

        // Create a new Excel workbook and get the first worksheet
        Workbook workbook = new Workbook();
        Worksheet sheet = workbook.Worksheets[0];

        // Starting cell coordinates for the first barcode image
        int startRow = 0;
        int startColumn = 0;

        // Iterate over each code string, generate a barcode image, and embed it
        foreach (string code in codes)
        {
            // Use a memory stream to hold the generated PNG image
            using (MemoryStream imageStream = new MemoryStream())
            {
                // Initialize the barcode generator for OneCode symbology
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.OneCode))
                {
                    generator.CodeText = code;

                    // OneCode requires an exact length; suppress exception for demonstration purposes
                    generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

                    // Generate the barcode as a bitmap
                    using (Bitmap bitmap = generator.GenerateBarCodeImage())
                    {
                        // Save the bitmap to the memory stream in PNG format
                        bitmap.Save(imageStream, ImageFormat.Png);
                    }
                }

                // Reset the stream position before reading it back into the worksheet
                imageStream.Position = 0;

                // Add the PNG image to the worksheet at the specified cell
                int pictureIndex = sheet.Pictures.Add(startRow, startColumn, imageStream);
                Picture picture = sheet.Pictures[pictureIndex];
                picture.Placement = PlacementType.FreeFloating;

                // Add a textual label below the barcode image for reference
                int labelRow = startRow + 5; // Adjust row offset as needed
                sheet.Cells[labelRow, startColumn].PutValue(code);
            }

            // Advance the start row to provide spacing between successive barcode images
            startRow += 15; // Space between images
        }

        // Save the populated workbook to an XLSX file
        string outputPath = "OneCodeBarcodes.xlsx";
        workbook.Save(outputPath, SaveFormat.Xlsx);
        Console.WriteLine($"Workbook saved to {Path.GetFullPath(outputPath)}");
    }
}