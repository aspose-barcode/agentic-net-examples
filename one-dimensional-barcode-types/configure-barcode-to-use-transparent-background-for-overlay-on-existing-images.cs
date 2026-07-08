// Title: Generate a Code128 barcode with transparent background and overlay on an image
// Description: Demonstrates how to create a Code128 barcode with a transparent background and draw it onto an existing image, saving the result as a PNG.
// Category-Description: This example belongs to the Aspose.BarCode image overlay category, illustrating the use of BarcodeGenerator, BarcodeParameters, and Aspose.Drawing to combine barcodes with background images. Typical scenarios include adding barcodes to product photos, documents, or UI elements without obscuring the underlying graphics. Developers often need to control barcode colors and transparency for seamless integration.
// Prompt: Configure barcode to use a transparent background for overlay on existing images.
// Tags: code128, transparent background, png, barcodegenerator, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode with a transparent background and overlaying it onto an existing image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Loads or creates a base image, generates a transparent barcode, draws it onto the base, and saves the result.
    /// </summary>
    static void Main()
    {
        // Define file paths for the background image and the resulting combined image.
        string inputPath = "input.png";
        string outputPath = "output.png";

        // Load the existing background image; if it does not exist, create a simple white canvas.
        Image baseImage;
        if (File.Exists(inputPath))
        {
            baseImage = Image.FromFile(inputPath);
        }
        else
        {
            // Create a white bitmap of size 400x200 as a placeholder background.
            baseImage = new Bitmap(400, 200);
            using (Graphics g = Graphics.FromImage(baseImage))
            {
                g.Clear(Color.White);
            }
        }

        // Ensure the base image is disposed after processing.
        using (baseImage)
        {
            // Initialize a barcode generator for Code128 with sample data.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                // Configure the barcode to have a transparent background.
                generator.Parameters.BackColor = Color.Transparent;
                // Optionally set the bar (foreground) color to black.
                generator.Parameters.Barcode.BarColor = Color.Black;

                // Generate the barcode as a bitmap.
                using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
                {
                    // Draw the barcode onto the base image at coordinates (10,10).
                    using (Graphics graphics = Graphics.FromImage(baseImage))
                    {
                        graphics.DrawImage(barcodeImage, new Point(10, 10));
                    }
                }
            }

            // Save the final image with the transparent barcode overlay as a PNG file.
            baseImage.Save(outputPath, ImageFormat.Png);
        }

        Console.WriteLine($"Combined image saved to {outputPath}");
    }
}