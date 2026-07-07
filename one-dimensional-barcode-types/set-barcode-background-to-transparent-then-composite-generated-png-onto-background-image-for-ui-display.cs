// Title: Transparent barcode compositing onto background image
// Description: Demonstrates generating a Code128 barcode with a transparent background and overlaying it onto a background PNG for UI display.
// Category-Description: This example belongs to the Aspose.BarCode image generation and manipulation category. It showcases using BarcodeGenerator, setting BackColor to Transparent, adjusting size with AutoSizeMode, and compositing the resulting bitmap with Aspose.Drawing graphics. Developers often need to create barcodes that blend seamlessly into UI designs, requiring transparent backgrounds and custom image composition.
// Prompt: Set barcode background to transparent, then composite the generated PNG onto a background image for UI display.
// Tags: code128, transparent background, image compositing, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a Code128 barcode with a transparent background,
/// then composites it onto an existing background image and saves the result.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, composites it, and writes the output file.
    /// </summary>
    static void Main()
    {
        // Paths for the generated barcode, background image and final composite image
        const string barcodePath = "barcode.png";
        const string backgroundPath = "background.png";
        const string outputPath = "composite.png";

        // Verify that the background image exists before proceeding
        if (!File.Exists(backgroundPath))
        {
            Console.WriteLine($"Background image not found: {backgroundPath}");
            return;
        }

        // Create a barcode generator configured for Code128 and set a transparent background
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Make the barcode background transparent
            generator.Parameters.BackColor = Aspose.Drawing.Color.Transparent;

            // Use interpolation mode to control the exact size via ImageWidth/ImageHeight
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Generate the barcode image as a bitmap
            using (Bitmap barcodeBitmap = generator.GenerateBarCodeImage())
            {
                // Load the background image onto which the barcode will be drawn
                using (Image backgroundImage = Image.FromFile(backgroundPath))
                {
                    // Obtain a graphics object for drawing onto the background
                    using (Graphics graphics = Graphics.FromImage(backgroundImage))
                    {
                        // Draw the barcode at the top‑left corner (0,0) with its original dimensions
                        graphics.DrawImage(barcodeBitmap, 0, 0, barcodeBitmap.Width, barcodeBitmap.Height);
                    }

                    // Save the composited image as PNG to preserve transparency where applicable
                    backgroundImage.Save(outputPath, ImageFormat.Png);
                }
            }
        }

        Console.WriteLine($"Composite image saved to: {outputPath}");
    }
}