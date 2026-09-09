// Title: Generate QR Code with 300 DPI Resolution and Save to Memory Stream
// Description: Demonstrates how to configure Aspose.BarCode's BarcodeGenerator to produce a QR code at 300 dpi, render it as a PNG image, and write the result into a MemoryStream.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to create barcode images. Typical scenarios include generating QR codes for web pages, mobile apps, or printing, where developers need precise control over image resolution and output format. The snippet shows best‑practice steps for setting resolution, saving to a stream, and retrieving image size.
// Prompt: Set BarcodeGenerator resolution to 300 dpi, generate QR code, and write bitmap to memory stream.
// Tags: qr, barcode, generation, resolution, png, memory-stream, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a QR code at 300 dpi and writes the PNG image to a memory stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, saves it to a stream, and reports the byte size.
    /// </summary>
    static void Main()
    {
        // Initialize the barcode generator for QR code with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Set the image resolution to 300 dots per inch.
            generator.Parameters.Resolution = 300f;

            // Create a memory stream to hold the generated PNG image.
            using (var memoryStream = new MemoryStream())
            {
                // Save the barcode as a PNG image into the memory stream.
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Output the size of the generated PNG image in bytes.
                Console.WriteLine($"Generated QR code PNG size: {memoryStream.Length} bytes");
            }
        }
    }
}