// Title: Batch QR Code Generation from URL List
// Description: Demonstrates how to generate QR code barcodes for a collection of URLs and save each as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing bulk creation of barcodes using the BarcodeGenerator class with EncodeTypes.QR. Typical scenarios include encoding URLs for marketing materials, inventory tracking, or mobile scanning applications. Developers often need to iterate over data sets, configure code text, and export images in common formats such as PNG.
// Prompt: Batch generate barcodes from a list of URLs, using each URL as CodeText and saving as PNG files.
// Tags: qr-code, barcode-generation, batch-processing, png, aspose.barcode, csharp

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates QR code barcodes for a predefined list of URLs and saves each barcode as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Iterates through a list of URLs, creates a QR code for each,
    /// and writes the resulting image to the file system.
    /// </summary>
    static void Main()
    {
        // Define a sample collection of URLs to be encoded as QR codes.
        List<string> urls = new List<string>
        {
            "https://example.com/page1",
            "https://example.com/page2",
            "https://example.com/page3",
            "https://example.com/page4",
            "https://example.com/page5"
        };

        // Determine the output directory relative to the current working folder.
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");

        // Ensure the output directory exists; create it if it does not.
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        int index = 1; // Counter used to generate unique file names.

        // Process each URL in the list.
        foreach (string url in urls)
        {
            // Initialize a QR code generator for the current URL.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Assign the URL as the code text to be encoded.
                generator.CodeText = url;

                // Construct a safe file name using the index counter.
                string safeFileName = $"barcode_{index}.png";
                string filePath = Path.Combine(outputFolder, safeFileName);

                // Save the generated QR code as a PNG image.
                generator.Save(filePath, BarCodeImageFormat.Png);

                // Log the successful creation of the barcode file.
                Console.WriteLine($"Saved barcode for '{url}' to '{filePath}'");
            }

            index++; // Increment the file name counter for the next barcode.
        }

        // Indicate that the batch processing has finished.
        Console.WriteLine("Barcode generation completed.");
    }
}