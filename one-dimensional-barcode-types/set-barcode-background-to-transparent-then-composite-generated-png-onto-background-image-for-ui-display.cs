// Title: Generate a transparent Code128 barcode and overlay it on a background image
// Description: Demonstrates how to create a barcode with a transparent background, then composite it onto a PNG background for UI display.
// Category-Description: This example belongs to the Aspose.BarCode image generation and manipulation category. It showcases the use of BarcodeGenerator, BarcodeParameters, and Aspose.Drawing classes to produce a barcode image, adjust its background transparency, and combine it with another image. Developers often need to embed barcodes into UI graphics or reports where the barcode must blend seamlessly with existing visuals.
// Prompt: Set barcode background to transparent, then composite the generated PNG onto a background image for UI display.
// Tags: code128, barcode, transparent background, image compositing, png, aspose.barcode, aspose.drawing, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a transparent Code128 barcode and compositing it onto a background image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates placeholder background if missing, generates barcode, and saves the final composited image.
    /// </summary>
    static void Main()
    {
        // Define file paths for the background, barcode, and final composited image
        string backgroundPath = "background.png";
        string barcodePath = "barcode.png";
        string finalPath = "final.png";

        // Ensure a background image exists; create a simple placeholder if it does not
        if (!File.Exists(backgroundPath))
        {
            using (var placeholder = new Bitmap(400, 200))
            {
                using (var g = Graphics.FromImage(placeholder))
                {
                    // Fill the placeholder with a light gray color
                    g.Clear(Aspose.Drawing.Color.LightGray);
                }
                // Save the placeholder as a PNG file
                placeholder.Save(backgroundPath, ImageFormat.Png);
            }
        }

        // Generate a Code128 barcode with a transparent background
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the barcode's background to transparent
            generator.Parameters.BackColor = Aspose.Drawing.Color.Transparent;

            using (Bitmap barcodeBmp = generator.GenerateBarCodeImage())
            {
                // Optionally save the standalone barcode image
                barcodeBmp.Save(barcodePath, ImageFormat.Png);

                // Load the background image onto which the barcode will be drawn
                using (Bitmap backgroundBmp = (Bitmap)Image.FromFile(backgroundPath))
                {
                    // Compute coordinates to center the barcode on the background
                    int posX = (backgroundBmp.Width - barcodeBmp.Width) / 2;
                    int posY = (backgroundBmp.Height - barcodeBmp.Height) / 2;

                    // Draw the barcode onto the background at the calculated position
                    using (Graphics graphics = Graphics.FromImage(backgroundBmp))
                    {
                        graphics.DrawImage(barcodeBmp, posX, posY, barcodeBmp.Width, barcodeBmp.Height);
                    }

                    // Save the final composited image as a PNG file
                    backgroundBmp.Save(finalPath, ImageFormat.Png);
                }
            }
        }
    }
}