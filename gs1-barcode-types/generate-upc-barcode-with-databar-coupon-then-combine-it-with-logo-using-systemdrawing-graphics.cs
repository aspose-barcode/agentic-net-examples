// Title: Generate UPC‑A DataBar Coupon barcode with logo overlay
// Description: Demonstrates creating a UPC‑A barcode with a GS1 DataBar coupon, then merging it with a simple logo using System.Drawing.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator with EncodeTypes.UpcaGs1DatabarCoupon, customize colors, and combine the resulting bitmap with other graphics via Aspose.Drawing. Typical use cases include retail product labeling with promotional coupons and brand logos. Developers often need to create combined images for printing or digital display, making this pattern a common reference.
// Prompt: Generate a UPC‑A barcode with a DataBar coupon, then combine it with a logo using System.Drawing graphics.
// Tags: upc-a, databar, coupon, barcode generation, logo overlay, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a UPC‑A barcode with a GS1 DataBar coupon,
/// generates a simple logo, and combines both images into a single PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, logo, and combined image,
    /// then writes the output file locations to the console.
    /// </summary>
    static void Main()
    {
        // Paths for output images
        const string barcodePath = "barcode.png";
        const string logoPath = "logo.png";
        const string combinedPath = "combined.png";

        // -----------------------------------------------------------------
        // 1. Create UPC‑A barcode with a DataBar coupon
        // -----------------------------------------------------------------
        // Sample codetext: UPCA part + DataBar part (see EncodeTypes.UpcaGs1DatabarCoupon documentation)
        const string couponCodeText = "514141100906(8110)106141416543213500110000310123196000";

        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1DatabarCoupon, couponCodeText))
        {
            // Optional visual tweaks
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Generate the barcode image
            using (Bitmap barcodeBmp = generator.GenerateBarCodeImage())
            {
                // Save the barcode for reference
                barcodeBmp.Save(barcodePath, ImageFormat.Png);

                // -----------------------------------------------------------------
                // 2. Create a simple logo image programmatically
                // -----------------------------------------------------------------
                using (var logoBmp = new Bitmap(100, 100))
                {
                    using (var gLogo = Graphics.FromImage(logoBmp))
                    {
                        // Fill background with light gray
                        gLogo.Clear(Color.LightGray);
                        using (var font = new Font("Arial", 12f))
                        {
                            // Draw the word "Logo" in the center area
                            gLogo.DrawString("Logo", font, Brushes.Black, new PointF(10f, 40f));
                        }
                    }

                    // Save the logo for reference
                    logoBmp.Save(logoPath, ImageFormat.Png);

                    // -----------------------------------------------------------------
                    // 3. Combine barcode and logo into a single image
                    // -----------------------------------------------------------------
                    int combinedWidth = Math.Max(barcodeBmp.Width, logoBmp.Width);
                    int combinedHeight = barcodeBmp.Height + logoBmp.Height;

                    using (var combinedBmp = new Bitmap(combinedWidth, combinedHeight))
                    {
                        using (var gCombined = Graphics.FromImage(combinedBmp))
                        {
                            // White background for the combined image
                            gCombined.Clear(Color.White);

                            // Draw logo at the top
                            gCombined.DrawImage(logoBmp, 0, 0, logoBmp.Width, logoBmp.Height);

                            // Draw barcode below the logo
                            gCombined.DrawImage(barcodeBmp, 0, logoBmp.Height, barcodeBmp.Width, barcodeBmp.Height);
                        }

                        // Save the final combined image
                        combinedBmp.Save(combinedPath, ImageFormat.Png);
                    }
                }
            }
        }

        // Inform the user where the files are saved
        Console.WriteLine($"Barcode saved to: {Path.GetFullPath(barcodePath)}");
        Console.WriteLine($"Logo saved to: {Path.GetFullPath(logoPath)}");
        Console.WriteLine($"Combined image saved to: {Path.GetFullPath(combinedPath)}");
    }
}