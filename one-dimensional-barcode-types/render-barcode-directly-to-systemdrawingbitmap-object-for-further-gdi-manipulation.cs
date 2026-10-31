// Title: Render barcode to Bitmap for GDI+ manipulation
// Description: Demonstrates generating a Code128 barcode, rendering it directly to a System.Drawing.Bitmap, and saving it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator with EncodeTypes to create barcodes, obtain a Bitmap via GenerateBarCodeImage, and apply GDI+ drawing operations. Developers commonly need to customize barcode images with graphics primitives before saving or displaying them.
// Prompt: Render barcode directly to a System.Drawing.Bitmap object for further GDI+ manipulation.
// Tags: barcode, code128, bitmap, gdi+, aspose.barcode, image generation, png, graphics

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode, draws a red rectangle around it,
/// and saves the result as a PNG image using Aspose.BarCode and GDI+.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, applies GDI+ drawing,
    /// and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // The text to encode in the barcode.
        string codeText = "12345678";

        // Initialize the barcode generator with Code128 symbology.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set the X-dimension (module width) to 2 points for better readability.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Generate the barcode directly as a Bitmap for GDI+ manipulation.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Obtain a Graphics object to draw on the bitmap.
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    // Create a red pen with a thickness of 2 pixels.
                    using (Pen pen = new Pen(Aspose.Drawing.Color.Red, 2f))
                    {
                        // Draw a rectangle around the entire barcode image.
                        graphics.DrawRectangle(pen, 0, 0, bitmap.Width - 1, bitmap.Height - 1);
                    }
                }

                // Determine the output file path in the current working directory.
                string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

                // Save the bitmap as a PNG file.
                using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    bitmap.Save(fileStream, ImageFormat.Png);
                }

                // Inform the user where the image was saved.
                Console.WriteLine($"Barcode image saved to {outputPath}");
            }
        }
    }
}