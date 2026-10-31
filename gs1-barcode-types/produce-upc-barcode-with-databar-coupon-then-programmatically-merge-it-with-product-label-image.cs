// Title: Generate UPC‑A DataBar Coupon and merge with product label image
// Description: Demonstrates creating a UPC‑A barcode with a GS1 DataBar coupon symbology and combining it with a simple product label graphic.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and image manipulation category. It showcases the BarcodeGenerator class for UPC‑A DataBar coupons, the use of Aspose.Drawing to create and compose bitmap images, and typical scenarios where developers need to embed barcodes onto product labels or packaging designs. Ideal for developers integrating barcode printing into label workflows.
// Prompt: Produce a UPC‑A barcode with a DataBar coupon, then programmatically merge it with a product label image.
// Tags: upc-a, databar, coupon, barcode generation, image merge, aspose.barcode, aspose.drawing, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a UPC‑A GS1 DataBar coupon barcode and merging it with a product label image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates output directory, generates the merged image, and reports the result.
    /// </summary>
    static void Main()
    {
        try
        {
            // Determine output folder path and ensure it exists
            string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // Full path for the merged image file
            string mergedPath = Path.Combine(outputDir, "MergedBarcodeLabel.png");

            // Generate barcode, create label, merge them, and save to file
            GenerateUpcAWithDataBarCouponAndMerge(mergedPath);

            Console.WriteLine($"Merged image saved to: {mergedPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a UPC‑A GS1 DataBar coupon barcode, creates a placeholder product label,
    /// merges both images vertically, and saves the result to the specified path.
    /// </summary>
    /// <param name="outputPath">File path where the merged PNG image will be saved.</param>
    static void GenerateUpcAWithDataBarCouponAndMerge(string outputPath)
    {
        // Generate UPC‑A DataBar coupon barcode into a memory stream
        using (var barcodeStream = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1DatabarCoupon, "123456789012(8110)ASPOSE"))
            {
                // Set barcode module (X) size to 2 pixels for better readability
                generator.Parameters.Barcode.XDimension.Pixels = 2;

                // Save barcode image as PNG into the stream
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            // Reset stream position for reading
            barcodeStream.Position = 0;

            // Load the generated barcode image from the stream
            using (var barcodeBitmap = new Bitmap(barcodeStream))
            {
                // Create a simple product label bitmap (placeholder text)
                int labelWidth = barcodeBitmap.Width;
                int labelHeight = 100;
                using (var labelBitmap = new Bitmap(labelWidth, labelHeight))
                {
                    using (var gLabel = Graphics.FromImage(labelBitmap))
                    {
                        // Fill background with white
                        gLabel.Clear(Color.White);

                        // Draw placeholder label text
                        gLabel.DrawString(
                            "Product Label",
                            new Font("Arial", 24f),
                            new SolidBrush(Color.Black),
                            new PointF(10f, 30f));
                    }

                    // Determine dimensions for the merged image (barcode on top, label below)
                    int mergedWidth = Math.Max(barcodeBitmap.Width, labelBitmap.Width);
                    int mergedHeight = barcodeBitmap.Height + labelBitmap.Height;

                    using (var mergedBitmap = new Bitmap(mergedWidth, mergedHeight))
                    {
                        using (var gMerged = Graphics.FromImage(mergedBitmap))
                        {
                            // Fill merged canvas with white background
                            gMerged.Clear(Color.White);

                            // Draw barcode at the top
                            gMerged.DrawImage(barcodeBitmap, 0, 0);

                            // Draw label directly beneath the barcode
                            gMerged.DrawImage(labelBitmap, 0, barcodeBitmap.Height);
                        }

                        // Save the final merged image as PNG
                        mergedBitmap.Save(outputPath, ImageFormat.Png);
                    }
                }
            }
        }
    }
}