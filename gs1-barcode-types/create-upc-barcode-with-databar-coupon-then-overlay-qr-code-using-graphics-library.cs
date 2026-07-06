// Title: Create UPC‑A barcode with DataBar coupon and overlay QR code
// Description: Demonstrates generating a UPC‑A barcode with a GS1 DataBar coupon and then compositing a QR code on top using Aspose.Drawing.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and image composition category. It shows how to use BarcodeGenerator with EncodeTypes.UpcaGs1DatabarCoupon and EncodeTypes.QR, configure parameters, and combine images via Aspose.Drawing. Developers often need to create composite barcode images for packaging, marketing, or inventory applications, and this snippet illustrates the typical workflow.
// Prompt: Create a UPC‑A barcode with a DataBar coupon, then overlay a QR code using a graphics library.
// Tags: upc barcode, databar coupon, qr code, image overlay, aspose.barcode, aspose.drawing, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates creating a UPC‑A barcode with a GS1 DataBar coupon and overlaying a QR code.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode images, composes them, and saves the result.
    /// </summary>
    static void Main()
    {
        // Define file names for the individual and combined images
        string upcFile = "upc_databar.png";
        string combinedFile = "combined.png";

        // -----------------------------------------------------------------
        // 1. Create UPC‑A barcode with a GS1 DataBar coupon
        // -----------------------------------------------------------------
        // Sample codetext: UPCA part + DataBar part
        string upcCodeText = "514141100906(8110)106141416543213500110000310123196000";

        using (var upcGenerator = new BarcodeGenerator(EncodeTypes.UpcaGs1DatabarCoupon, upcCodeText))
        {
            // Optional: set image size (auto‑size mode)
            upcGenerator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            upcGenerator.Parameters.ImageWidth.Point = 400f;
            upcGenerator.Parameters.ImageHeight.Point = 200f;

            // Save the UPC‑A barcode image to file
            upcGenerator.Save(upcFile);
        }

        // -----------------------------------------------------------------
        // 2. Create a QR code that will be overlaid on the UPC image
        // -----------------------------------------------------------------
        string qrCodeText = "https://example.com";

        using (var qrGenerator = new BarcodeGenerator(EncodeTypes.QR, qrCodeText))
        {
            // Set high error correction level for better readability after overlay
            qrGenerator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Define QR image size (auto‑size mode)
            qrGenerator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            qrGenerator.Parameters.ImageWidth.Point = 150f;
            qrGenerator.Parameters.ImageHeight.Point = 150f;

            // Generate QR bitmap in memory
            using (Bitmap qrBitmap = qrGenerator.GenerateBarCodeImage())
            {
                // -----------------------------------------------------------------
                // 3. Load the UPC image and overlay the QR code onto it
                // -----------------------------------------------------------------
                if (!File.Exists(upcFile))
                {
                    Console.WriteLine($"Error: UPC image file '{upcFile}' not found.");
                    return;
                }

                using (Bitmap upcBitmap = new Bitmap(upcFile))
                {
                    // Determine position: bottom‑right corner with a 10‑pixel margin
                    int margin = 10;
                    int x = upcBitmap.Width - qrBitmap.Width - margin;
                    int y = upcBitmap.Height - qrBitmap.Height - margin;

                    // Draw the QR code onto the UPC image
                    using (Graphics graphics = Graphics.FromImage(upcBitmap))
                    {
                        graphics.DrawImage(qrBitmap, new Rectangle(x, y, qrBitmap.Width, qrBitmap.Height));
                    }

                    // Save the combined image to file
                    upcBitmap.Save(combinedFile, ImageFormat.Png);
                }
            }
        }

        Console.WriteLine($"Combined barcode image saved as '{combinedFile}'.");
    }
}