// Title: Generate a UPC‑A barcode with a DataBar coupon and overlay a QR code
// Description: Demonstrates creating a UPC‑A barcode that includes a GS1‑128 coupon, then compositing a QR code on top using Aspose.Drawing.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and image composition category. It shows how to use BarcodeGenerator with EncodeTypes.UpcaGs1Code128Coupon and EncodeTypes.QR, configure visual parameters, render images, and combine them via Aspose.Drawing.Graphics. Developers often need to combine multiple symbologies into a single image for packaging, marketing, or retail applications.
// Prompt: Create a UPC‑A barcode with a DataBar coupon, then overlay a QR code using a graphics library.
// Tags: upc-a, databar, coupon, qr, barcode generation, image composition, aspose.barcode, aspose.drawing, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a UPC‑A barcode with a GS1‑128 coupon and overlaying a QR code,
/// then saving the combined image as PNG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcodes, composes them, and writes the result to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the final combined image.
        string outputPath = "combined.png";

        // UPC‑A barcode text that includes a GS1‑128 coupon segment.
        string upcCodeText = "514141100906(8102)03";

        // Create a generator for the UPC‑A barcode with a DataBar coupon.
        using (var upcGenerator = new BarcodeGenerator(EncodeTypes.UpcaGs1Code128Coupon, upcCodeText))
        {
            // Set visual appearance for the UPC barcode.
            upcGenerator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            upcGenerator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Render the UPC barcode to a bitmap image.
            using (Bitmap upcBitmap = upcGenerator.GenerateBarCodeImage())
            {
                // Text to encode in the QR code that will be overlaid.
                string qrText = "https://example.com";

                // Create a generator for the QR code.
                using (var qrGenerator = new BarcodeGenerator(EncodeTypes.QR, qrText))
                {
                    // Use a high error correction level for better readability after overlay.
                    qrGenerator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
                    qrGenerator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                    qrGenerator.Parameters.BackColor = Aspose.Drawing.Color.White;

                    // Render the QR code to a bitmap image.
                    using (Bitmap qrBitmap = qrGenerator.GenerateBarCodeImage())
                    {
                        // Calculate position for the QR code: bottom‑right corner with a margin.
                        int margin = 10;
                        int qrX = upcBitmap.Width - qrBitmap.Width - margin;
                        int qrY = upcBitmap.Height - qrBitmap.Height - margin;

                        // Ensure the QR code does not go outside the UPC image bounds.
                        if (qrX < 0) qrX = 0;
                        if (qrY < 0) qrY = 0;

                        // Draw the QR bitmap onto the UPC bitmap using graphics.
                        using (Graphics graphics = Graphics.FromImage(upcBitmap))
                        {
                            graphics.DrawImage(qrBitmap, qrX, qrY, qrBitmap.Width, qrBitmap.Height);
                        }

                        // Save the combined image as a PNG file.
                        upcBitmap.Save(outputPath, ImageFormat.Png);
                    }
                }
            }
        }

        // Inform the user where the combined image was saved.
        Console.WriteLine($"Combined barcode saved to {Path.GetFullPath(outputPath)}");
    }
}