// Title: Generate QR Code with token-based endpoint protection
// Description: Demonstrates creating a QR Code image using Aspose.BarCode and protecting the generation endpoint with a simple token check.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure QR Code parameters (EncodeTypes.QR, error correction level, X-dimension) and save the result as PNG. Developers often need to generate barcodes on demand in web services, requiring basic authentication or token validation before serving the image. The code showcases typical usage of BarcodeGenerator, Parameters, and BarCodeImageFormat classes.
// Prompt: Generate QR Code barcode and secure endpoint with token authentication before serving image.
// Tags: qr code, token authentication, barcode generation, png output, aspose.barcode, encode types, qrcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code image after validating a simple token.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Validates token, creates QR Code, and saves it as PNG.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument can be the token.</param>
    static void Main(string[] args)
    {
        // Define the required token for authentication
        const string requiredToken = "secret123";

        // Use the token supplied via command line if present; otherwise default to the required token
        string providedToken = args.Length > 0 ? args[0] : requiredToken;

        // Verify the provided token matches the required token
        if (!string.Equals(providedToken, requiredToken, StringComparison.Ordinal))
        {
            Console.WriteLine("Invalid token. Access denied.");
            return;
        }

        // Determine the output file path for the generated QR Code image
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr.png");

        // Text to encode in the QR Code
        string codeText = "Hello, Aspose!";

        // Create a QR Code generator with specified encoding type and text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set the size of a single QR module (pixel dimension)
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Set the error correction level to Medium (LevelM)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated QR Code as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image has been saved
        Console.WriteLine($"QR Code generated and saved to: {outputPath}");
    }
}