// Title: Batch generation of GS1 Code 128 barcodes and ZIP compression
// Description: Demonstrates creating multiple GS1 Code 128 barcode images, saving them as PNG files, and packaging them into a single ZIP archive for easy distribution.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator with EncodeTypes.GS1Code128, configure barcode parameters, export images via BarCodeImageFormat, and combine output files using System.IO.Compression. Typical use cases include bulk barcode creation for inventory, shipping labels, or product catalogs where developers need to automate image generation and deliver assets in a compressed package.
// Prompt: Batch generate GS1 Code 128 barcodes, compress PNG outputs into a single ZIP archive for distribution.
// Tags: gs1,code128,barcode,generation,png,zip,compression,aspose.barcode,aspose.drawing

using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch generation of GS1 Code 128 barcodes and compressing the resulting PNG images into a ZIP archive.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary folder, generates barcodes, saves PNGs, and creates a ZIP file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample GS1 Code 128 codetext with valid GTIN-14 (AI 01)
        string gs1CodeText = "(01)01234567890128";

        // List to hold generated file paths
        List<string> barcodeFiles = new List<string>();

        // Generate a batch of 5 barcodes
        for (int i = 0; i < 5; i++)
        {
            // Define the output file path for each barcode image
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");

            // Initialize the barcode generator with GS1 Code 128 symbology and the sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, gs1CodeText))
            {
                // Set the X-dimension (module width) to 2 pixels for better readability
                generator.Parameters.Barcode.XDimension.Pixels = 2f;

                // Save the generated barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Add the generated file path to the collection for later zipping
            barcodeFiles.Add(filePath);
        }

        // Create ZIP archive containing the PNG files
        string zipPath = Path.Combine(tempFolder, "Barcodes.zip");
        using (var zip = ZipFile.Open(zipPath, ZipArchiveMode.Create))
        {
            foreach (string file in barcodeFiles)
            {
                // Add each PNG file to the ZIP archive using its file name
                zip.CreateEntryFromFile(file, Path.GetFileName(file));
            }
        }

        // Output the result location
        Console.WriteLine($"Generated {barcodeFiles.Count} barcodes and compressed into: {zipPath}");
    }
}