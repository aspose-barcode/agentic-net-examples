// Title: Generate Codabar barcodes for product codes and package them into a zip file
// Description: Demonstrates how to create Codabar barcode images from a list of product identifiers and store each PNG image in a zip archive.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.Codabar, configuring optional parameters, and saving images in PNG format. Typical use cases include batch barcode creation for inventory, retail, or logistics systems where multiple barcodes need to be generated and delivered as a single archive. Developers often need to combine barcode generation with .NET compression APIs to automate distribution of barcode assets.
// Prompt: Iterate through a list of product codes, generate Codabar barcodes, and store each in a zip archive.
// Tags: codabar, barcode generation, zip archive, png, aspose.barcode, batch processing

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Program that generates Codabar barcodes for a set of product codes and saves them into a zip archive.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes, writes them as PNG images into a zip file, and outputs the archive location.
    /// </summary>
    static void Main()
    {
        // Define a list of product codes to be encoded as Codabar barcodes.
        var productCodes = new List<string>
        {
            "A12345B",
            "C67890D",
            "A11111A"
        };

        // Determine the full path for the output zip archive.
        string zipPath = Path.Combine(Directory.GetCurrentDirectory(), "CodabarBarcodes.zip");

        // Ensure any existing archive with the same name is removed before creating a new one.
        if (File.Exists(zipPath))
        {
            File.Delete(zipPath);
        }

        // Create a file stream for the zip archive and open a ZipArchive in update mode.
        using (FileStream zipFile = new FileStream(zipPath, FileMode.Create))
        {
            using (ZipArchive archive = new ZipArchive(zipFile, ZipArchiveMode.Update))
            {
                // Iterate over each product code, generate a barcode, and add it to the archive.
                foreach (string code in productCodes)
                {
                    // Initialize the barcode generator for Codabar symbology with the current code.
                    using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Codabar, code))
                    {
                        // Optional: configure start/stop symbols if needed.
                        // generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.A;
                        // generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.A;

                        // Save the generated barcode image to a memory stream in PNG format.
                        using (MemoryStream ms = new MemoryStream())
                        {
                            generator.Save(ms, BarCodeImageFormat.Png);
                            ms.Position = 0; // Reset stream position for reading.

                            // Create a new entry in the zip archive for the current barcode image.
                            ZipArchiveEntry entry = archive.CreateEntry($"{code}.png", CompressionLevel.Optimal);
                            using (Stream entryStream = entry.Open())
                            {
                                // Copy the PNG data from the memory stream into the zip entry.
                                ms.CopyTo(entryStream);
                            }
                        }
                    }
                }
            }
        }

        // Inform the user where the zip archive has been created.
        Console.WriteLine($"Zip archive created at: {zipPath}");
    }
}