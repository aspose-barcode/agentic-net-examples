// Title: Overlay Barcode with Transparent Background on an Image
// Description: Demonstrates generating a Code128 barcode with a transparent background and drawing it onto an existing PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to create barcodes with custom visual properties and combine them with existing graphics using Aspose.Drawing. It highlights the use of BarcodeGenerator, setting Parameters.BackColor to transparent, and drawing the generated bitmap onto another image—common tasks for developers who need to embed barcodes into product photos, marketing materials, or UI overlays.
// Prompt: Configure barcode to use a transparent background for overlay on existing images.
// Tags: barcode, code128, transparent background, overlay, image, aspose.barcode, aspose.drawing, png, generation

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a Code128 barcode with a transparent background
/// and overlays it onto an existing image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, draws it onto the background image,
    /// and saves the combined result.
    /// </summary>
    static void Main()
    {
        // Paths for the background image and the resulting image
        string backgroundPath = "background.png";
        string outputPath = "output.png";

        // Verify that the background image exists
        if (!File.Exists(backgroundPath))
        {
            Console.WriteLine("Background image not found: " + backgroundPath);
            return;
        }

        // Load the background image
        using (Bitmap background = (Bitmap)Image.FromFile(backgroundPath))
        {
            // Create a barcode generator for Code128 with sample text
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                // Set the barcode background to transparent so underlying image shows through
                generator.Parameters.BackColor = Color.Transparent;

                // Optional: set the bar (foreground) color to black
                generator.Parameters.Barcode.BarColor = Color.Black;

                // Generate the barcode image as a bitmap
                using (Bitmap barcode = generator.GenerateBarCodeImage())
                {
                    // Draw the barcode onto the background image at position (0,0)
                    using (Graphics graphics = Graphics.FromImage(background))
                    {
                        graphics.DrawImage(barcode, new Point(0, 0));
                    }
                }
            }

            // Save the combined image as PNG to preserve transparency
            using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                background.Save(outStream, ImageFormat.Png);
            }
        }

        Console.WriteLine("Barcode overlay saved to " + outputPath);
    }
}