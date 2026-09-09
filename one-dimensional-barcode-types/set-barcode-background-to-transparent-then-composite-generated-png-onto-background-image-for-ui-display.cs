// Title: Generate Transparent Barcode and Composite onto Background Image
// Description: Demonstrates creating a Code128 barcode with a transparent background, saving it as PNG, and overlaying it onto a custom background image for UI display.
// Category-Description: This example belongs to the Aspose.BarCode image generation and manipulation category. It showcases the use of BarcodeGenerator, BarcodeParameters, and Aspose.Drawing classes to produce a barcode image, adjust visual properties such as background transparency, and combine it with other graphics. Developers often need to embed barcodes into UI components or reports where a seamless visual integration with existing backgrounds is required.
// Prompt: Set barcode background to transparent, then composite the generated PNG onto a background image for UI display.
// Tags: code128, barcode, transparent background, image compositing, png, aspose.barcode, aspose.drawing, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a barcode with a transparent background,
/// composites it onto a background image, and saves the result.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates images and writes their file paths to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all generated files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for background, barcode, and composite images
        string backgroundPath = Path.Combine(tempDir, "background.png");
        string barcodePath = Path.Combine(tempDir, "barcode.png");
        string compositePath = Path.Combine(tempDir, "composite.png");

        // --------------------------------------------------------------------
        // Create a simple background image (light gray rectangle)
        // --------------------------------------------------------------------
        using (Bitmap backgroundBitmap = new Bitmap(400, 200))
        {
            using (Graphics g = Graphics.FromImage(backgroundBitmap))
            {
                g.Clear(Color.LightGray);
            }
            backgroundBitmap.Save(backgroundPath, ImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Generate a Code128 barcode with a transparent background
        // --------------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the barcode's background to transparent
            generator.Parameters.BackColor = Color.Transparent;

            using (MemoryStream ms = new MemoryStream())
            {
                // Save the barcode image to a memory stream in PNG format
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;

                using (Bitmap barcodeBitmap = new Bitmap(ms))
                {
                    // Persist the barcode image to disk
                    barcodeBitmap.Save(barcodePath, ImageFormat.Png);

                    // ----------------------------------------------------------------
                    // Load the background image and composite the barcode onto it
                    // ----------------------------------------------------------------
                    using (Bitmap bg = new Bitmap(backgroundPath))
                    {
                        using (Graphics graphics = Graphics.FromImage(bg))
                        {
                            // Center the barcode on the background
                            int x = (bg.Width - barcodeBitmap.Width) / 2;
                            int y = (bg.Height - barcodeBitmap.Height) / 2;
                            graphics.DrawImage(barcodeBitmap, x, y);
                        }
                        // Save the final composite image
                        bg.Save(compositePath, ImageFormat.Png);
                    }
                }
            }
        }

        // Output the locations of the generated files
        Console.WriteLine("Background image: " + backgroundPath);
        Console.WriteLine("Barcode image (transparent background): " + barcodePath);
        Console.WriteLine("Composite image: " + compositePath);
    }
}