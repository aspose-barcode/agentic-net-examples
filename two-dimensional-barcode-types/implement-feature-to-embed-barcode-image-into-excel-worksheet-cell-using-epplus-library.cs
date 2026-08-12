// Title: Embed Code128 barcode image into Excel worksheet using Aspose.Cells
// Description: Demonstrates generating a Code128 barcode with Aspose.BarCode, saving it as a PNG image, and embedding that image into an Excel file using Aspose.Cells.
// Category-Description: This example belongs to the barcode generation and document integration category. It showcases how to use Aspose.BarCode to create barcode images and Aspose.Cells to insert those images into Excel workbooks. Typical use cases include adding product identifiers, inventory tags, or QR codes directly into spreadsheets for reporting or distribution. Developers often need to combine barcode creation with Office document manipulation, leveraging classes like BarcodeGenerator, Workbook, Worksheet, and Picture.
// Prompt: Implement feature to embed barcode image into Excel worksheet cell using EPPlus library.
// Tags: code128, barcode-generation, png, aspose.barcode, aspose.cells

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Cells;
using Aspose.Cells.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a barcode image and embeds it into an Excel worksheet cell.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a Code128 barcode, saves it as PNG,
    /// inserts it into cell A1 of a new Excel workbook, and writes the file to disk.
    /// </summary>
    static void Main()
    {
        // Define barcode content and output file name
        const string barcodeText = "123ABC";
        const string outputExcelPath = "BarcodeExcel.xlsx";

        // Create a memory stream to hold the generated barcode image
        using (var barcodeStream = new MemoryStream())
        {
            // Generate the barcode and write it to the stream in PNG format
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, barcodeText))
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position so it can be read from the beginning
            barcodeStream.Position = 0;

            // Create a new Excel workbook and obtain the first worksheet
            using (var workbook = new Workbook())
            {
                Worksheet sheet = workbook.Worksheets[0];

                // Insert the barcode image into cell A1 (row 0, column 0)
                int pictureIndex = sheet.Pictures.Add(0, 0, barcodeStream);
                Picture picture = sheet.Pictures[pictureIndex];
                picture.Placement = PlacementType.FreeFloating;

                // Save the workbook containing the embedded barcode
                workbook.Save(outputExcelPath);
            }
        }

        // Output the full path of the generated Excel file
        Console.WriteLine($"Excel file with embedded barcode saved to: {Path.GetFullPath(outputExcelPath)}");
    }
}