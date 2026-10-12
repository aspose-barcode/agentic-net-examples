// Title: Embed a logo at the center of a Han Xin barcode
// Description: Demonstrates how to generate a Han Xin barcode and overlay a custom logo image without compromising scan readability.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on image manipulation and error correction. It showcases the use of BarcodeGenerator, HanXin parameters, and System.Drawing (Aspose.Drawing) to create barcodes with embedded graphics, a common requirement for branding and product labeling. Developers often need to combine barcode data with logos while maintaining scan reliability.
// Prompt: Provide option to embed logo image at center of Han Xin barcode without affecting readability.
// Tags: hanxin, logo, png, barcodegenerator, bitmap, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates embedding a logo into a Han Xin barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Han Xin barcode, creates a logo bitmap, and merges them.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for all generated files
        string outputDir = Path.Combine(Path.GetTempPath(), "HanXinLogoDemo");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define file paths for the logo, the raw barcode, and the final combined image
        string logoPath = Path.Combine(outputDir, "logo.png");
        string barcodePath = Path.Combine(outputDir, "hanxin.png");
        string finalPath = Path.Combine(outputDir, "hanxin_with_logo.png");

        // --------------------------------------------------------------------
        // Create a simple logo bitmap (50x50 pixels, red background with white text)
        // --------------------------------------------------------------------
        using (var logoBmp = new Bitmap(50, 50))
        {
            using (var graphics = Graphics.FromImage(logoBmp))
            {
                graphics.Clear(Color.Red);
                using (var font = new Font("Arial", 12f))
                {
                    graphics.DrawString("LOGO", font, new SolidBrush(Color.White), new PointF(5f, 15f));
                }
            }
            // Save the logo to disk for later use
            logoBmp.Save(logoPath, ImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Generate a Han Xin barcode with optional error correction level
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, "Sample123"))
        {
            // Increase error correction to improve readability after logo overlay
            generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L2;

            using (var barcodeBmp = generator.GenerateBarCodeImage())
            {
                // Load the previously saved logo bitmap
                using (var logoBmp = new Bitmap(logoPath))
                {
                    // Calculate coordinates to center the logo on the barcode image
                    int x = (barcodeBmp.Width - logoBmp.Width) / 2;
                    int y = (barcodeBmp.Height - logoBmp.Height) / 2;

                    // Draw the logo onto the barcode at the calculated position
                    using (var graphics = Graphics.FromImage(barcodeBmp))
                    {
                        graphics.DrawImage(logoBmp, x, y, logoBmp.Width, logoBmp.Height);
                    }

                    // Save the combined image (barcode with embedded logo) to disk
                    barcodeBmp.Save(finalPath, ImageFormat.Png);
                }
            }
        }

        // Inform the user where the final image was saved
        Console.WriteLine("Barcode with logo saved to:");
        Console.WriteLine(finalPath);
    }
}