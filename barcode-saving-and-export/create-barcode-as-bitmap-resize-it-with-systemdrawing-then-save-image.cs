// Title: Generate and Resize a Barcode Image using Aspose.BarCode and System.Drawing
// Description: This example creates a Code128 barcode, converts it to a bitmap, resizes it with System.Drawing, and saves the result as a PNG file.
// Category-Description: Demonstrates Aspose.BarCode barcode generation combined with System.Drawing image manipulation. It showcases the use of BarcodeGenerator for creating barcodes, Bitmap and Graphics for resizing, and ImageFormat for saving. Ideal for developers needing to produce custom-sized barcode images for printing, labeling, or UI display.
// Prompt: Create a barcode as a Bitmap, resize it with System.Drawing, then save the image.
// Tags: barcode, code128, resize, bitmap, system.drawing, aspose.barcode, image, png, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates creating a barcode, resizing it, and saving the result as an image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, resizes the bitmap, and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "resized_barcode.png");

        // Initialize the barcode generator with the desired symbology and data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Generate the original barcode as a bitmap image.
            using (Bitmap originalBitmap = generator.GenerateBarCodeImage())
            {
                // Target dimensions for the resized image.
                int newWidth = 300;
                int newHeight = 150;

                // Create a new bitmap with the specified size.
                using (Bitmap resizedBitmap = new Bitmap(newWidth, newHeight))
                {
                    // Obtain a graphics object to draw onto the resized bitmap.
                    using (Graphics graphics = Graphics.FromImage(resizedBitmap))
                    {
                        // Render the original barcode onto the resized bitmap, scaling it to fit.
                        graphics.DrawImage(originalBitmap, 0, 0, newWidth, newHeight);
                    }

                    // Save the resized bitmap to the output path in PNG format.
                    resizedBitmap.Save(outputPath, ImageFormat.Png);
                }
            }
        }

        // Inform the user where the resized barcode image was saved.
        Console.WriteLine($"Resized barcode saved to: {outputPath}");
    }
}