// Title: Generate Australia Post barcodes to memory streams
// Description: Demonstrates creating Australia Post barcodes for a set of alphanumeric codes and storing each image in a memory stream.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on the Australia Post symbology. It shows how to configure barcode parameters such as X‑dimension, bar height, and encoding table using the BarcodeGenerator class, then save the output to PNG format in memory. Developers working with postal barcode standards often need to generate images on‑the‑fly for batch processing, web services, or PDF embedding, and this snippet illustrates the typical workflow with Aspose.BarCode.
// Prompt: Generate Australia Post barcodes for a list of alphanumeric codes and store results in a memory stream array.
// Tags: australia post, barcode, generation, png, memory stream, aspose.barcode, encode types

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating Australia Post barcodes and storing them in memory streams.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes for predefined codes and returns them as an array of <see cref="MemoryStream"/> objects.
    /// </summary>
    static void Main()
    {
        // Define a list of sample Australia Post codes (FCC 59 with CTable customer info)
        List<string> codes = new List<string>
        {
            "5901234567AB",
            "5902345678CD",
            "5903456789EF",
            "5904567890GH",
            "5905678901IJ"
        };

        // Collection to hold the generated memory streams
        List<MemoryStream> streams = new List<MemoryStream>();

        // Iterate over each code and generate a barcode image
        foreach (string code in codes)
        {
            // Create a generator for the Australia Post barcode symbology
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, code))
            {
                // Configure basic appearance: X‑dimension and bar height
                generator.Parameters.Barcode.XDimension.Pixels = 4;
                generator.Parameters.Barcode.BarHeight.Pixels = 50;

                // Use the CTable encoding table (allows letters and digits)
                generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

                // Save the barcode image to a memory stream in PNG format
                MemoryStream ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for later reading

                // Add the stream to the collection
                streams.Add(ms);
            }
        }

        // Convert the list of streams to an array for easier consumption
        MemoryStream[] barcodeStreams = streams.ToArray();

        Console.WriteLine($"Generated {barcodeStreams.Length} Australia Post barcode images in memory.");

        // Clean up: dispose each memory stream after use
        foreach (MemoryStream ms in barcodeStreams)
        {
            ms.Dispose();
        }
    }
}