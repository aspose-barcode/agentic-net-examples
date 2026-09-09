// Title: Generate QR Code and export to memory stream
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, setting a high error correction level, and saving the image to a MemoryStream for further API consumption.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical use cases include generating barcodes on-the-fly for web services, embedding them in documents, or transmitting them via APIs. Developers often need to create barcodes in memory without writing to disk, and this snippet illustrates that common scenario.
// Prompt: Generate QR Code barcode and export image to memory stream for API consumption.
// Tags: qr code, barcode generation, memory stream, aspose.barcode, png, error correction

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR Code barcode and writes it to a memory stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR Code with high error correction and saves it as PNG into a MemoryStream.
    /// </summary>
    static void Main()
    {
        // QR code content
        const string codeText = "https://example.com";

        // Initialize barcode generator for QR code
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set error correction level to high (Level H)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Create a memory stream to hold the generated image
            using (var memoryStream = new MemoryStream())
            {
                // Save the barcode image as PNG into the stream
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Output the size of the generated image stream
                Console.WriteLine($"QR code generated. Stream length: {memoryStream.Length} bytes.");
            }
        }
    }
}