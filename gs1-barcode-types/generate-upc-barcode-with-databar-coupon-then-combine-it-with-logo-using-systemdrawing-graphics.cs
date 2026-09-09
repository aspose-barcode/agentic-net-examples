// Title: Generate UPC-A DataBar Coupon barcode with embedded logo
// Description: Demonstrates creating a UPC‑A barcode with a GS1 DataBar coupon symbology and overlaying a custom logo using System.Drawing.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on combining generated barcodes with graphics. It showcases the BarcodeGenerator class for UPC‑A DataBar coupons, the use of Aspose.Drawing for image manipulation, and typical scenarios where developers need to add branding or logos to barcodes before saving them as PNG files. Such techniques are common in retail and marketing applications where visual identity must accompany machine‑readable codes.
// Prompt: Generate a UPC‑A barcode with a DataBar coupon, then combine it with a logo using System.Drawing graphics.
// Tags: upc-a, databar, coupon, barcode generation, logo overlay, aspose.barcode, aspose.drawing, png, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a UPC‑A DataBar coupon barcode and adds a logo overlay.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, draws a simple logo, merges them, and saves the result as PNG.
    /// </summary>
    static void Main()
    {
        // Prepare output directory and file path
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "UpcA_With_Logo.png");

        // Initialize barcode generator for UPC‑A GS1 DataBar coupon with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1DatabarCoupon, "123456789012(8110)ASPOSE"))
        {
            // Configure barcode appearance
            generator.Parameters.Barcode.XDimension.Pixels = 2;                 // Width of the smallest bar
            generator.Parameters.Barcode.Coupon.SupplementSpace.Pixels = 30;   // Space for the supplemental data

            // Generate the barcode image
            using (var barcodeImage = generator.GenerateBarCodeImage())
            {
                // Create a simple logo bitmap (100x100) with white background and black text
                using (var logoImage = new Bitmap(100, 100))
                {
                    using (var gLogo = Graphics.FromImage(logoImage))
                    {
                        gLogo.Clear(Color.White);
                        using (var font = new Font("Helvetica", 20f))
                        using (var brush = new SolidBrush(Color.Black))
                        {
                            gLogo.DrawString("Logo", font, brush, new PointF(10, 40));
                        }
                    }

                    // Overlay the logo onto the barcode image at the bottom‑right corner
                    using (var g = Graphics.FromImage(barcodeImage))
                    {
                        int x = barcodeImage.Width - logoImage.Width - 10; // 10‑pixel margin from right edge
                        int y = barcodeImage.Height - logoImage.Height - 10; // 10‑pixel margin from bottom edge
                        g.DrawImage(logoImage, x, y, logoImage.Width, logoImage.Height);
                    }

                    // Save the combined image as PNG
                    barcodeImage.Save(outputPath, ImageFormat.Png);
                }
            }
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode with logo saved to: {outputPath}");
    }
}