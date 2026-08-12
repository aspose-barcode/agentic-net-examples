// Title: Generate QR Code and output as PNG via MemoryStream
// Description: Creates a QR Code barcode from a URL, saves it as a PNG image into a MemoryStream, and displays the generated size.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to use the BarcodeGenerator class with EncodeTypes.QR to produce QR Code barcodes. Typical use cases include creating barcode images for web responses, APIs, or dynamic content where the image must be held in memory rather than written to disk. Developers often need to configure QR parameters such as error correction level and serialize the result to common image formats like PNG.
// Prompt: Generate a QR Code barcode and write image to memory stream for web response.
// Tags: qr code, barcode generation, memory stream, png, aspose.barcode, image output, web response

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode, saving it to a MemoryStream as a PNG,
/// and outputting the resulting image size. Suitable for scenarios where the image
/// is returned directly in a web response.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Encodes a URL into a QR Code, writes the PNG image
    /// to a MemoryStream, and writes the stream length to the console.
    /// </summary>
    static void Main()
    {
        // The data to be encoded in the QR Code (e.g., a website URL).
        string qrText = "https://example.com";

        // Create a MemoryStream that will hold the generated PNG image.
        using (var memoryStream = new MemoryStream())
        {
            // Initialize the QR Code generator with the QR symbology.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Assign the text to be encoded.
                generator.CodeText = qrText;

                // Optional: configure the QR Code error correction level (Medium in this case).
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Save the generated barcode directly into the MemoryStream in PNG format.
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning for any subsequent reads.
            memoryStream.Position = 0;

            // Output the size of the generated PNG image to verify creation.
            Console.WriteLine($"QR Code PNG generated, size: {memoryStream.Length} bytes");
        }
    }
}