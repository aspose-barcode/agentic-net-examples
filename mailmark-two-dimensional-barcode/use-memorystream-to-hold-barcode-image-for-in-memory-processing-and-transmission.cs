// Title: Generate Code128 Barcode into MemoryStream
// Description: Demonstrates generating a Code128 barcode image, storing it in a MemoryStream, and accessing the raw bytes for further processing.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to create barcode images in memory. Typical use cases include preparing barcode images for network transmission, embedding in documents, or further image manipulation without writing to disk. Developers often need in‑memory processing to improve performance and simplify deployment in cloud or CI environments.
// Prompt: Use a MemoryStream to hold the barcode image for in‑memory processing and transmission.
// Tags: barcode symbology, generation, memorystream, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeMemoryStreamDemo
{
    /// <summary>
    /// Demonstrates creating a barcode image in memory using Aspose.BarCode.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point that generates a Code128 barcode, writes it to a MemoryStream,
        /// and outputs the size of the resulting PNG image.
        /// </summary>
        static void Main()
        {
            // Create a MemoryStream to hold the generated barcode image in memory
            using (var ms = new MemoryStream())
            {
                // Initialize a barcode generator for Code128 with the sample text "ABC123"
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
                {
                    // Configure visual appearance: blue foreground on white background
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;
                    generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                    // Save the generated barcode as a PNG image into the MemoryStream
                    generator.Save(ms, BarCodeImageFormat.Png);
                }

                // Reset the stream position to the beginning for subsequent reads
                ms.Position = 0;

                // Retrieve the image bytes from the MemoryStream
                byte[] imageBytes = ms.ToArray();

                // Output the size of the generated barcode image (useful for verification)
                Console.WriteLine($"Generated barcode image size: {imageBytes.Length} bytes");
                // The byte array can now be transmitted over a network, embedded in documents, etc.
            }
        }
    }
}