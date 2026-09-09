// Title: Generate Code128 barcode into a MemoryStream
// Description: Demonstrates creating a Code128 barcode, rendering it as PNG into a MemoryStream, and preparing the stream for use in a web API response.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator with EncodeTypes and BarCodeImageFormat to produce image data in memory. Developers often need to embed barcode images directly into HTTP responses or other streams without writing to disk, making in‑memory generation essential for web services and APIs.
// Prompt: Create a MemoryStream, render the barcode into it, and return the stream from a web API.
// Tags: code128, barcode generation, png, memorystream, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeConsoleApp
{
    /// <summary>
    /// Console application that generates a Code128 barcode and writes it to a MemoryStream.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the application. Generates a barcode, saves it to a MemoryStream, and outputs the stream length.
        /// </summary>
        static void Main()
        {
            // Define the text to encode in the barcode.
            string codeText = "12345678";

            // Create a MemoryStream to hold the generated barcode image.
            using (MemoryStream ms = new MemoryStream())
            {
                // Initialize the barcode generator with Code128 symbology and the desired text.
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
                {
                    // Save the barcode as a PNG image directly into the memory stream.
                    generator.Save(ms, BarCodeImageFormat.Png);
                }

                // Reset the stream position to the beginning for any subsequent read operations.
                ms.Position = 0;

                // Output the size of the generated barcode data for verification.
                Console.WriteLine($"Generated barcode stream length: {ms.Length} bytes");
            }
        }
    }
}