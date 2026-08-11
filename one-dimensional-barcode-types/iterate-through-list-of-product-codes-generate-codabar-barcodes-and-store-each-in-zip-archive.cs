// Title: Generate Codabar Barcodes and Package into a ZIP Archive
// Description: Demonstrates iterating over product codes, creating Codabar barcode images with Aspose.BarCode, and storing each PNG in a ZIP file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and barcode parameters to produce images. Typical use cases include batch barcode creation for inventory, shipping labels, or product catalogs, where developers need to automate image generation and archive the results. The code also illustrates combining .NET's System.IO.Compression to create ZIP archives of the generated files.
// Prompt: Iterate through a list of product codes, generate Codabar barcodes, and store each in a zip archive.
// Tags: codabar, barcode generation, zip archive, png, aspose.barcode, csharp

using System;
using System.IO;
using System.IO.Compression;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates Codabar barcode images for a set of product codes
/// and packages the resulting PNG files into a ZIP archive.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates barcodes, saves them to memory streams, and adds them to a ZIP file.
    /// </summary>
    static void Main()
    {
        // Define a sample list of product codes to encode as Codabar barcodes
        string[] productCodes = new string[]
        {
            "A12345",
            "B67890",
            "C24680",
            "D13579",
            "E11223"
        };

        // Target ZIP file name
        string zipPath = "CodabarBarcodes.zip";

        // Remove any existing ZIP file to ensure a fresh archive
        if (File.Exists(zipPath))
        {
            File.Delete(zipPath);
        }

        // Create a new ZIP archive and add each generated barcode image as an entry
        using (FileStream zipFileStream = new FileStream(zipPath, FileMode.Create))
        using (ZipArchive archive = new ZipArchive(zipFileStream, ZipArchiveMode.Create))
        {
            // Iterate through each product code
            foreach (string code in productCodes)
            {
                // Generate a Codabar barcode for the current product code
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Codabar, code))
                {
                    // Configure Codabar-specific parameters
                    generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.A;
                    generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.A;
                    generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;
                    generator.Parameters.Barcode.FilledBars = false;

                    // Save the barcode image to a memory stream in PNG format
                    using (MemoryStream imageStream = new MemoryStream())
                    {
                        generator.Save(imageStream, BarCodeImageFormat.Png);
                        imageStream.Position = 0; // Reset stream position for reading

                        // Create a new entry in the ZIP archive for this barcode image
                        ZipArchiveEntry entry = archive.CreateEntry($"{code}.png");
                        using (Stream entryStream = entry.Open())
                        {
                            // Copy the image data into the ZIP entry
                            imageStream.CopyTo(entryStream);
                        }
                    }
                }
            }
        }

        Console.WriteLine($"Barcode images have been saved to '{zipPath}'.");
    }
}