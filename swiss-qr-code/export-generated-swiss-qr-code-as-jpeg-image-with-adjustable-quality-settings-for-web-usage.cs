// Title: Export Swiss QR Code as JPEG with adjustable quality
// Description: Demonstrates generating a Swiss QR Bill barcode and saving it as a JPEG image where the compression quality can be set via a command‑line argument, useful for web‑optimized graphics.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Swiss QR Bill. It showcases the ComplexBarcodeGenerator, SwissQRCodetext, and image handling via Aspose.Drawing to produce JPEG output with custom encoder parameters. Developers often need to create high‑quality, web‑friendly barcode images with controllable compression for e‑commerce, invoicing, or mobile apps.
// Prompt: Export the generated Swiss QR Code as a JPEG image with adjustable quality settings for web usage.
// Tags: swiss qr, barcode generation, jpeg output, quality setting, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Linq;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a Swiss QR Bill barcode and saves it as a JPEG file with configurable compression quality.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts an optional integer argument (0‑100) to set JPEG quality; defaults to 80.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may specify JPEG quality.</param>
    static void Main(string[] args)
    {
        // Set default JPEG quality (0‑100). Default is 80.
        int jpegQuality = 80;

        // If a command‑line argument is provided, try to parse it as an integer quality value.
        if (args.Length > 0 && int.TryParse(args[0], out int parsedQuality))
        {
            // Validate the parsed quality range.
            if (parsedQuality < 0 || parsedQuality > 100)
            {
                Console.WriteLine("Quality must be between 0 and 100. Using default 80.");
            }
            else
            {
                jpegQuality = parsedQuality;
            }
        }

        // Prepare Swiss QR bill data (mandatory fields).
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Generate the Swiss QR Code into a memory stream as JPEG.
        using (var ms = new MemoryStream())
        {
            using (var generator = new ComplexBarcodeGenerator(swissQr))
            {
                generator.Save(ms, BarCodeImageFormat.Jpeg);
            }

            // Reset stream position for reading.
            ms.Position = 0;

            // Load the generated image using Aspose.Drawing.
            using (var image = Image.FromStream(ms))
            {
                // Find JPEG encoder.
                var jpegEncoder = ImageCodecInfo.GetImageEncoders()
                    .FirstOrDefault(enc => enc.FormatID == ImageFormat.Jpeg.Guid);

                if (jpegEncoder == null)
                {
                    // Fallback: save with default settings if encoder not found.
                    Console.WriteLine("JPEG encoder not found. Saving with default settings.");
                    image.Save("SwissQR.jpeg", ImageFormat.Jpeg);
                    return;
                }

                // Set quality parameter using encoder parameters.
                using (var encoderParams = new EncoderParameters(1))
                {
                    encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, (long)jpegQuality);
                    image.Save("SwissQR.jpeg", jpegEncoder, encoderParams);
                }
            }
        }

        Console.WriteLine($"Swiss QR Code saved as 'SwissQR.jpeg' with quality {jpegQuality}.");
    }
}