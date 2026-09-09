// Title: Embed barcode image into an Excel worksheet cell
// Description: Demonstrates generating a Code128 barcode with Aspose.BarCode, converting it to PNG, and inserting the image into a specific cell of an Excel file using Aspose.Cells.
// Category-Description: This example belongs to the Aspose.BarCode and Aspose.Cells integration category, showcasing how to combine barcode generation with spreadsheet manipulation. It highlights key API classes such as BarcodeGenerator, Bitmap, Workbook, Worksheet, and Picture, which developers frequently use to embed visual barcode data into Excel reports, invoices, or inventory sheets.
// Prompt: Implement feature to embed barcode image into Excel worksheet cell using EPPlus library.
// Tags: barcode, code128, generation, png, excel, aspose.cells, aspose.barcode, epplus

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Cells;
using Aspose.Cells.Drawing;

/// <summary>
/// Provides an example that creates a barcode image and embeds it into an Excel worksheet cell.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, inserts it into a cell, and saves the workbook.
    /// </summary>
    static void Main()
    {
        // Define constants for cell location, image resolution, barcode text, and output file name.
        const int row = 2;                     // Zero‑based row index where the image will be placed
        const int column = 1;                  // Zero‑based column index where the image will be placed
        const float resolution = 300f;         // Desired barcode image resolution (dpi)
        const string barcodeText = "1234567890";
        const string outputPath = "BarcodeInExcel.xlsx";

        // Generate the barcode image and store it in a memory stream.
        using (MemoryStream imageStream = new MemoryStream())
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, barcodeText))
            {
                generator.Parameters.Resolution = resolution;
                generator.Save(imageStream, BarCodeImageFormat.Png);
            }

            // Reset stream position and load the image into a Bitmap to obtain its dimensions.
            imageStream.Position = 0;
            using (Bitmap bitmap = new Bitmap(imageStream))
            {
                // Convert pixel dimensions to Excel column width/row height (Excel uses 96 DPI as base).
                int cellWidth = (96 * bitmap.Width) / 300;
                int cellHeight = (96 * bitmap.Height) / 300;

                // Create a new workbook and adjust the target cell size to fit the barcode image.
                Workbook workbook = new Workbook();
                Worksheet sheet = workbook.Worksheets[0];
                sheet.Cells.SetColumnWidthPixel(column, cellWidth);
                sheet.Cells.SetRowHeightPixel(row, cellHeight);

                // Insert the barcode image into the specified cell range.
                imageStream.Position = 0;
                int pictureIndex = sheet.Pictures.Add(row, column, row + 1, column + 1, imageStream);
                Picture picture = sheet.Pictures[pictureIndex];
                picture.Placement = PlacementType.MoveAndSize; // Ensure the picture moves/resizes with the cell
                picture.Width = bitmap.Width;
                picture.Height = bitmap.Height;

                // Save the workbook to the designated file.
                workbook.Save(outputPath, SaveFormat.Xlsx);
                Console.WriteLine($"Excel file saved to {Path.GetFullPath(outputPath)}");
            }
        }
    }
}