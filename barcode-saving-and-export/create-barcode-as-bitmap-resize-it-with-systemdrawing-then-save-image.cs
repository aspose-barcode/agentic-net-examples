// Title: Generate and Resize a Barcode Image Using Aspose.BarCode and System.Drawing
// Description: This example creates a Code128 barcode, converts it to a bitmap, enlarges it using System.Drawing, and saves the result as a PNG file.
// Category-Description: Demonstrates Aspose.BarCode barcode generation combined with System.Drawing image manipulation. The example uses BarcodeGenerator (Aspose.BarCode.Generation) to produce a bitmap, then employs Bitmap, Graphics, and ImageFormat (Aspose.Drawing) to resize and export the image. Typical scenarios include preparing high‑resolution barcodes for printing, UI display, or further graphic processing.
// Prompt: Create a barcode as a Bitmap, resize it with System.Drawing, then save the image.
// Tags: barcode, code128, generation, resize, system.drawing, bitmap, png, aspose.barcode, image-processing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate a barcode, resize it using System.Drawing, and save it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, doubles its dimensions, and writes the result to disk.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "resized_barcode.png");

        // Ensure the output directory exists; create it if necessary.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Initialize the barcode generator with Code128 symbology and the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Generate the barcode as a bitmap image.
            using (Bitmap originalBitmap = generator.GenerateBarCodeImage())
            {
                // Calculate new dimensions (double the original width and height).
                int newWidth = originalBitmap.Width * 2;
                int newHeight = originalBitmap.Height * 2;

                // Create a new bitmap with the calculated size.
                using (Bitmap resizedBitmap = new Bitmap(newWidth, newHeight))
                {
                    // Obtain a graphics object to draw onto the new bitmap.
                    using (Graphics graphics = Graphics.FromImage(resizedBitmap))
                    {
                        // Render the original barcode onto the resized bitmap, scaling it to fill the new dimensions.
                        graphics.DrawImage(originalBitmap, new Rectangle(0, 0, newWidth, newHeight));
                    }

                    // Save the resized bitmap as a PNG file to the specified path.
                    resizedBitmap.Save(outputPath, ImageFormat.Png);
                }
            }
        }

        // Inform the user where the resized barcode image has been saved.
        Console.WriteLine($"Resized barcode saved to: {outputPath}");
    }
}