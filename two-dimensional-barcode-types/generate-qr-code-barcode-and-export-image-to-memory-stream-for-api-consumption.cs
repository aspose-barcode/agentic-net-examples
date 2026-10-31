// Title: Generate QR Code and export to memory stream
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, setting a high error correction level, and saving the image to a MemoryStream for further API consumption.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with QR symbology. It shows typical steps such as configuring barcode parameters, selecting image format, and retrieving the generated image as a stream, which developers often need when integrating barcode images into web services, APIs, or other in‑memory workflows.
// Prompt: Generate QR Code barcode and export image to memory stream for API consumption.
// Tags: qr code, barcode generation, memory stream, aspose.barcode, image export, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeExample
{
    /// <summary>
    /// Demonstrates QR Code generation and saving to a memory stream using Aspose.BarCode.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point that creates a QR Code, configures error correction, and writes the PNG image to a MemoryStream.
        /// </summary>
        static void Main()
        {
            // Define the text to encode in the QR Code
            const string codeText = "Hello, Aspose!";

            // Initialize the barcode generator with QR symbology and the specified text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Set a high error correction level (Level H) for better resilience
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Create a memory stream to hold the generated PNG image
                using (var memoryStream = new MemoryStream())
                {
                    // Save the QR Code image into the memory stream in PNG format
                    generator.Save(memoryStream, BarCodeImageFormat.Png);

                    // Output the length of the stream for verification (optional)
                    Console.WriteLine($"QR Code generated. Stream length: {memoryStream.Length} bytes.");
                }
            }
        }
    }
}