// Title: Generate UPC‑A DataBar Coupon Barcode and Merge with a Logo
// Description: Demonstrates how to create a UPC‑A barcode with a GS1 DataBar coupon payload using Aspose.BarCode, then combine it with a simple logo image using Aspose.Drawing graphics.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.UpcaGs1DatabarCoupon, customizing visual parameters, and compositing the result with System.Drawing (Aspose.Drawing) graphics. Typical use cases include creating promotional barcodes that embed coupon data and branding them with a company logo. Developers often need to adjust dimensions, colors, and merge multiple images for printable assets.
// Prompt: Generate a UPC‑A barcode with a DataBar coupon, then combine it with a logo using System.Drawing graphics.
// Tags: barcode, upc-a, databar, coupon, logo, aspose.barcode, aspose.drawing, image composition, png, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a UPC‑A GS1 DataBar coupon barcode,
/// creates a placeholder logo, and merges both images side by side.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, builds a logo, combines them,
    /// and saves the result as a PNG file.
    /// </summary>
    static void Main()
    {
        // UPC‑A with DataBar coupon text (example from Aspose documentation)
        const string barcodeText = "514141100906(8110)106141416543213500110000310123196000";

        // Initialize the barcode generator for the specific symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1DatabarCoupon, barcodeText))
        {
            // Optional visual tweaks
            generator.Parameters.Barcode.XDimension.Point = 2f;          // Module size (width of the smallest bar)
            generator.Parameters.Barcode.BarHeight.Point = 100f;       // Height of the linear part of the barcode
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Generate the barcode image as a bitmap
            using (Bitmap barcodeBitmap = generator.GenerateBarCodeImage())
            {
                // Create a simple placeholder logo (blue square with a white inner rectangle)
                const int logoSize = 100;
                using (Bitmap logoBitmap = new Bitmap(logoSize, logoSize))
                {
                    using (Graphics gLogo = Graphics.FromImage(logoBitmap))
                    {
                        // Fill background with white
                        gLogo.Clear(Aspose.Drawing.Color.White);
                        // Draw a blue border rectangle
                        using (Pen pen = new Pen(Aspose.Drawing.Color.Blue, 3f))
                        {
                            gLogo.DrawRectangle(pen, 5, 5, logoSize - 10, logoSize - 10);
                        }
                    }

                    // Combine barcode and logo side by side
                    int combinedWidth = barcodeBitmap.Width + logoBitmap.Width;
                    int combinedHeight = Math.Max(barcodeBitmap.Height, logoBitmap.Height);
                    using (Bitmap combinedBitmap = new Bitmap(combinedWidth, combinedHeight))
                    {
                        using (Graphics g = Graphics.FromImage(combinedBitmap))
                        {
                            // Fill the combined image background with white
                            g.Clear(Aspose.Drawing.Color.White);
                            // Draw barcode on the left
                            g.DrawImage(barcodeBitmap, 0, 0);
                            // Draw logo on the right, vertically centered
                            int logoY = (combinedHeight - logoBitmap.Height) / 2;
                            g.DrawImage(logoBitmap, barcodeBitmap.Width, logoY);
                        }

                        // Save the final combined image as PNG
                        const string outputPath = "combined.png";
                        combinedBitmap.Save(outputPath, ImageFormat.Png);
                        Console.WriteLine($"Combined image saved to: {Path.GetFullPath(outputPath)}");
                    }
                }
            }
        }
    }
}