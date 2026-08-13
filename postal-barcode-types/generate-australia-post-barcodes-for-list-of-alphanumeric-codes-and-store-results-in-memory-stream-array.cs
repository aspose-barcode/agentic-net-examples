// Title: Generate Australia Post barcodes and store them in memory streams
// Description: Demonstrates how to create Australia Post barcodes from a list of alphanumeric codes using Aspose.BarCode and keep the PNG images in memory.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.AustraliaPost, setting the CustomerInformationInterpretingType, and saving images to MemoryStream. Developers often need to generate barcodes programmatically for mailing services, batch processing, or web APIs, and this pattern shows typical API classes and workflow for such scenarios.
// Prompt: Generate Australia Post barcodes for a list of alphanumeric codes and store results in a memory stream array.
// Tags: australia post, barcode generation, memory stream, png, aspose.barcode, csharp

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace AustraliaPostBarcodeDemo
{
    /// <summary>
    /// Demonstrates generating Australia Post barcodes from a set of codes and storing the PNG images in memory streams.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point. Generates barcodes for predefined codes, logs results, and returns an array of MemoryStream objects.
        /// </summary>
        static void Main()
        {
            // Sample list of valid Australia Post codes.
            // Format: FCC (2 digits) + DPID (8 digits) + optional customer info.
            var codes = new List<string>
            {
                "1100000000",          // FCC=11, no customer info
                "5980123456AB",        // FCC=59, 2 CTable chars
                "6280123456ABCDE",     // FCC=62, 5 CTable chars (max)
                "9280123456AB"         // FCC=92, 2 CTable chars
            };

            // Container for the generated barcode images.
            var barcodeStreams = new List<MemoryStream>();

            // Iterate over each code and generate the corresponding barcode.
            foreach (var code in codes)
            {
                try
                {
                    // Create a generator for Australia Post barcode with the given code text.
                    using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, code))
                    {
                        // Use CTable encoding for customer information (optional).
                        generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

                        // Generate the barcode image into a memory stream (PNG format).
                        var ms = new MemoryStream();
                        generator.Save(ms, BarCodeImageFormat.Png);
                        ms.Position = 0; // Reset position for later reading.

                        // Store the stream for later use.
                        barcodeStreams.Add(ms);
                    }

                    Console.WriteLine($"Successfully generated barcode for code: {code}");
                }
                catch (Exception ex)
                {
                    // Handle any validation or generation errors gracefully.
                    Console.WriteLine($"Error generating barcode for code '{code}': {ex.Message}");
                }
            }

            // Convert the list to an array as required.
            MemoryStream[] barcodeArray = barcodeStreams.ToArray();

            Console.WriteLine($"Total barcodes generated: {barcodeArray.Length}");

            // Example usage of the generated streams (e.g., write sizes).
            for (int i = 0; i < barcodeArray.Length; i++)
            {
                Console.WriteLine($"Barcode {i + 1}: Stream length = {barcodeArray[i].Length} bytes");
            }

            // Note: The memory streams remain open; they will be disposed when the application exits.
        }
    }
}