// Title: Generate QR Code with Token Authentication
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode, securing the operation with a simple token check, and saving the image as PNG.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to configure QR Code parameters (X‑Dimension, error correction level) and integrate basic token‑based authentication before serving the image. Developers commonly use the BarcodeGenerator class with EncodeTypes.QR and BarCodeImageFormat to produce QR codes for URLs, product links, or authentication flows.
// Prompt: Generate QR Code barcode and secure endpoint with token authentication before serving image.
// Tags: qr code, token authentication, barcode generation, png output, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR Code image after validating a token.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Validates a token passed via command‑line arguments, creates a QR Code for a URL, and saves it as a PNG file.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument may contain the authentication token.</param>
    static void Main(string[] args)
    {
        // Expected token for simple authentication
        const string expectedToken = "secret123";

        // Retrieve token from command‑line arguments or use the expected token as default
        string token = args.Length > 0 ? args[0] : expectedToken;

        // Validate token; abort if it does not match
        if (token != expectedToken)
        {
            Console.WriteLine("Invalid token. Access denied.");
            return;
        }

        // Determine temporary file path for the generated QR code image
        string outputPath = Path.Combine(Path.GetTempPath(), "qr_code.png");

        // Text to encode in the QR code (e.g., a URL)
        string codeText = "https://example.com";

        // Create a QR Code generator with the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set the size of a single module (X‑Dimension) in points
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Set error correction level to Medium (Level M)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR code image was saved
        Console.WriteLine($"QR code generated at: {outputPath}");
    }
}