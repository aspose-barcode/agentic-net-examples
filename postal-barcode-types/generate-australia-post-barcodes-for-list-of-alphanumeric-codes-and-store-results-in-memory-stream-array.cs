// Title: Generate Australia Post barcodes and store them in memory streams
// Description: Demonstrates creating Australia Post barcodes from a set of alphanumeric strings and keeping the PNG images in MemoryStream objects for further processing.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on the Australia Post symbology. It showcases the BarcodeGenerator class with EncodeTypes.AustraliaPost, configuring the CustomerInformationInterpretingType, and saving output to PNG format via BarCodeImageFormat. Developers often need to generate barcodes programmatically for shipping labels, batch processing, or integration with document workflows.
// Prompt: Generate Australia Post barcodes for a list of alphanumeric codes and store results in a memory stream array.
// Tags: barcode symbology, australia post, generation, png, memorystream, aspose.barcode

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates Australia Post barcodes from a predefined list of codes
/// and stores each PNG image in a <see cref="MemoryStream"/> collection.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcodes, writes them to files (optional),
    /// and disposes all allocated resources.
    /// </summary>
    static void Main()
    {
        // Define a sample list of valid Australia Post codes.
        var codeTexts = new List<string>
        {
            "1100000000",          // FCC=11, no customer info
            "4580123456",          // FCC=45, no customer info
            "5980123456AB",        // FCC=59, 2 CTable chars
            "6280123456ABCDE",     // FCC=62, 5 CTable chars (max)
            "9280123456AB"         // FCC=92, 2 CTable chars
        };

        // Collection that will hold the generated barcode images in memory.
        var streams = new List<MemoryStream>();

        // Iterate over each code and generate the corresponding barcode.
        foreach (var code in codeTexts)
        {
            try
            {
                // Initialise the generator for Australia Post symbology with the current code.
                using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, code))
                {
                    // Enable CTable interpreting type for customer information (allows letters).
                    generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

                    // Save the barcode image to a memory stream in PNG format.
                    var ms = new MemoryStream();
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0; // Reset stream position for later reading.

                    // Add the prepared stream to the collection.
                    streams.Add(ms);
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during barcode generation.
                Console.WriteLine($"Error generating barcode for '{code}': {ex.Message}");
            }
        }

        Console.WriteLine($"Generated {streams.Count} barcode images.");

        // OPTIONAL: Write each image to a physical file for verification.
        for (int i = 0; i < streams.Count; i++)
        {
            var fileName = $"AustraliaPost_{i + 1}.png";
            using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                streams[i].CopyTo(fileStream);
            }
            Console.WriteLine($"Saved {fileName}");
        }

        // Dispose all memory streams to free resources.
        foreach (var ms in streams)
        {
            ms.Dispose();
        }
    }
}