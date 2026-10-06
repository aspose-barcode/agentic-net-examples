// Title: Generate UPC‑A DataBar Coupon and Merge with Product Label Image
// Description: Demonstrates how to create a UPC‑A barcode with a GS1 DataBar coupon, render it to PNG, and combine it with a custom product label using Aspose.BarCode and Aspose.Drawing.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and image manipulation category. It showcases the BarcodeGenerator class for creating GS1 DataBar coupon barcodes, the use of Aspose.Drawing to draw graphics, and techniques for merging barcode images with other graphics. Developers working on packaging, retail, or inventory systems often need to embed barcodes into product labels, and this snippet provides a concise reference for that workflow.
// Prompt: Produce a UPC‑A barcode with a DataBar coupon, then programmatically merge it with a product label image.
// Tags: upc-a, databar, barcode generation, image merging, aspose.barcode, aspose.drawing, png, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a UPC‑A GS1 DataBar coupon barcode,
/// draws a simple product label, and merges the barcode onto the label.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates output folder, generates barcode, builds label,
    /// merges them, and saves the final image.
    /// </summary>
    static void Main()
    {
        // Prepare output folder
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputFolder);

        // Generate UPC‑A barcode with DataBar coupon
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1DatabarCoupon, "123456789012(8110)ASPOSE"))
        {
            // Set barcode module size (X-dimension) to 2 pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Render barcode to an in‑memory bitmap
            using (var barcodeBitmap = generator.GenerateBarCodeImage())
            {
                // Save barcode bitmap to a memory stream in PNG format
                using (var barcodeStream = new MemoryStream())
                {
                    barcodeBitmap.Save(barcodeStream, ImageFormat.Png);
                    barcodeStream.Position = 0; // Reset stream position for reading

                    // Create a blank product label image (500x300 pixels)
                    using (var labelBitmap = new Bitmap(500, 300))
                    {
                        // Obtain graphics object for drawing on the label
                        using (var graphics = Graphics.FromImage(labelBitmap))
                        {
                            // Fill background with white color
                            graphics.Clear(Color.White);

                            // Draw product name text
                            using (var font = new Font("Arial", 24f))
                            using (var brush = new SolidBrush(Color.Black))
                            {
                                graphics.DrawString("Sample Product", font, brush, 20f, 20f);
                            }

                            // Load barcode image from the memory stream
                            using (var barcodeImg = new Bitmap(barcodeStream))
                            {
                                // Position barcode at coordinates (20, 80) on the label
                                graphics.DrawImage(barcodeImg, 20f, 80f);
                            }
                        }

                        // Save the merged label with barcode to PNG file
                        string mergedPath = Path.Combine(outputFolder, "ProductLabelWithBarcode.png");
                        labelBitmap.Save(mergedPath, ImageFormat.Png);
                        Console.WriteLine($"Merged image saved to: {mergedPath}");
                    }
                }
            }
        }
    }
}