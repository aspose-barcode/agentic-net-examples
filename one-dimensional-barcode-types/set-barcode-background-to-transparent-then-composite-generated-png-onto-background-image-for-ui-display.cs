// Title: Generate Transparent Barcode and Composite onto Background Image
// Description: Demonstrates creating a Code128 barcode with a transparent background, compositing it onto a solid‑color bitmap, and saving the result as a PNG for UI display.
// Category-Description: This example belongs to the Aspose.BarCode image generation and manipulation category. It showcases the use of BarcodeGenerator (Aspose.BarCode.Generation) to produce a barcode image, configuring its background to be transparent, and then using Aspose.Drawing (Bitmap, Graphics) to composite the barcode onto another image. Typical scenarios include preparing barcode graphics for UI overlays, reports, or custom branding where the barcode must blend with existing backgrounds.
// Prompt: Set barcode background to transparent, then composite the generated PNG onto a background image for UI display.
// Tags: code128, barcode, transparent background, composite, png, aspose.barcode, aspose.drawing, image generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a barcode with a transparent background,
/// compositing it onto a solid‑color background image, and saving the result as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output folder, generates the barcode,
    /// composites it onto a background, and writes the final image to disk.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for the resulting image.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeCompositeDemo");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Create a background bitmap (400x200) with a light‑gray fill.
        using (Bitmap background = new Bitmap(400, 200, PixelFormat.Format32bppArgb))
        {
            using (Graphics bgGraphics = Graphics.FromImage(background))
            {
                bgGraphics.Clear(Color.LightGray);
            }

            // Initialize the barcode generator for Code128 with sample data.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Set the barcode's background to transparent so it blends with the underlying image.
                generator.Parameters.BackColor = Color.Transparent;

                // Generate the barcode image as a bitmap.
                using (Bitmap barcodeBmp = generator.GenerateBarCodeImage())
                {
                    // Composite the barcode onto the background, centering it.
                    using (Graphics compGraphics = Graphics.FromImage(background))
                    {
                        int x = (background.Width - barcodeBmp.Width) / 2;
                        int y = (background.Height - barcodeBmp.Height) / 2;
                        compGraphics.DrawImage(barcodeBmp, x, y, barcodeBmp.Width, barcodeBmp.Height);
                    }
                }
            }

            // Save the combined image as a PNG file.
            string outputPath = Path.Combine(outputDir, "Composite.png");
            background.Save(outputPath, ImageFormat.Png);
            Console.WriteLine($"Composite image saved to: {outputPath}");
        }
    }
}