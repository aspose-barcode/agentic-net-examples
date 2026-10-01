// Title: Create and resize a barcode image using Aspose.BarCode and System.Drawing
// Description: Demonstrates generating a Code128 barcode as a bitmap, resizing it with System.Drawing graphics, and saving the result as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode image manipulation category, showing how to use BarcodeGenerator to produce a bitmap, then employ Aspose.Drawing (System.Drawing compatible) classes such as Bitmap, Graphics, and ImageFormat to resize and export the barcode. Typical use cases include preparing barcodes for printing, embedding in UI, or adjusting dimensions for layout constraints. Developers often need to generate barcodes, modify their size, and save them in common image formats.
// Prompt: Create a barcode as a Bitmap, resize it with System.Drawing, then save the image.
// Tags: barcode, code128, resize, bitmap, system.drawing, aspose.barcode, image generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Drawing.Drawing2D;

/// <summary>
/// Demonstrates barcode generation, resizing, and saving using Aspose.BarCode and Aspose.Drawing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, resizes it, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the data to encode in the barcode.
        string codeText = "1234567890";

        // Initialize the barcode generator for Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Generate the barcode image as an Aspose.Drawing.Bitmap.
            using (Bitmap originalBitmap = generator.GenerateBarCodeImage())
            {
                // Target dimensions for the resized image.
                int newWidth = 300;
                int newHeight = 150;

                // Create a new bitmap with the desired size.
                using (Bitmap resizedBitmap = new Bitmap(newWidth, newHeight))
                {
                    // Obtain a graphics object to draw the original barcode onto the new bitmap.
                    using (Graphics graphics = Graphics.FromImage(resizedBitmap))
                    {
                        // Use high-quality bicubic interpolation for smooth scaling.
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                        // Draw and scale the original barcode to fill the new bitmap.
                        graphics.DrawImage(
                            originalBitmap,
                            new Rectangle(0, 0, newWidth, newHeight));
                    }

                    // Build the full output path for the PNG file.
                    string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode_resized.png");

                    // Save the resized bitmap as a PNG image.
                    resizedBitmap.Save(outputPath, ImageFormat.Png);

                    // Inform the user where the file was saved.
                    Console.WriteLine($"Resized barcode saved to: {outputPath}");
                }
            }
        }
    }
}