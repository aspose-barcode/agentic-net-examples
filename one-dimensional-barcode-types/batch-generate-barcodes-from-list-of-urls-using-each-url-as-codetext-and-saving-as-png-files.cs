// Title: Batch QR Code generation from URL list
// Description: Demonstrates how to generate QR code barcodes for a collection of URLs and save each as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showing how to use the BarcodeGenerator class with EncodeTypes.QR to create QR code images. Typical use cases include bulk creation of QR codes for marketing, inventory, or link sharing. Developers often need to loop through data sources, configure output settings, and store the resulting images in a file system.
// Prompt: Batch generate barcodes from a list of URLs, using each URL as CodeText and saving as PNG files.
// Tags: qr code, barcode generation, batch processing, png output, aspose.barcode, encode types, file saving

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates QR code barcodes for a list of URLs
/// and saves each barcode as a PNG file in a temporary folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Iterates over a predefined list of URLs,
    /// creates a QR code for each, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define a sample collection of URLs to be encoded as QR codes.
        List<string> urls = new List<string>
        {
            "https://example.com",
            "https://openai.com",
            "https://github.com",
            "https://dotnet.microsoft.com",
            "https://aspose.com"
        };

        // Build a unique temporary output directory for the generated barcode images.
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine($"Saving barcodes to: {outputFolder}");

        // Iterate through the URL list, generating and saving a QR code for each entry.
        for (int i = 0; i < urls.Count; i++)
        {
            string url = urls[i];
            string fileName = $"barcode_{i + 1}.png";
            string filePath = Path.Combine(outputFolder, fileName);

            // Create a BarcodeGenerator configured for QR encoding with the current URL as the code text.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, url))
            {
                // Optional: customize appearance (e.g., pixel size) if required.
                // generator.Parameters.Barcode.XDimension.Pixels = 4f;

                // Save the generated barcode image as a PNG file.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Generated barcode for \"{url}\" -> {filePath}");
        }

        Console.WriteLine("Batch barcode generation completed.");
    }
}