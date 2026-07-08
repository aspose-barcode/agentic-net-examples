// Title: Batch generate Code128 barcodes from URLs
// Description: Demonstrates how to create PNG barcode images for a collection of URLs, using each URL as the CodeText.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating batch processing with the BarcodeGenerator class. It shows typical use cases such as encoding multiple data items (e.g., URLs) into Code128 barcodes and saving them as image files, a common requirement for inventory, tracking, or QR code generation workflows.
// Prompt: Batch generate barcodes from a list of URLs, using each URL as CodeText and saving as PNG files.
// Tags: code128, batch-generation, png, barcodegenerator, aspose.barcode

using System;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of Code128 barcodes from a list of URLs
/// and saves each barcode as a PNG image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Iterates over a predefined list of URLs,
    /// creates a Code128 barcode for each, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define a sample collection of URLs to be encoded as barcodes.
        List<string> urls = new List<string>
        {
            "https://example.com/page1",
            "https://example.com/page2",
            "https://example.com/page3",
            "https://example.com/page4",
            "https://example.com/page5"
        };

        int index = 1; // Counter used to generate unique file names.

        // Process each URL in the list.
        foreach (string url in urls)
        {
            // Initialise a BarcodeGenerator for Code128 symbology with the current URL as the CodeText.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, url))
            {
                // Construct the output file name (e.g., barcode_1.png, barcode_2.png, ...).
                string fileName = $"barcode_{index}.png";

                // Save the generated barcode image in PNG format.
                generator.Save(fileName);

                // Inform the user about the successful generation.
                Console.WriteLine($"Generated barcode for URL '{url}' -> {fileName}");
            }

            index++; // Increment the file name counter.
        }
    }
}