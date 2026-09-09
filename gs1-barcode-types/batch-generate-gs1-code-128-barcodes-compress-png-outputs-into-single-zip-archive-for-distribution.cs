// Title: Batch Generation of GS1 Code 128 Barcodes with ZIP Compression
// Description: Demonstrates how to generate multiple GS1 Code 128 barcodes as PNG images using Aspose.BarCode and package them into a single ZIP file for distribution.
// Category-Description: This example belongs to the batch barcode creation category of Aspose.BarCode, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce high‑volume barcode images. Typical scenarios include preparing product identifiers for inventory systems, printing labels in bulk, or delivering barcode assets to partners. Developers often need to automate image generation and archive results efficiently, which this snippet illustrates.
// Prompt: Batch generate GS1 Code 128 barcodes, compress PNG outputs into a single ZIP archive for distribution.
// Tags: gs1 code 128, batch generation, png, zip, aspose.barcode, barcodegenerator, encode types

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a set of GS1 Code 128 barcodes, saves them as PNG files,
/// and compresses all images into a single ZIP archive.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary PNG barcodes and archives them.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "GS1Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample GS1 Code 128 data strings
        List<string> codeTexts = new List<string>
        {
            "(01)12345678901231(21)ITEM001",
            "(01)98765432109876(21)ITEM002",
            "(01)55555555555555(21)ITEM003",
            "(01)11111111111111(21)ITEM004",
            "(01)22222222222222(21)ITEM005"
        };

        // Keep track of generated file paths for later zipping
        List<string> generatedFiles = new List<string>();

        // Generate PNG barcodes
        for (int i = 0; i < codeTexts.Count; i++)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{i + 1}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeTexts[i]))
            {
                // Set X-dimension to improve readability
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                // Save the barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            generatedFiles.Add(filePath);
        }

        // Create ZIP archive containing the generated PNGs
        string zipPath = Path.Combine(Directory.GetCurrentDirectory(), "GS1Barcodes.zip");
        using (FileStream zipStream = new FileStream(zipPath, FileMode.Create))
        using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
        {
            foreach (string file in generatedFiles)
            {
                try
                {
                    // Add each PNG file to the archive using its file name
                    archive.CreateEntryFromFile(file, Path.GetFileName(file));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to add {file} to ZIP: {ex.Message}");
                }
            }
        }

        Console.WriteLine($"Generated {generatedFiles.Count} barcodes and compressed into: {zipPath}");

        // Cleanup temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}