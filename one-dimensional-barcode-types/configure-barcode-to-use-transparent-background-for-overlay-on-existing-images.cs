// Title: Generate barcode with transparent background and overlay on an image
// Description: Demonstrates creating a Code128 barcode with a transparent background and drawing it onto an existing PNG image.
// Category-Description: This example belongs to the Aspose.BarCode image manipulation category, illustrating how to use BarcodeGenerator, set background transparency, and combine generated barcodes with other graphics. Typical use cases include overlaying barcodes on product photos, documents, or UI elements where the underlying image must remain visible. Developers often need to adjust colors, positions, and save the result in common image formats.
// Prompt: Configure barcode to use a transparent background for overlay on existing images.
// Tags: barcode, code128, transparent background, image overlay, aspose.barcode, aspose.drawing, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a barcode with a transparent background
/// and draws it onto an existing image, saving the combined result.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define file paths for the source image and the final combined image.
        string baseImagePath = "base.png";
        string outputImagePath = "combined.png";

        // If the base image does not exist, create a simple placeholder image.
        if (!File.Exists(baseImagePath))
        {
            using (Bitmap baseBmp = new Bitmap(300, 200))
            {
                using (Graphics g = Graphics.FromImage(baseBmp))
                {
                    // Fill the placeholder with a light gray background.
                    g.Clear(Aspose.Drawing.Color.LightGray);
                }
                // Save the placeholder as a PNG file.
                baseBmp.Save(baseImagePath, ImageFormat.Png);
            }
        }

        // Load the existing base image into a Bitmap object.
        using (Bitmap baseImage = (Bitmap)Image.FromFile(baseImagePath))
        {
            // Initialize the barcode generator for Code128 with the desired text.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                // Set the barcode background to transparent so the underlying image shows through.
                generator.Parameters.BackColor = Aspose.Drawing.Color.Transparent;
                // Optionally set the bar (foreground) color to black.
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

                // Generate the barcode as a Bitmap.
                using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
                {
                    // Draw the barcode onto the base image at the specified coordinates.
                    using (Graphics graphics = Graphics.FromImage(baseImage))
                    {
                        int posX = 50; // X-coordinate of the barcode's top‑left corner.
                        int posY = 50; // Y-coordinate of the barcode's top‑left corner.
                        graphics.DrawImage(barcodeImage, posX, posY, barcodeImage.Width, barcodeImage.Height);
                    }
                }
            }

            // Save the combined image (base + barcode) as a PNG file.
            baseImage.Save(outputImagePath, ImageFormat.Png);
        }

        // Inform the user where the output file was saved.
        Console.WriteLine("Barcode with transparent background overlaid and saved to: " + Path.GetFullPath(outputImagePath));
    }
}