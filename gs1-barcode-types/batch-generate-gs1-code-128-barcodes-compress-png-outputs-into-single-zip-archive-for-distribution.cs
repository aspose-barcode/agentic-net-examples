// Title: Batch generate GS1 Code 128 barcodes and zip them
// Description: Generates multiple GS1 Code 128 barcodes as PNG files and compresses them into a single ZIP archive for easy distribution.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to use the BarcodeGenerator class with EncodeTypes.GS1Code128 to create barcodes, customize parameters (e.g., checksum display), and save them in PNG format. It also shows how to package the generated images using System.IO.Compression.ZipArchive. Developers working with product identification, inventory, or logistics often need to produce GS1-compliant barcodes in bulk and deliver them as a single archive.
// Prompt: Batch generate GS1 Code 128 barcodes, compress PNG outputs into a single ZIP archive for distribution.
// Tags: gs1, code128, barcode, generation, png, zip, aspose.barcode

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch creation of GS1 Code 128 barcodes and compression of the resulting PNG files into a ZIP archive.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcode images from predefined GS1 data strings,
    /// saves them as PNG files, and archives them into a single ZIP file.
    /// </summary>
    static void Main()
    {
        // Define sample GS1 Code 128 data strings using Application Identifier (AI) format.
        List<string> gs1Data = new List<string>
        {
            "(01)12345678901231",                     // GTIN only
            "(01)98765432109876(10)ABC123",           // GTIN + Batch/Lot
            "(01)55555555555555(21)SN001",            // GTIN + Serial Number
            "(01)11111111111111(17)230101",           // GTIN + Expiration Date
            "(01)22222222222222(3103)001500"          // GTIN + Net weight (kg)
        };

        // Create an output directory for the generated PNG files.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(outputDir);

        // Iterate over each GS1 data string and generate a corresponding barcode image.
        for (int i = 0; i < gs1Data.Count; i++)
        {
            string codeText = gs1Data[i];
            string fileName = $"barcode_{i + 1}.png";
            string filePath = Path.Combine(outputDir, fileName);

            // Initialize the barcode generator with GS1 Code 128 symbology and the current data string.
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
            {
                // Ensure the checksum is always displayed (optional visual requirement).
                generator.Parameters.Barcode.ChecksumAlwaysShow = true;

                // Save the generated barcode as a PNG file.
                generator.Save(filePath);
            }
        }

        // Define the path for the ZIP archive that will contain all generated PNG files.
        string zipPath = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes.zip");

        // Create the ZIP archive and add each PNG file as an entry.
        using (var zipStream = new FileStream(zipPath, FileMode.Create))
        using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create, leaveOpen: false))
        {
            foreach (string file in Directory.GetFiles(outputDir, "*.png"))
            {
                string entryName = Path.GetFileName(file);
                archive.CreateEntryFromFile(file, entryName);
            }
        }

        // Output summary information to the console.
        Console.WriteLine($"Generated {gs1Data.Count} GS1 Code 128 barcodes in '{outputDir}'.");
        Console.WriteLine($"Compressed into ZIP archive: {zipPath}");
    }
}