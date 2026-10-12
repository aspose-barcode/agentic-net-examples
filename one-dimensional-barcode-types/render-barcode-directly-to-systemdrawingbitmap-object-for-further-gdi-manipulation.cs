// Title: Render barcode to Bitmap for GDI+ manipulation
// Description: Demonstrates generating a Code128 barcode, converting it to a System.Drawing.Bitmap, applying simple GDI+ drawing, and saving as PNG.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator with EncodeTypes to create barcodes, retrieve them as Aspose.Drawing.Bitmap objects, and manipulate them using GDI+ (System.Drawing) APIs. Typical use cases include custom graphics overlays, branding, or further image processing before saving or displaying. Developers often need to combine barcode generation with standard .NET drawing tools for advanced visual customization.
// Prompt: Render barcode directly to a System.Drawing.Bitmap object for further GDI+ manipulation.
// Tags: barcode, code128, bitmap, gdi+, image manipulation, aspose.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a barcode, manipulates it with GDI+ and saves to a file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a Code128 barcode, draws a red rectangle, and writes the image to a temporary PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the system's temporary folder
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode.png");

        // Initialize the barcode generator with Code128 symbology and the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Generate the barcode as an Aspose.Drawing.Bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Perform GDI+ manipulation: draw a red rectangle around the barcode
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    var pen = new Pen(Color.Red, 2f);
                    graphics.DrawRectangle(pen, 0, 0, bitmap.Width - 1, bitmap.Height - 1);
                }

                // Save the manipulated bitmap to a PNG file
                using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    bitmap.Save(fs, ImageFormat.Png);
                }
            }
        }

        // Inform the user where the image was saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}