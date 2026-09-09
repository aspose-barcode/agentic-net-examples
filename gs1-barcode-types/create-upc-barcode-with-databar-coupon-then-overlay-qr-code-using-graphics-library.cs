// Title: Create UPC‑A barcode with DataBar coupon and overlay QR code
// Description: Demonstrates generating a UPC‑A barcode that includes a GS1 DataBar coupon, then compositing a QR code onto the barcode image using Aspose.BarCode and Aspose.Drawing.
// Category-Description: This example belongs to the Aspose.BarCode image generation and manipulation category. It showcases the use of BarcodeGenerator with EncodeTypes.UpcaGs1DatabarCoupon and EncodeTypes.QR, as well as Aspose.Drawing's Bitmap and Graphics classes to combine multiple symbologies into a single PNG. Developers creating packaging, coupons, or marketing materials often need to embed multiple barcodes in one image, and this pattern illustrates a typical workflow for such scenarios.
// Prompt: Create a UPC‑A barcode with a DataBar coupon, then overlay a QR code using a graphics library.
// Tags: upc, databar, qr, overlay, image, aspose.barcode, aspose.drawing, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a UPC‑A barcode with a GS1 DataBar coupon and overlays a QR code onto it.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the combined barcode image and saves it to the Output folder.
    /// </summary>
    static void Main()
    {
        // Prepare output directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Define the data for each barcode
        string upcCodeText = "123456789012(8110)ASPOSE";
        string qrCodeText = "https://example.com";

        // Path for the final combined image
        string combinedPath = Path.Combine(outputDir, "CombinedBarcode.png");

        // Generate UPC‑A barcode with a DataBar coupon
        using (var upcGenerator = new BarcodeGenerator(EncodeTypes.UpcaGs1DatabarCoupon, upcCodeText))
        {
            // Set barcode resolution (pixel size of the smallest bar)
            upcGenerator.Parameters.Barcode.XDimension.Pixels = 2f;

            using (Bitmap upcBitmap = upcGenerator.GenerateBarCodeImage())
            {
                // Generate QR code to be overlaid
                using (var qrGenerator = new BarcodeGenerator(EncodeTypes.QR, qrCodeText))
                {
                    qrGenerator.Parameters.Barcode.XDimension.Pixels = 2f;
                    qrGenerator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH; // High error correction

                    using (Bitmap qrBitmap = qrGenerator.GenerateBarCodeImage())
                    {
                        // Composite QR code onto the UPC image using a graphics context
                        using (Graphics graphics = Graphics.FromImage(upcBitmap))
                        {
                            int offsetX = upcBitmap.Width - qrBitmap.Width - 10; // 10 px margin from the right edge
                            int offsetY = upcBitmap.Height - qrBitmap.Height - 10; // 10 px margin from the bottom edge
                            graphics.DrawImage(qrBitmap, new Rectangle(offsetX, offsetY, qrBitmap.Width, qrBitmap.Height));
                        }

                        // Save the combined image as PNG
                        upcBitmap.Save(combinedPath, ImageFormat.Png);
                    }
                }
            }
        }

        Console.WriteLine($"Combined barcode saved to: {combinedPath}");
    }
}