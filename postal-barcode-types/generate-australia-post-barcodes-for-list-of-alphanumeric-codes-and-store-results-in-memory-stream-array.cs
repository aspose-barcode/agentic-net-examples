using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of Australia Post barcodes using Aspose.BarCode and
/// writes each barcode image to a memory stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates sample barcodes, stores them in memory streams,
    /// displays stream sizes, and disposes of the streams.
    /// </summary>
    static void Main()
    {
        // Define a collection of sample alphanumeric codes for Australia Post barcodes.
        var codes = new List<string>
        {
            "5912345678AB",
            "5912345678CD",
            "5912345678EF",
            "5912345678GH",
            "5912345678IJ"
        };

        // List to hold the memory streams that will contain the generated barcode images.
        var memoryStreams = new List<MemoryStream>();

        // Iterate over each code and generate a corresponding barcode image.
        foreach (var code in codes)
        {
            // Create a BarcodeGenerator for Australia Post format with the current code.
            using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, code))
            {
                // Set the encoding table to CTable for interpreting customer information.
                generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.CTable;

                // Save the generated barcode image to a memory stream in PNG format.
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for subsequent reading.

                // Add the prepared stream to the collection.
                memoryStreams.Add(ms);
            }
        }

        // Convert the list of streams to an array as required by subsequent processing.
        MemoryStream[] barcodeStreams = memoryStreams.ToArray();

        // Output the length of each stream to demonstrate that data has been written.
        for (int i = 0; i < barcodeStreams.Length; i++)
        {
            Console.WriteLine($"Barcode {i + 1}: Stream length = {barcodeStreams[i].Length} bytes");
        }

        // Dispose of all memory streams to release resources (good practice).
        foreach (var ms in barcodeStreams)
        {
            ms.Dispose();
        }
    }
}