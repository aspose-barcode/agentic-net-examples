// Title: Create UPC‑A barcode with DataBar coupon and overlay QR code
// Description: Demonstrates generating a UPC‑A barcode with a GS1 DataBar coupon and overlaying a QR code using Aspose.BarCode and Aspose.Drawing.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to combine multiple symbologies in a single image. It uses the BarcodeGenerator class to create UPC‑A (with GS1 DataBar coupon) and QR code barcodes, then employs Aspose.Drawing's Bitmap and Graphics classes to composite the images. Developers often need such combined barcodes for packaging, coupons, or marketing materials where a product identifier and a scannable link are required together.
// Prompt: Create a UPC‑A barcode with a DataBar coupon, then overlay a QR code using a graphics library.
// Tags: upc-a, databar, qr, overlay, aspose.barcode, aspose.drawing, png, barcode-generation

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a UPC‑A barcode with a GS1 DataBar coupon and overlays a QR code on it.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the combined barcode image and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Prepare the output directory where the final image will be saved
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Create a UPC‑A barcode that includes a GS1 DataBar coupon payload
        using (var upcGenerator = new BarcodeGenerator(EncodeTypes.UpcaGs1DatabarCoupon, "123456789012(8110)ASPOSE"))
        {
            // Set the X-dimension (module width) for better readability
            upcGenerator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Generate the UPC‑A barcode as a bitmap image
            using (Bitmap upcBitmap = upcGenerator.GenerateBarCodeImage())
            {
                // Create a QR code that will be overlaid on the UPC image
                using (var qrGenerator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
                {
                    // Configure QR code size and error correction level
                    qrGenerator.Parameters.Barcode.XDimension.Pixels = 3f;
                    qrGenerator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                    // Generate the QR code bitmap
                    using (Bitmap qrBitmap = qrGenerator.GenerateBarCodeImage())
                    {
                        // Draw the QR code onto the UPC bitmap using graphics context
                        using (Graphics graphics = Graphics.FromImage(upcBitmap))
                        {
                            int offset = 10; // Margin from the edges
                            int x = upcBitmap.Width - qrBitmap.Width - offset;
                            int y = upcBitmap.Height - qrBitmap.Height - offset;
                            graphics.DrawImage(qrBitmap, x, y, qrBitmap.Width, qrBitmap.Height);
                        }

                        // Save the combined barcode image as PNG
                        string outputPath = Path.Combine(outputDir, "CombinedBarcode.png");
                        upcBitmap.Save(outputPath, ImageFormat.Png);
                        Console.WriteLine($"Combined barcode saved to: {outputPath}");
                    }
                }
            }
        }
    }
}