// Title: Generate QR Code and Export as PNG Memory Stream
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, saving it as a PNG image into a MemoryStream, and retrieving the byte array for API consumption.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It showcases the use of the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce QR Code images. Typical scenarios include embedding barcodes in web APIs, storing them in databases, or sending them over network services where an in‑memory image representation is required.
// Prompt: Generate QR Code barcode and export image to memory stream for API consumption.
// Tags: qr code, barcode generation, memory stream, png, aspose.barcode, image export

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode and exports it as a PNG image in a memory stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates QR code, saves to MemoryStream, and outputs size and Base64 string.
    /// </summary>
    static void Main()
    {
        // Text to encode in the QR code
        string qrText = "https://example.com";

        // MemoryStream to hold the generated image
        using (MemoryStream ms = new MemoryStream())
        {
            // Create QR code generator with the desired text
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
            {
                // Optional: set error correction level (Level M provides a good balance of data capacity and resilience)
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Save the barcode image as PNG into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Reset stream position to the beginning for subsequent reading
            ms.Position = 0;

            // Retrieve the image bytes (suitable for API consumption, storage, etc.)
            byte[] imageBytes = ms.ToArray();

            // Output information to the console (size and Base64 representation)
            Console.WriteLine($"Generated QR code image size: {imageBytes.Length} bytes");
            Console.WriteLine("Base64 PNG:");
            Console.WriteLine(Convert.ToBase64String(imageBytes));
        }
    }
}