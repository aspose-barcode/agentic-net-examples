// Title: Generate Codabar barcodes and package them into a ZIP archive
// Description: Demonstrates iterating over product codes, creating Codabar barcode images, and storing each image in a zip file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce images. Typical use cases include batch barcode creation for inventory, shipping labels, or product catalogs, where developers need to automate image output and archive results. The example also illustrates using System.IO.Compression to bundle generated files, a common requirement in bulk processing scenarios.
// Prompt: Iterate through a list of product codes, generate Codabar barcodes, and store each in a zip archive.
// Tags: codabar, barcode generation, zip archive, batch processing, aspose.barcode, image output

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Program that generates Codabar barcodes for a list of product codes and saves them into a zip archive.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes, writes them to a zip file, and reports the result.
    /// </summary>
    static void Main()
    {
        // Define a sample list of product codes (replace with actual data as needed)
        List<string> productCodes = new List<string>
        {
            "A12345B",
            "C67890D",
            "E11223F",
            "G44556H",
            "I77889J"
        };

        // Path for the output ZIP archive
        string zipPath = "CodabarBarcodes.zip";

        // Create the ZIP archive file stream
        using (FileStream zipFileStream = new FileStream(zipPath, FileMode.Create))
        {
            // Initialize the ZIP archive in create mode
            using (ZipArchive zipArchive = new ZipArchive(zipFileStream, ZipArchiveMode.Create, leaveOpen: true))
            {
                // Iterate over each product code to generate its barcode
                foreach (string code in productCodes)
                {
                    // Initialize the barcode generator for Codabar symbology
                    using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Codabar, code))
                    {
                        // Save the generated barcode image to a memory stream in PNG format
                        using (MemoryStream imageStream = new MemoryStream())
                        {
                            generator.Save(imageStream, BarCodeImageFormat.Png);
                            imageStream.Position = 0; // Reset stream position for reading

                            // Create a new entry in the ZIP archive for this barcode image
                            ZipArchiveEntry entry = zipArchive.CreateEntry($"{code}.png");
                            using (Stream entryStream = entry.Open())
                            {
                                // Copy the image data into the ZIP entry
                                imageStream.CopyTo(entryStream);
                            }
                        }
                    }
                }
            }
        }

        // Inform the user about the successful generation
        Console.WriteLine($"Generated {productCodes.Count} Codabar barcodes and saved to '{zipPath}'.");
    }
}