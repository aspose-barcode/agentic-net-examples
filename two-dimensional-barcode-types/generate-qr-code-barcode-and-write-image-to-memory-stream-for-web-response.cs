// Title: Generate QR Code barcode and output as PNG stream
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, configuring high error correction, and saving the image to a memory stream suitable for returning in a web response.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with QR symbology. It shows typical steps such as setting encoding parameters, choosing an image format, and writing the result to a MemoryStream for web APIs or other in‑memory processing scenarios. Developers often need these patterns when integrating barcode images into web pages, REST services, or email attachments.
// Prompt: Generate a QR Code barcode and write image to memory stream for web response.
// Tags: qr code, barcode generation, memory stream, png, aspose.barcode, encode types, web response

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates QR Code generation using Aspose.BarCode and writing the image to a memory stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR Code for a URL, sets high error correction, saves as PNG to a MemoryStream, and writes the size to console.
    /// </summary>
    static void Main()
    {
        // Define the text to encode (URL)
        string codeText = "https://example.com";

        // Initialize the barcode generator with QR symbology and the text to encode
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set the QR error correction level to high (Level H)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Create a memory stream to hold the generated PNG image
            using (var memoryStream = new MemoryStream())
            {
                // Save the barcode image into the memory stream in PNG format
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Output the size of the generated image (useful for debugging or logging)
                Console.WriteLine($"QR code image generated. Size: {memoryStream.Length} bytes.");
            }
        }
    }
}