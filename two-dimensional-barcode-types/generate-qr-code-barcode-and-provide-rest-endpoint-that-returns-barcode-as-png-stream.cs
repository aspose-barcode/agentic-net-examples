// Title: Generate QR Code and expose as PNG via REST endpoint (demo)
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, encoding it as PNG, and showing how the image could be returned from a REST API as a stream.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and image export APIs. Typical scenarios include creating QR codes for URLs, product information, or authentication tokens, which developers often need to serve as image streams in web services or embed in HTML. The snippet shows how to configure error correction, generate a PNG, and obtain the binary data for HTTP responses.
// Prompt: Generate QR Code barcode and provide a REST endpoint that returns barcode as PNG stream.
// Tags: qr, barcode, generation, png, rest, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode and demonstrates how the PNG image
/// could be returned from a REST endpoint as a binary stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the console application.
    /// </summary>
    static void Main()
    {
        // In a real web application this code would be placed inside a controller action
        // that writes the PNG stream to the HTTP response with content type "image/png".
        // Here we simply generate the barcode and output a Base64 string for demonstration.

        const string codeText = "https://example.com";

        // Initialize the barcode generator for QR code with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Configure a high error correction level to improve scan reliability.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Create a memory stream to hold the generated PNG image.
            using (var ms = new MemoryStream())
            {
                // Save the barcode image as PNG into the memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Convert the PNG bytes to a Base64 string for console output.
                string base64 = Convert.ToBase64String(ms.ToArray());
                Console.WriteLine(base64);
            }
        }
    }
}