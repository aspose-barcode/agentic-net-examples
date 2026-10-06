// Title: Batch generate GS1 Code 128 barcodes and zip them
// Description: Demonstrates generating multiple GS1 Code 128 barcodes as PNG images, then compressing them into a single ZIP archive for easy distribution.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.GS1Code128 to create barcodes, configure image parameters, and save them in PNG format. It also illustrates file handling with System.IO and compression using System.IO.Compression to package multiple barcode images into a ZIP file, a common requirement for batch processing and distribution scenarios.
// Prompt: Batch generate GS1 Code 128 barcodes, compress PNG outputs into a single ZIP archive for distribution.
// Tags: barcode, gs1code128, batch, png, zip, generation, aspose.barcode, compression

using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch creation of GS1 Code 128 barcodes and packaging them into a ZIP archive.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcode images, zips them, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "GS1Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample GS1 Code 128 data strings (each includes Application Identifiers)
        List<string> codeTexts = new List<string>
        {
            "(01)12345678901231(21)ITEM001",
            "(01)12345678901231(21)ITEM002",
            "(01)12345678901231(21)ITEM003",
            "(01)12345678901231(21)ITEM004",
            "(01)12345678901231(21)ITEM005"
        };

        // Keep track of generated file paths for later zipping
        List<string> generatedFiles = new List<string>();

        // Generate PNG barcodes for each data string
        for (int i = 0; i < codeTexts.Count; i++)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{i + 1}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeTexts[i]))
            {
                // Set X-dimension (module width) to 2 pixels for better readability
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                // Save the barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            generatedFiles.Add(filePath);
        }

        // Define the output ZIP archive path in the current working directory
        string zipPath = Path.Combine(Directory.GetCurrentDirectory(), "GS1Barcodes.zip");
        using (FileStream zipStream = new FileStream(zipPath, FileMode.Create))
        {
            using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
            {
                // Add each generated PNG file to the ZIP archive
                foreach (string file in generatedFiles)
                {
                    try
                    {
                        archive.CreateEntryFromFile(file, Path.GetFileName(file));
                    }
                    catch (Exception ex)
                    {
                        // Log any errors that occur while adding files to the archive
                        Console.WriteLine($"Failed to add {file} to ZIP: {ex.Message}");
                    }
                }
            }
        }

        // Inform the user about the successful operation
        Console.WriteLine($"Generated {generatedFiles.Count} barcodes and compressed into: {zipPath}");

        // Cleanup temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any cleanup errors (e.g., files in use)
        }
    }
}