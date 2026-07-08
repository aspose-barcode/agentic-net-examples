// Title: Generate Barcode Image into MemoryStream for Web API
// Description: Demonstrates creating a Code128 barcode, rendering it to a PNG image stored in a MemoryStream, and returning the stream as would be done in a web API.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, BarCodeImageFormat, and related parameter classes to produce barcode images on the fly. Typical use cases include generating barcodes for invoices, shipping labels, or tickets within ASP.NET Core endpoints. Developers often need to stream the image directly to HTTP responses without writing to disk.
// Prompt: Create a MemoryStream, render the barcode into it, and return the stream from a web API.
// Tags: code128, barcode generation, png, memorystream, aspose.barcode, aspnet

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeApiSimulation
{
    /// <summary>
    /// Simulates the core logic of a web API that generates a barcode image and returns it as a <see cref="MemoryStream"/>.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Generates a barcode image for the specified text, writes it to a <see cref="MemoryStream"/> in PNG format,
        /// and returns the stream positioned at the beginning for reading.
        /// </summary>
        /// <param name="codeText">The text to encode in the barcode.</param>
        /// <returns>A <see cref="MemoryStream"/> containing the PNG barcode image.</returns>
        static MemoryStream GenerateBarcodeStream(string codeText)
        {
            // Create a memory stream to hold the generated image.
            var memoryStream = new MemoryStream();

            // Initialize the barcode generator with Code128 symbology and the provided text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Optional: customize the barcode's appearance.
                generator.Parameters.Barcode.BarColor = Color.Blue;   // Set barcode bars to blue.
                generator.Parameters.BackColor = Color.White;        // Set background to white.

                // Save the barcode image into the memory stream as a PNG.
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position so callers can read from the beginning.
            memoryStream.Position = 0;
            return memoryStream;
        }

        /// <summary>
        /// Entry point for the console demonstration. Generates a sample barcode and writes its size to the console.
        /// In a real web API, the returned <see cref="MemoryStream"/> would be sent to the client.
        /// </summary>
        /// <param name="args">Command‑line arguments (not used).</param>
        static void Main(string[] args)
        {
            const string sampleText = "123ABC";

            // Generate the barcode stream for the sample text.
            using (MemoryStream barcodeStream = GenerateBarcodeStream(sampleText))
            {
                // Output the length of the generated stream for verification.
                Console.WriteLine($"Generated barcode stream length: {barcodeStream.Length} bytes");
                // In a real API, the stream would be returned directly to the HTTP response.
            }
        }
    }
}