// Title: Generate UPC‑A DataBar Coupon barcode with embedded logo
// Description: Demonstrates creating a UPC‑A barcode with a DataBar coupon symbology, then overlaying a custom logo using System.Drawing graphics.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use BarcodeGenerator with EncodeTypes.UpcaGs1DatabarCoupon, customize dimensions, and combine the generated image with additional graphics. Typical use cases include adding branding or logos to product barcodes for marketing or compliance. Developers often need to merge barcode images with other visual elements using Aspose.Drawing's Bitmap and Graphics classes.
// Prompt: Generate a UPC‑A barcode with a DataBar coupon, then combine it with a logo using System.Drawing graphics.
// Tags: upc-a, databar, barcode generation, logo overlay, aspose.barcode, aspose.drawing, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a UPC‑A DataBar coupon barcode and compositing it with a logo image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, creates a simple logo, merges them, and saves the result as PNG.
    /// </summary>
    static void Main()
    {
        // Determine output file path in current directory
        string outputPath = Path.Combine(Environment.CurrentDirectory, "UPC_A_With_Logo.png");

        // Initialize barcode generator with UPC‑A DataBar coupon symbology and data string
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1DatabarCoupon, "123456789012(8110)ASPOSE"))
        {
            // Set X-dimension (module width) to 2 pixels for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Generate barcode image as a Bitmap
            using (Bitmap barcodeBitmap = generator.GenerateBarCodeImage())
            {
                // Create a placeholder logo bitmap (100x50) with ARGB pixel format
                using (Bitmap logoBitmap = new Bitmap(100, 50, PixelFormat.Format32bppArgb))
                {
                    // Draw simple graphics onto the logo bitmap
                    using (Graphics logoGraphics = Graphics.FromImage(logoBitmap))
                    {
                        // Fill background with light gray
                        logoGraphics.Clear(Color.LightGray);
                        // Draw a dark blue ellipse as a visual element
                        using (Pen pen = new Pen(Color.DarkBlue, 2f))
                        {
                            logoGraphics.DrawEllipse(pen, 5, 5, 90, 40);
                        }
                    }

                    // Get dimensions of the generated barcode
                    int width = barcodeBitmap.Width;
                    int height = barcodeBitmap.Height;

                    // Create a new bitmap to hold the combined image
                    using (Bitmap combined = new Bitmap(width, height, PixelFormat.Format32bppArgb))
                    {
                        // Render barcode and logo onto the combined bitmap
                        using (Graphics g = Graphics.FromImage(combined))
                        {
                            // Fill background with white
                            g.Clear(Color.White);
                            // Draw the barcode at the top‑left corner
                            g.DrawImage(barcodeBitmap, 0, 0, barcodeBitmap.Width, barcodeBitmap.Height);

                            // Position logo near the bottom‑right corner with a 10‑pixel margin
                            int logoX = width - logoBitmap.Width - 10;
                            int logoY = height - logoBitmap.Height - 10;
                            g.DrawImage(logoBitmap, logoX, logoY, logoBitmap.Width, logoBitmap.Height);
                        }

                        // Save the combined image as PNG
                        combined.Save(outputPath, ImageFormat.Png);
                    }
                }
            }
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode with logo saved to: {outputPath}");
    }
}