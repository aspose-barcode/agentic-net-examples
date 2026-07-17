// Title: Embed Code128 barcode image into an Excel worksheet cell using Aspose.Cells
// Description: Demonstrates generating a Code128 barcode, converting it to PNG, and inserting it into cell A1 of an Excel file.
// Category-Description: This example belongs to the Aspose.BarCode and Aspose.Cells integration category, showing how to combine barcode generation with spreadsheet manipulation. It uses BarcodeGenerator, Workbook, and Worksheet.Pictures to embed barcode images, a common requirement for inventory, shipping, or reporting solutions where barcodes need to be part of Excel documents.
// Prompt: Implement feature to embed barcode image into Excel worksheet cell using EPPlus library.
// Tags: code128, barcode-generation, png, excel, aspose.barcode, aspose.cells

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Cells;
using Aspose.Cells.Drawing;
using Aspose.Drawing;

/// <summary>
/// Demonstrates embedding a generated barcode image into an Excel worksheet cell.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, saves it to a memory stream,
    /// and inserts the image into cell A1 of a new Excel workbook.
    /// </summary>
    static void Main()
    {
        // Define the output Excel file path.
        string excelPath = "BarcodeExcel.xlsx";

        // Initialize a barcode generator for Code128 with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Optional: customize barcode appearance.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Parameters.Barcode.XDimension.Point = 2f; // Set module size.

            // Save the barcode image to a memory stream in PNG format.
            using (var barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0; // Reset stream position for reading.

                // Create a new workbook and obtain the first worksheet.
                var workbook = new Workbook();
                var worksheet = workbook.Worksheets[0];

                // Add the barcode image to cell A1 (row 0, column 0).
                int pictureIndex = worksheet.Pictures.Add(0, 0, barcodeStream);
                var picture = worksheet.Pictures[pictureIndex];
                picture.Placement = PlacementType.FreeFloating; // Allow free positioning.

                // Save the workbook to the specified file.
                workbook.Save(excelPath, SaveFormat.Xlsx);
            }
        }

        // Inform the user where the Excel file was created.
        Console.WriteLine($"Excel file with embedded barcode created at: {Path.GetFullPath(excelPath)}");
    }
}