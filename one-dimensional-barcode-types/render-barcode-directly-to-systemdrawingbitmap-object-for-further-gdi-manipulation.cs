// Title: Render barcode to Bitmap for GDI+ manipulation
// Description: Demonstrates generating a Code128 barcode, drawing a red border using GDI+, and saving it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator to create barcodes, obtain a System.Drawing.Bitmap for further graphics operations, and employ Aspose.Drawing classes such as Graphics, Pen, and ImageFormat. Typical use cases include custom barcode styling, overlaying graphics, or integrating barcodes into existing GDI+ workflows. Developers often need to render barcodes to in‑memory images for additional processing before saving or displaying.
// Prompt: Render barcode directly to a System.Drawing.Bitmap object for further GDI+ manipulation.
// Tags: barcode, code128, bitmap, gdi+, drawing, aspose.barcode, aspose.drawing, png, image generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates rendering a barcode to a Bitmap and applying GDI+ drawing operations.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, draws a red rectangle around it, and saves the result as PNG.
    /// </summary>
    static void Main()
    {
        // Determine temporary output file path
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode.png");
        // Ensure the directory exists
        string dir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        // Create a barcode generator for Code128 with the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Generate the barcode as a Bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Obtain a Graphics object to draw on the bitmap
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    // Create a red pen with thickness 5
                    using (Pen pen = new Pen(Aspose.Drawing.Color.Red, 5f))
                    {
                        // Draw a rectangle border around the barcode image
                        graphics.DrawRectangle(pen, 0, 0, bitmap.Width - 1, bitmap.Height - 1);
                    }
                }

                // Save the modified bitmap to PNG file
                bitmap.Save(outputPath, ImageFormat.Png);
            }
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}