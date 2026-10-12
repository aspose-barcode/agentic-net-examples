// Title: Embed Code128 barcode image into an Excel worksheet cell using Aspose.Cells
// Description: Generates a Code128 barcode, saves it as a PNG image, and inserts the image into cell A1 of a new Excel workbook.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to create barcode images with the BarcodeGenerator class and embed them into Office documents using Aspose.Cells. Typical use cases include adding product identifiers, inventory tags, or QR codes directly into spreadsheets for reporting or distribution. Developers often need to combine barcode creation with document automation to streamline data workflows.
/// Prompt: Implement feature to embed barcode image into Excel worksheet cell using EPPlus library.
/// Tags: code128, barcode generation, png, aspose.barcode, aspose.cells, excel

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Cells;
using Aspose.Cells.Drawing;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate a barcode image and embed it into an Excel worksheet cell.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, inserts it into an Excel file, and saves the result.
    /// </summary>
    static void Main()
    {
        // Define the output Excel file path in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "BarcodeInExcel.xlsx");

        // Create a BarcodeGenerator for Code128 with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Hide the human‑readable text beneath the barcode (optional).
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Render the barcode to a memory stream as a PNG image.
            using (var imageStream = new MemoryStream())
            {
                generator.Save(imageStream, BarCodeImageFormat.Png);
                imageStream.Position = 0; // Reset stream position for reading.

                // Create a new Excel workbook and get the first worksheet.
                var workbook = new Workbook();
                var sheet = workbook.Worksheets[0];

                // Insert the barcode image into cell A1 (row 0, column 0).
                int row = 0;
                int column = 0;
                int pictureIndex = sheet.Pictures.Add(row, column, row + 1, column + 1, imageStream);
                var picture = sheet.Pictures[pictureIndex];
                picture.Placement = PlacementType.MoveAndSize; // Ensure the image moves/resizes with the cell.

                // Save the workbook to the specified file in XLSX format.
                workbook.Save(outputPath, SaveFormat.Xlsx);
            }
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"Excel file with barcode saved to: {outputPath}");
    }
}