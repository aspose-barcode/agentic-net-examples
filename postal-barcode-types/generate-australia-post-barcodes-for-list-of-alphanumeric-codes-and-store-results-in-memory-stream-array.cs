// Title: Generate Australia Post Barcodes and Store in Memory Streams
// Description: Demonstrates how to create Australia Post barcodes from a list of alphanumeric codes using Aspose.BarCode and keep the generated PNG images in memory streams.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on the Australia Post symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and barcode parameters such as XDimension, BarHeight, and AustralianPost encoding table. Developers often need to generate barcodes programmatically for shipping labels, inventory, or tracking, and may require the images in memory for further processing or transmission.
// Prompt: Generate Australia Post barcodes for a list of alphanumeric codes and store results in a memory stream array.
// Tags: australia post,barcode,generation,memorystream,aspose.barcode,encoding,png

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an entry point that generates Australia Post barcodes for sample codes and stores them in memory streams.
/// </summary>
class Program
{
    /// <summary>
    /// Generates barcodes for predefined Australia Post codes, saves each as a PNG into a MemoryStream, and outputs basic information.
    /// </summary>
    static void Main()
    {
        // Define sample Australia Post codes (FCC + DPID + optional customer info)
        var codes = new List<string>
        {
            "1101234567",          // FCC 11, no customer info
            "5901234567AB",        // FCC 59, CTable customer info
            "6201234567AS",        // FCC 62, CTable customer info
            "6201234567ABCD",      // FCC 62, CTable (4 chars)
            "6201234567ABCD1"      // FCC 62, CTable (5 chars, max)
        };

        // Collection to hold the generated MemoryStream objects
        var streams = new List<MemoryStream>();

        // Iterate over each code and generate the corresponding barcode
        foreach (var code in codes)
        {
            // Initialize the barcode generator for Australia Post symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, code))
            {
                // Configure visual parameters
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Parameters.Barcode.BarHeight.Pixels = 50f;
                generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

                // Save the barcode image to a memory stream in PNG format
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);

                // Reset stream position for potential downstream reading
                ms.Position = 0;

                // Add the stream to the collection
                streams.Add(ms);
            }
        }

        // Output summary information
        Console.WriteLine($"Generated {streams.Count} Australia Post barcodes.");
        for (int i = 0; i < streams.Count; i++)
        {
            Console.WriteLine($"Barcode {i + 1}: {streams[i].Length} bytes");
        }

        // Dispose all memory streams to release resources
        foreach (var ms in streams)
        {
            ms.Dispose();
        }
    }
}