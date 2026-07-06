// Title: Generate UPC‑A DataBar Coupon barcode and merge with product label
// Description: Demonstrates creating a UPC‑A barcode with a GS1 DataBar coupon payload and combining it with an existing product label image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and image manipulation category. It showcases the use of BarcodeGenerator with EncodeTypes.UpcaGs1DatabarCoupon, setting visual parameters, and merging the generated barcode bitmap onto a product label using Aspose.Drawing graphics. Developers often need to create combined label images for retail packaging, where a barcode is placed on top of product artwork.
// Prompt: Produce a UPC‑A barcode with a DataBar coupon, then programmatically merge it with a product label image.
// Tags: upc-a, databar, coupon, barcode-generation, image-merge, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a UPC‑A DataBar coupon barcode,
/// saves it as an image, and merges it onto a product label picture.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, merges it with a label,
    /// and writes the resulting images to disk.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Define file paths (adjust as needed for your environment)
        // --------------------------------------------------------------------
        const string productLabelPath = "product_label.png";
        const string barcodePath = "barcode.png";
        const string mergedPath = "merged_label.png";

        // Verify that the product label image exists before proceeding
        if (!File.Exists(productLabelPath))
        {
            Console.WriteLine($"Error: Product label image not found at '{productLabelPath}'.");
            return;
        }

        // --------------------------------------------------------------------
        // Prepare the barcode data: UPC‑A with GS1 DataBar coupon payload
        // --------------------------------------------------------------------
        const string couponCodeText = "514141100906(8110)106141416543213500110000310123196000";

        // Create a BarcodeGenerator for the UPC‑A DataBar coupon symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1DatabarCoupon, couponCodeText))
        {
            // Optional visual customizations
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Enable automatic sizing using interpolation
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set desired image dimensions (points)
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // ----------------------------------------------------------------
            // Generate the barcode bitmap
            // ----------------------------------------------------------------
            using (Bitmap barcodeBitmap = generator.GenerateBarCodeImage())
            {
                // Save the standalone barcode image (optional)
                barcodeBitmap.Save(barcodePath, ImageFormat.Png);

                // Load the existing product label image
                using (Image productImage = Image.FromFile(productLabelPath))
                {
                    // Prepare a graphics object for drawing onto the label
                    using (Graphics graphics = Graphics.FromImage(productImage))
                    {
                        // Define placement margin and calculate position (bottom‑left)
                        int margin = 50;
                        int posX = margin;
                        int posY = productImage.Height - barcodeBitmap.Height - margin;

                        // Draw the barcode onto the label at the calculated coordinates
                        graphics.DrawImage(barcodeBitmap, posX, posY, barcodeBitmap.Width, barcodeBitmap.Height);
                    }

                    // Save the combined label image
                    productImage.Save(mergedPath, ImageFormat.Png);
                }
            }
        }

        // Inform the user where the output files are located
        Console.WriteLine($"Barcode image saved to: {barcodePath}");
        Console.WriteLine($"Merged label image saved to: {mergedPath}");
    }
}