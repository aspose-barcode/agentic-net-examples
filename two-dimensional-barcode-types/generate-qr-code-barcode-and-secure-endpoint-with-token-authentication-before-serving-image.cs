using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR code image and outputting it as a Base64 string after simple token authentication.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Validates a token passed via command‑line arguments,
    /// generates a QR code for a predefined URL, and writes the PNG image as a Base64 string to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments where the first argument may contain an authentication token.</param>
    static void Main(string[] args)
    {
        // Expected token for authentication
        const string expectedToken = "secret-token";

        // Retrieve token from command‑line arguments or use a default fallback
        string providedToken = args.Length > 0 ? args[0] : "secret-token";

        // Verify that the provided token matches the expected token
        if (!string.Equals(providedToken, expectedToken, StringComparison.Ordinal))
        {
            Console.WriteLine("Unauthorized: invalid token.");
            return;
        }

        // QR code content (sample URL)
        const string qrContent = "https://example.com";

        // Create a BarcodeGenerator for a QR code with the specified content
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrContent))
        {
            // Set optional parameters: error correction level and image resolution
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
            generator.Parameters.Resolution = 300f;

            // Write the generated barcode to a memory stream
            using (var ms = new MemoryStream())
            {
                // Save the barcode as a PNG image into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);

                // Convert the memory stream to a byte array
                byte[] imageBytes = ms.ToArray();

                // Encode the PNG bytes as a Base64 string (simulating an HTTP response body)
                string base64 = Convert.ToBase64String(imageBytes);
                Console.WriteLine("QR Code Image (Base64 PNG):");
                Console.WriteLine(base64);
            }
        }
    }
}