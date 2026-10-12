// Title: Generate a UPC‑A barcode with DataBar coupon and overlay a QR code
// Description: Demonstrates creating a UPC‑A barcode that includes a GS1 DataBar coupon and then compositing a QR code on top using Aspose.Drawing.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to combine multiple symbologies in a single image. It uses BarcodeGenerator, EncodeTypes, and Aspose.Drawing graphics classes to generate barcodes, adjust parameters, and draw one image onto another—common tasks when creating promotional labels or combined barcode symbols.
// Prompt: Create a UPC‑A barcode with a DataBar coupon, then overlay a QR code using a graphics library.
// Tags: upc a, databar, coupon, qr code, barcode generation, image composition, aspose.barcode, aspose.drawing, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a UPC‑A barcode with a GS1 DataBar coupon and overlaying a QR code,
/// then saving the combined image as PNG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcodes, composes them, and writes the result to disk.
    /// </summary>
    static void Main()
    {
        // Determine output file path in the current directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "CombinedBarcode.png");

        // Create UPC‑A barcode generator with DataBar coupon symbology and sample data
        using (var upcGenerator = new BarcodeGenerator(EncodeTypes.UpcaGs1DatabarCoupon, "123456789012(8110)ASPOSE"))
        {
            // Set X-dimension (module width) to 2 pixels for better resolution
            upcGenerator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Generate the UPC barcode image
            using (Bitmap upcBitmap = upcGenerator.GenerateBarCodeImage())
            {
                // Create QR code generator with target URL
                using (var qrGenerator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
                {
                    // Set QR code X-dimension and high error correction level
                    qrGenerator.Parameters.Barcode.XDimension.Pixels = 2f;
                    qrGenerator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                    // Generate the QR code image
                    using (Bitmap qrBitmap = qrGenerator.GenerateBarCodeImage())
                    {
                        // Prepare graphics object to draw QR code onto the UPC image
                        using (Graphics graphics = Graphics.FromImage(upcBitmap))
                        {
                            // Position QR code at bottom‑right corner with a small offset
                            int offset = 10;
                            int x = upcBitmap.Width - qrBitmap.Width - offset;
                            int y = upcBitmap.Height - qrBitmap.Height - offset;

                            // Draw QR code onto the UPC bitmap
                            graphics.DrawImage(qrBitmap, new Rectangle(x, y, qrBitmap.Width, qrBitmap.Height));
                        }

                        // Save the combined image as PNG
                        upcBitmap.Save(outputPath, ImageFormat.Png);
                        Console.WriteLine($"Combined barcode saved to {outputPath}");
                    }
                }
            }
        }
    }
}