// Title: Generate QR Code and export to memory stream
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, saving it as PNG into a MemoryStream for further API consumption.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use BarcodeGenerator with EncodeTypes.QR, configure QR error correction, and output the image to a stream. Developers commonly need to generate barcodes on the fly for web services, APIs, or in-memory processing without writing to disk. Key classes include BarcodeGenerator, EncodeTypes, QRErrorLevel, and BarCodeImageFormat, useful for scenarios like dynamic QR code creation for URLs or authentication tokens.
// Prompt: Generate QR Code barcode and export image to memory stream for API consumption.
// Tags: qr code, barcode generation, memory stream, png, aspose.barcode, encode types, qrcode, error correction

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR Code barcode and writes the image to a memory stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // The text to encode in the QR Code (e.g., a URL).
        const string codeText = "https://example.com";

        // Create a BarcodeGenerator for QR encoding with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Optional: set a high error correction level to improve readability under damage.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Prepare a memory stream to hold the generated PNG image.
            using (var memoryStream = new MemoryStream())
            {
                // Save the QR Code image into the memory stream in PNG format.
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Output the size of the generated image for verification.
                Console.WriteLine($"QR code image generated. Stream length: {memoryStream.Length} bytes.");
            }
        }
    }
}