// Title: Embed a Code128 barcode image into an Excel cell using Aspose.BarCode and Aspose.Cells
// Description: Demonstrates generating a Code128 barcode image with Aspose.BarCode and inserting it into a specific cell of an Excel worksheet using Aspose.Cells. The example adjusts the cell dimensions to fit the barcode image.
// Category-Description: This example belongs to the Aspose.BarCode generation and Aspose.Cells integration category, showing how to create barcode images (using BarcodeGenerator, EncodeTypes) and embed them into Excel files (using Workbook, Worksheet, Picture). Typical use cases include adding product identifiers, inventory tags, or QR codes directly into spreadsheets for reporting or distribution. Developers often need to control image resolution, hide human‑readable text, and align the picture within cells.
// Prompt: Implement feature to embed barcode image into Excel worksheet cell using EPPlus library.
// Tags: code128, barcode-generation, png, aspose.barcode, aspose.cells

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Cells;
using Aspose.Cells.Drawing;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates embedding a generated barcode image into an Excel worksheet cell.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, adjusts cell size, inserts the image, and saves the workbook.
    /// </summary>
    static void Main()
    {
        // Define output file path and barcode parameters
        string outputPath = Path.Combine(Path.GetTempPath(), "BarcodeInExcel.xlsx");
        string codeText = "1234567890";
        BaseEncodeType encodeType = EncodeTypes.Code128;
        int resolution = 300; // DPI for barcode image
        int cellRow = 2;      // Zero‑based row index where the image will be placed
        int cellColumn = 1;   // Zero‑based column index where the image will be placed

        // Create a new Excel workbook
        using (var workbook = new Workbook())
        {
            Worksheet sheet = workbook.Worksheets[0];

            // Generate the barcode image using Aspose.BarCode
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Set image resolution and hide human‑readable text
                generator.Parameters.Resolution = (float)resolution;
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

                using (var bitmap = generator.GenerateBarCodeImage())
                {
                    // Determine image dimensions in pixels
                    int imageWidth = bitmap.Width;
                    int imageHeight = bitmap.Height;

                    // Convert image size to Excel column width / row height (96 DPI base)
                    int cellWidth = (96 * imageWidth) / resolution;
                    int cellHeight = (96 * imageHeight) / resolution;
                    sheet.Cells.SetColumnWidthPixel(cellColumn, cellWidth);
                    sheet.Cells.SetRowHeightPixel(cellRow, cellHeight);

                    // Save bitmap to a memory stream in PNG format
                    using (var imageStream = new MemoryStream())
                    {
                        bitmap.Save(imageStream, ImageFormat.Png);
                        imageStream.Position = 0; // Reset stream position for reading

                        // Insert the picture into the specified cell range
                        int pictureIndex = sheet.Pictures.Add(cellRow, cellColumn, cellRow + 1, cellColumn + 1, imageStream);
                        Picture picture = sheet.Pictures[pictureIndex];
                        picture.Placement = PlacementType.MoveAndSize; // Ensure picture moves/resizes with the cell
                    }
                }
            }

            // Save the workbook to the temporary path
            workbook.Save(outputPath, SaveFormat.Xlsx);
            Console.WriteLine($"Excel file with barcode saved to: {outputPath}");
        }
    }
}