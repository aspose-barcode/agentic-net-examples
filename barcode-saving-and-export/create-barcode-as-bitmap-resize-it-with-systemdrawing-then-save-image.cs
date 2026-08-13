// Title: Generate and Resize a Code128 Barcode as PNG
// Description: Creates a Code128 barcode, resizes it using System.Drawing, and saves it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode image generation and manipulation category. It demonstrates how to use BarcodeGenerator (Aspose.BarCode.Generation) to produce a barcode bitmap, employ Aspose.Drawing (System.Drawing compatible) for resizing, and persist the result with Aspose.Drawing.Imaging. Developers often need to generate barcodes, adjust dimensions for UI or printing, and export them in common image formats.
// Prompt: Create a barcode as a Bitmap, resize it with System.Drawing, then save the image.
// Tags: code128, generate, resize, png, aspose.barcode, aspose.drawing, aspose.drawing.imaging

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation, resizing, and saving using Aspose.BarCode and Aspose.Drawing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, resizes it, and writes the result to a PNG file.
    /// </summary>
    static void Main()
    {
        // Output file path for the resized barcode image
        const string outputPath = "barcode_resized.png";

        // Initialize a barcode generator for Code128 with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Produce the barcode as an Aspose.Drawing.Bitmap
            using (var originalBitmap = generator.GenerateBarCodeImage())
            {
                // Target dimensions for the resized image
                const int newWidth = 300;
                const int newHeight = 150;

                // Create a blank bitmap with the desired size
                using (var resizedBitmap = new Bitmap(newWidth, newHeight))
                {
                    // Obtain a graphics object to draw onto the new bitmap
                    using (var graphics = Graphics.FromImage(resizedBitmap))
                    {
                        // Draw and scale the original barcode onto the resized bitmap
                        graphics.DrawImage(
                            originalBitmap,
                            new Rectangle(0, 0, newWidth, newHeight));
                    }

                    // Save the resized bitmap as a PNG file
                    using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        resizedBitmap.Save(fileStream, ImageFormat.Png);
                    }
                }
            }
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Resized barcode saved to '{Path.GetFullPath(outputPath)}'");
    }
}