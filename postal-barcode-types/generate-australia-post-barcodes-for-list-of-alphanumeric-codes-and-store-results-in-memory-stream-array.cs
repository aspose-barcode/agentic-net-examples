// Title: Generate Australia Post barcodes and store in memory streams
// Description: Demonstrates creating Australia Post barcodes from a list of alphanumeric codes and saving each barcode as a PNG image in a MemoryStream.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on the Australia Post symbology. It shows how to configure barcode parameters such as X‑dimension, bar height, and the CTable encoding for customer information, then render the barcode to a PNG image using the BarcodeGenerator class. Developers working with postal barcode standards can use this pattern to produce barcodes programmatically for batch processing or web services.
// Prompt: Generate Australia Post barcodes for a list of alphanumeric codes and store results in a memory stream array.
// Tags: australia post, barcode generation, png, aspose.barcode, memory stream, ctable

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates Australia Post barcodes for a set of codes
/// and stores each barcode image in a <see cref="MemoryStream"/> as PNG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Iterates over sample Australia Post codes, creates a barcode for each,
    /// saves it to a memory stream, and reports the number of successfully generated barcodes.
    /// </summary>
    static void Main()
    {
        // Sample Australia Post codes (FCC + 8‑digit DPID + optional customer info)
        List<string> codes = new List<string>
        {
            "1101234567",            // FCC 11, no customer info
            "5901234567AB",          // FCC 59, CTable (2 letters)
            "6201234567ASPO",        // FCC 62, CTable (4 letters)
            "5901234567ABCDE",       // FCC 59, CTable (5 letters, max)
            "6201234567XYZ"          // FCC 62, CTable (3 letters)
        };

        // Collection to hold the generated barcode images
        List<MemoryStream> streams = new List<MemoryStream>();

        // Process each code individually
        foreach (string code in codes)
        {
            try
            {
                // Create a generator for the Australia Post symbology with the current code
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, code))
                {
                    // Configure basic appearance
                    generator.Parameters.Barcode.XDimension.Pixels = 4f;   // width of the smallest bar
                    generator.Parameters.Barcode.BarHeight.Pixels = 50f; // height of the bars

                    // Enable alphanumeric customer information using the CTable encoding
                    generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

                    // Render the barcode to a memory stream in PNG format
                    MemoryStream ms = new MemoryStream();
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0; // reset stream position for downstream consumers
                    streams.Add(ms);
                }
            }
            catch (Exception ex)
            {
                // Log any generation errors without terminating the whole process
                Console.WriteLine($"Failed to generate barcode for '{code}': {ex.Message}");
            }
        }

        // Summarize the result
        Console.WriteLine($"Generated {streams.Count} Australia Post barcodes.");

        // Clean up all memory streams before exiting
        foreach (MemoryStream ms in streams)
        {
            ms.Dispose();
        }
    }
}