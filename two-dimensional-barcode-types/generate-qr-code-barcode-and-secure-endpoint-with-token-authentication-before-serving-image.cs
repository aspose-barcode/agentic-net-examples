// Title: Generate QR Code with Token Authentication and Save as PNG
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode, securing the operation with a simple token check, and saving the image to disk.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation, error correction configuration, and image output. It showcases key API classes such as BarcodeGenerator, EncodeTypes, and BarCodeImageFormat, which developers commonly use to embed data in QR codes for web links, authentication, or product information. Ideal for scenarios where secure, programmatic barcode generation is required before serving the image to clients.
// Prompt: Generate QR Code barcode and secure endpoint with token authentication before serving image.
// Tags: qr code, barcode generation, token authentication, png, aspose.barcode, qr error correction, image output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code image after validating a token.
/// </summary>
class Program
{
    // Expected token for authentication
    private const string ExpectedToken = "secure123";

    /// <summary>
    /// Entry point. Validates the token, creates a QR Code, and saves it as a PNG file.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may contain the authentication token.</param>
    static void Main(string[] args)
    {
        // Retrieve token from command‑line arguments; use a default if none provided.
        string token = args.Length > 0 ? args[0] : "secure123";

        // Simple token validation
        if (!string.Equals(token, ExpectedToken, StringComparison.Ordinal))
        {
            Console.WriteLine("Invalid token. Access denied.");
            return;
        }

        // Data to encode in the QR code
        string qrData = "https://example.com";

        // Output file path
        string outputPath = "qr_code.png";

        // Generate QR code with high error correction level
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrData))
        {
            // Set QR error correction to Level H (high)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Configure image size using interpolation mode
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 300f;

            // Save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"QR code generated and saved to '{Path.GetFullPath(outputPath)}'.");
    }
}