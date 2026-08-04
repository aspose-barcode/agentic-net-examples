// Title: Generate UPC‑A DataBar Coupon Barcode and Merge with Product Label
// Description: Demonstrates creating a UPC‑A barcode with a GS1 DataBar coupon symbology, then compositing it onto a product label image.
// Category-Description: This example belongs to the barcode generation and image manipulation category, showcasing how to use Aspose.BarCode's BarcodeGenerator with EncodeTypes.UpcaGs1DatabarCoupon, configure visual parameters, and combine the generated barcode with other graphics using Aspose.Drawing. Typical use cases include creating product labels, coupons, and packaging artwork where a barcode must be placed on a pre‑designed label.
// Prompt: Produce a UPC‑A barcode with a DataBar coupon, then programmatically merge it with a product label image.
// Tags: barcode, upc-a, databar, coupon, image merging, aspose.barcode, aspose.drawing, png, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a UPC‑A GS1 DataBar coupon barcode,
/// draws it onto a simple product label, and saves the merged image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, merges it with a label, and writes the result to disk.
    /// </summary>
    static void Main()
    {
        // Output file path for the final merged label image
        const string mergedPath = "merged_label.png";

        // Text to encode – a UPC‑A code with GS1 DataBar coupon data
        const string couponCodeText = "514141100906(8110)106141416543213500110000310123196000";

        // Create a barcode generator for the UPC‑A GS1 DataBar coupon symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1DatabarCoupon, couponCodeText))
        {
            // Optional visual customizations
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;   // barcode bars color
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;         // background color
            generator.Parameters.Barcode.XDimension.Point = 2f;                  // module (X) size

            // Store the generated barcode image in a memory stream
            using (var barcodeStream = new MemoryStream())
            {
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                barcodeStream.Position = 0; // reset stream position for reading

                // Load the barcode image from the memory stream
                using (var barcodeImage = Image.FromStream(barcodeStream))
                {
                    // Create a blank product label canvas (400 px × 300 px, white background)
                    using (var labelImage = new Bitmap(400, 300))
                    {
                        // Fill the label background with white
                        using (var gfx = Graphics.FromImage(labelImage))
                        {
                            gfx.Clear(Aspose.Drawing.Color.White);
                        }

                        // Draw the barcode onto the label in the bottom‑right corner with a 10 px margin
                        using (var gfx = Graphics.FromImage(labelImage))
                        {
                            int x = labelImage.Width - barcodeImage.Width - 10;
                            int y = labelImage.Height - barcodeImage.Height - 10;
                            gfx.DrawImage(barcodeImage, x, y, barcodeImage.Width, barcodeImage.Height);
                        }

                        // Save the combined label image as PNG
                        labelImage.Save(mergedPath, ImageFormat.Png);
                        Console.WriteLine($"Merged label saved to: {mergedPath}");
                    }
                }
            }
        }
    }
}