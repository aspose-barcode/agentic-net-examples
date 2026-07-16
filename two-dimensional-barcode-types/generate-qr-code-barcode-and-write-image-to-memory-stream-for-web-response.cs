// Title: Generate QR Code and Write to MemoryStream
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, saving it as a PNG image into a MemoryStream for use in web responses.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to use the BarcodeGenerator class to encode data into QR Code symbology, configure error correction, and output the result as an image stream. Typical use cases include generating barcodes on-the-fly for web APIs, embedding images in JSON responses, or serving them directly to browsers. Developers often need to control image format, stream handling, and response headers, which this snippet illustrates.
// Prompt: Generate a QR Code barcode and write image to memory stream for web response.
// Tags: qr code, barcode generation, image output, memory stream, aspose.barcode, png, web response

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a QR Code barcode and writes the PNG image to a memory stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR Code, saves it to a MemoryStream, and outputs its size and Base64 representation.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the QR Code.
        string codeText = "https://example.com";

        // Initialize the barcode generator for QR Code symbology with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set the QR Code error correction level (optional, LevelM provides a good balance).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Create a memory stream to hold the generated PNG image.
            using (var memoryStream = new MemoryStream())
            {
                // Save the barcode image into the memory stream in PNG format.
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Reset the stream position to the beginning for subsequent reads.
                memoryStream.Position = 0;

                // Output the size of the generated image (useful for setting Content-Length in HTTP responses).
                Console.WriteLine($"QR code image size: {memoryStream.Length} bytes");

                // Convert the image bytes to a Base64 string (commonly used in JSON payloads or data URIs).
                string base64Image = Convert.ToBase64String(memoryStream.ToArray());
                Console.WriteLine(base64Image);
            }
        }
    }
}