// Title: Generate QR Code and Export to MemoryStream
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode and saving it directly to a MemoryStream in PNG format for API consumption.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to use the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce QR Code images. Typical use cases include generating barcodes for web services, APIs, or any scenario where an image needs to be transmitted or stored in memory without writing to disk. Developers often need to create barcodes on the fly and pass the resulting image streams to other components or external systems.
// Prompt: Generate QR Code barcode and export image to memory stream for API consumption.
// Tags: qr code, generation, png, memory stream, aspose.barcode, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a QR Code barcode and writes the image to a memory stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR Code, saves it as PNG into a MemoryStream,
    /// and outputs the stream size.
    /// </summary>
    static void Main()
    {
        // Create a memory stream to hold the generated QR code image.
        using (var memoryStream = new MemoryStream())
        {
            // Initialize the QR code generator with the desired text.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
            {
                // Set QR error correction level (optional, LevelM provides a good balance).
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Save the QR code image directly to the memory stream in PNG format.
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Reset stream position for any subsequent reading.
            memoryStream.Position = 0;

            // Example output: display the size of the generated image.
            Console.WriteLine($"QR code image generated. Stream length: {memoryStream.Length} bytes.");
        }
    }
}