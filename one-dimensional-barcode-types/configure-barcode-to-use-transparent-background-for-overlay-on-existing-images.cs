// Title: Generate Barcode with Transparent Background and Overlay on Image
// Description: Demonstrates creating a barcode with a transparent background, compositing it onto an existing image, and saving the combined result as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation and image manipulation category. It showcases the use of BarcodeGenerator to produce a barcode image, configuring its background to be transparent, and then using Aspose.Drawing's Bitmap and Graphics classes to overlay the barcode onto another image. Typical use cases include adding barcodes to product photos, documents, or UI elements without obscuring the underlying graphics. Developers often need to combine barcode generation with custom graphics workflows, and this snippet illustrates the essential API calls for such scenarios.
// Prompt: Configure barcode to use a transparent background for overlay on existing images.
// Tags: barcode, code128, transparent background, overlay, image composition, png, aspose.barcode, aspose.drawing, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a barcode with a transparent background,
/// overlays it onto a base image, and saves the combined image as PNG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the images and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define dimensions for the base image (200x200 pixels)
        int baseWidth = 200;
        int baseHeight = 200;

        // Create a bitmap that will serve as the background canvas
        using (Bitmap baseImage = new Bitmap(baseWidth, baseHeight))
        {
            // Fill the base image with a light gray color
            using (Graphics gBase = Graphics.FromImage(baseImage))
            {
                gBase.Clear(Color.FromArgb(200, 200, 200));
            }

            // Text to encode in the barcode
            string codeText = "1234567890";

            // Initialize the barcode generator for Code128 symbology
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Configure the barcode to have a transparent background
                generator.Parameters.BackColor = Color.Transparent;

                // Optional: set the color of the bars to black
                generator.Parameters.Barcode.BarColor = Color.Black;

                // Generate the barcode image as a bitmap
                using (Bitmap barcodeBmp = generator.GenerateBarCodeImage())
                {
                    // Overlay the barcode onto the base image at coordinates (50, 50)
                    using (Graphics gOverlay = Graphics.FromImage(baseImage))
                    {
                        gOverlay.DrawImage(barcodeBmp, new Point(50, 50));
                    }
                }
            }

            // Determine the output file path in the current working directory
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "output.png");

            // Save the combined image as a PNG file
            using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                baseImage.Save(fs, ImageFormat.Png);
            }

            // Inform the user where the file was saved
            Console.WriteLine($"Combined image saved to: {outputPath}");
        }
    }
}