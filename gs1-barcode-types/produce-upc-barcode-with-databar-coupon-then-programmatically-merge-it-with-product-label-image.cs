// Title: Generate UPC‑A DataBar Coupon barcode and merge with product label
// Description: Demonstrates creating a UPC‑A barcode with a DataBar coupon symbology and combining it with a product label image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and image manipulation category. It shows how to use BarcodeGenerator with EncodeTypes.UpcaGs1DatabarCoupon, configure barcode parameters, render the barcode to a bitmap, and merge it onto a custom label using Aspose.Drawing. Developers often need to create combined label graphics for retail packaging, where a barcode is placed alongside product information.
// Prompt: Produce a UPC‑A barcode with a DataBar coupon, then programmatically merge it with a product label image.
// Tags: upc-a, databar, coupon, barcode generation, image merging, aspose.barcode, aspose.drawing, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a UPC‑A DataBar coupon barcode,
/// draws a simple product label, and merges the barcode onto the label.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directories, generates the barcode,
    /// composes a label image, merges them, and writes the results to disk.
    /// </summary>
    static void Main()
    {
        // Define a unique temporary output folder.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Paths for the individual barcode image and the final merged label.
        string barcodePath = Path.Combine(outputDir, "barcode.png");
        string mergedPath = Path.Combine(outputDir, "merged_label.png");

        // Initialize the barcode generator for UPC‑A DataBar coupon symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1DatabarCoupon, "123456789012(8110)ASPOSE"))
        {
            // Set the X‑dimension (module width) to 2 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Generate the barcode image as a bitmap.
            using (Bitmap barcodeBmp = generator.GenerateBarCodeImage())
            {
                // Save the barcode bitmap to a PNG file.
                barcodeBmp.Save(barcodePath, ImageFormat.Png);

                // Create a blank label bitmap (400x200) with 32‑bit ARGB pixel format.
                using (var labelBmp = new Bitmap(400, 200, PixelFormat.Format32bppArgb))
                {
                    // Obtain a graphics object to draw on the label bitmap.
                    using (var graphics = Graphics.FromImage(labelBmp))
                    {
                        // Fill the background with white.
                        graphics.Clear(Color.White);

                        // Draw product information text onto the label.
                        using (var font = new Font("Arial", 12f))
                        using (var brush = new SolidBrush(Color.Black))
                        {
                            graphics.DrawString("Product Name", font, brush, new PointF(10f, 10f));
                            graphics.DrawString("Price: $9.99", font, brush, new PointF(10f, 30f));
                        }

                        // Draw the generated barcode onto the label at the specified location.
                        graphics.DrawImage(barcodeBmp, new Rectangle(150, 50, barcodeBmp.Width, barcodeBmp.Height));
                    }

                    // Save the merged label image to a PNG file.
                    labelBmp.Save(mergedPath, ImageFormat.Png);
                }
            }
        }

        // Output the locations of the generated files.
        Console.WriteLine("Barcode image saved to: " + barcodePath);
        Console.WriteLine("Merged label image saved to: " + mergedPath);
    }
}