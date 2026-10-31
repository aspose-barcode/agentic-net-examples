// Title: Generate UPC‑A DataBar Coupon barcode with embedded logo
// Description: Demonstrates creating a UPC‑A barcode that includes a GS1 DataBar coupon, then overlaying a custom logo using System.Drawing graphics, and saving the result as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on combining generated barcodes with custom graphics. It showcases the BarcodeGenerator class for UPC‑A/GS1 DataBar coupon symbology, the use of XDimension settings, and integration with Aspose.Drawing to composite images such as logos. Developers often need to brand barcodes or add promotional graphics, and this pattern illustrates how to merge barcode images with additional visual elements before exporting to common image formats.
// Prompt: Generate a UPC‑A barcode with a DataBar coupon, then combine it with a logo using System.Drawing graphics.
// Tags: upc-a, databar, coupon, logo overlay, png, aspose.barcode, aspose.drawing, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a UPC‑A GS1 DataBar coupon barcode,
/// overlays a simple logo, and saves the combined image as PNG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, composites the logo, and writes the output file.
    /// </summary>
    static void Main()
    {
        // Prepare output directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Create a simple logo bitmap (red circle on white background)
        using (Bitmap logoBmp = new Bitmap(100, 100))
        {
            using (Graphics logoGraphics = Graphics.FromImage(logoBmp))
            {
                // Fill background with white
                logoGraphics.Clear(Color.White);
                // Draw a red circle
                using (SolidBrush brush = new SolidBrush(Color.Red))
                {
                    logoGraphics.FillEllipse(brush, 10, 10, 80, 80);
                }
            }

            // Generate UPC‑A barcode with DataBar coupon
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.UpcaGs1DatabarCoupon, "123456789012(8110)ASPOSE"))
            {
                // Set barcode module size (X‑dimension) to 2 pixels
                generator.Parameters.Barcode.XDimension.Pixels = 2f;

                using (Bitmap barcodeBmp = generator.GenerateBarCodeImage())
                {
                    // Calculate position to center the logo on the barcode
                    int x = (barcodeBmp.Width - logoBmp.Width) / 2;
                    int y = (barcodeBmp.Height - logoBmp.Height) / 2;

                    // Composite logo onto barcode
                    using (Graphics graphics = Graphics.FromImage(barcodeBmp))
                    {
                        graphics.DrawImage(logoBmp, x, y, logoBmp.Width, logoBmp.Height);
                    }

                    // Save final image as PNG
                    string outPath = Path.Combine(outputDir, "UpcA_With_Logo.png");
                    barcodeBmp.Save(outPath, ImageFormat.Png);
                    Console.WriteLine($"Barcode with logo saved to: {outPath}");
                }
            }
        }
    }
}