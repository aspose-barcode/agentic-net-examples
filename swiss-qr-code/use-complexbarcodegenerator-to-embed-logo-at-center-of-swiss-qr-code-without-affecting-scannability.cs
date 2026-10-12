// Title: Embed a Logo into a Swiss QR Code using ComplexBarcodeGenerator
// Description: Demonstrates generating a Swiss QR Code with a centered logo, ensuring the code remains scannable.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It shows how to use ComplexBarcodeGenerator, SwissQRCodetext, and related classes to create QR codes with custom graphics. Typical use cases include adding branding to payment QR codes or other QR symbols while maintaining error correction. Developers often need to overlay images without breaking decoding, and this sample illustrates that workflow.
// Prompt: Use ComplexBarcodeGenerator to embed a logo at the center of the Swiss QR Code without affecting scannability.
// Tags: swiss qr, logo embedding, complex barcode, qr code, image generation, barcode recognition, aspnet.barcode

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a Swiss QR Code, overlays a simple logo at its center, saves the image,
/// and optionally verifies that the QR code remains readable.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode creation, logo compositing, saving,
    /// and optional decoding verification.
    /// </summary>
    static void Main(string[] args)
    {
        // ------------------------------------------------------------
        // Prepare the output directory where the generated image will be stored
        // ------------------------------------------------------------
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // ------------------------------------------------------------
        // Build the Swiss QR Code payload (codetext) with required fields
        // ------------------------------------------------------------
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Currency = "CHF";
        swissQr.Bill.Creditor = new Address
        {
            Name = "John Doe",
            CountryCode = "CH"
        };

        // ------------------------------------------------------------
        // Configure the generator: set module size and high error correction level
        // ------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // ------------------------------------------------------------
            // Generate the base QR code image
            // ------------------------------------------------------------
            using (Bitmap barcodeBitmap = generator.GenerateBarCodeImage())
            {
                // ------------------------------------------------------------
                // Create a simple circular logo bitmap (100x100 pixels)
                // ------------------------------------------------------------
                using (Bitmap logoBitmap = new Bitmap(100, 100))
                {
                    using (Graphics gLogo = Graphics.FromImage(logoBitmap))
                    {
                        // Fill background with white
                        gLogo.Clear(Color.White);
                        // Draw a blue circle
                        using (Brush brush = new SolidBrush(Color.Blue))
                        {
                            gLogo.FillEllipse(brush, 10, 10, 80, 80);
                        }
                    }

                    // ------------------------------------------------------------
                    // Composite the logo onto the center of the QR code image
                    // ------------------------------------------------------------
                    using (Graphics g = Graphics.FromImage(barcodeBitmap))
                    {
                        int logoSize = Math.Min(barcodeBitmap.Width, barcodeBitmap.Height) / 4;
                        var destRect = new Rectangle(
                            (barcodeBitmap.Width - logoSize) / 2,
                            (barcodeBitmap.Height - logoSize) / 2,
                            logoSize,
                            logoSize);
                        g.DrawImage(logoBitmap, destRect);
                    }

                    // ------------------------------------------------------------
                    // Save the final image with the embedded logo
                    // ------------------------------------------------------------
                    string outputPath = Path.Combine(outputDir, "SwissQR_With_Logo.png");
                    barcodeBitmap.Save(outputPath, ImageFormat.Png);
                    Console.WriteLine($"Swiss QR Code with logo saved to: {outputPath}");
                }
            }
        }

        // ------------------------------------------------------------
        // Optional: verify that the QR code is still readable after logo overlay
        // ------------------------------------------------------------
        string readPath = Path.Combine(outputDir, "SwissQR_With_Logo.png");
        if (File.Exists(readPath))
        {
            using (var reader = new BarCodeReader(readPath, DecodeType.QR))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    var decoded = ComplexCodetextReader.TryDecodeSwissQR(result.CodeText);
                    if (decoded != null)
                    {
                        Console.WriteLine("Decoded Swiss QR Code successfully:");
                        Console.WriteLine($"Amount: {decoded.Bill.Amount} {decoded.Bill.Currency}");
                        Console.WriteLine($"Creditor: {decoded.Bill.Creditor.Name}");
                    }
                    else
                    {
                        Console.WriteLine("Failed to decode Swiss QR Code.");
                    }
                }
            }
        }
    }
}