// Title: Generate Codabar Barcodes and Package into a ZIP Archive
// Description: This example iterates over a collection of product codes, creates Codabar barcode images for each code, and stores the PNG files in a temporary ZIP archive.
// Category-Description: Demonstrates Aspose.BarCode barcode generation using the BarcodeGenerator class with EncodeTypes.Codabar, configuring start/stop symbols, and saving images in PNG format. The generated images are then added to a ZIP file via System.IO.Compression. This pattern is common for batch barcode creation, archival, or distribution scenarios where developers need to produce multiple barcodes programmatically and deliver them as a single package.
// Prompt: Iterate through a list of product codes, generate Codabar barcodes, and store each in a zip archive.
// Tags: codabar, barcode generation, zip archive, png, aspose.barcode, csharp, file-io

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides an entry point that generates Codabar barcodes for a set of product codes
/// and stores the resulting PNG images in a ZIP archive.
/// </summary>
class Program
{
    /// <summary>
    /// Main method that performs barcode generation and ZIP packaging.
    /// </summary>
    static void Main()
    {
        // Define a sample list of product codes.
        // Codabar requires start/stop characters; these examples include them.
        List<string> productCodes = new List<string>
        {
            "A12345B",
            "C67890D",
            "A98765B",
            "C54321D",
            "A11223B"
        };

        // Determine a temporary path for the output ZIP file.
        string zipPath = Path.Combine(Path.GetTempPath(), "CodabarBarcodes.zip");

        // Remove any existing ZIP file to ensure a clean run.
        if (File.Exists(zipPath))
        {
            File.Delete(zipPath);
        }

        // Create a new ZIP archive for storing barcode images.
        using (FileStream zipFileStream = new FileStream(zipPath, FileMode.Create, FileAccess.ReadWrite))
        using (ZipArchive zipArchive = new ZipArchive(zipFileStream, ZipArchiveMode.Create))
        {
            // Iterate over each product code and generate a barcode.
            foreach (string code in productCodes)
            {
                // Initialize the barcode generator for Codabar symbology.
                using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, code))
                {
                    // Explicitly set start and stop symbols (optional, shown for clarity).
                    generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.A;
                    generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.A;

                    // Render the barcode to a memory stream in PNG format.
                    using (MemoryStream imageStream = new MemoryStream())
                    {
                        generator.Save(imageStream, BarCodeImageFormat.Png);
                        imageStream.Position = 0; // Reset stream position for reading.

                        // Create a new entry in the ZIP archive for this barcode image.
                        string entryName = $"{code}.png";
                        ZipArchiveEntry entry = zipArchive.CreateEntry(entryName, CompressionLevel.Optimal);

                        // Copy the PNG data into the ZIP entry.
                        using (Stream entryStream = entry.Open())
                        {
                            imageStream.CopyTo(entryStream);
                        }
                    }
                }
            }
        }

        // Inform the user where the ZIP file has been saved.
        Console.WriteLine($"Barcodes have been saved to zip file: {zipPath}");
    }
}