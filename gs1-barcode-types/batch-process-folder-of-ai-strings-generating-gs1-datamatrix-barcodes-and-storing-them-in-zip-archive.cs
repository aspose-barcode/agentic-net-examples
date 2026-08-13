// Title: Generate GS1 DataMatrix barcodes from AI strings in batch and zip them
// Description: This example reads AI (Application Identifier) strings from text files, creates GS1 DataMatrix barcodes for each, and stores the PNG images in a ZIP archive.
// Category-Description: Demonstrates batch barcode generation using Aspose.BarCode. It covers reading input files, using BarcodeGenerator with EncodeTypes.GS1DataMatrix, configuring colors, saving images to streams, and packaging results with System.IO.Compression. Ideal for developers needing to automate barcode creation for inventory, shipping, or compliance scenarios.
// Prompt: Batch process a folder of AI strings, generating GS1 DataMatrix barcodes and storing them in a ZIP archive.
// Tags: gs1, datamatrix, barcode, generation, batch processing, zip, output, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.IO.Compression;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates batch creation of GS1 DataMatrix barcodes from AI strings and packaging them into a ZIP file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads AI strings from text files, generates barcodes, and writes PNG images into a ZIP archive.
    /// </summary>
    /// <param name="args">Optional command‑line argument specifying the input folder path.</param>
    static void Main(string[] args)
    {
        // Determine input folder (argument or default)
        string inputFolder = args.Length > 0 ? args[0] : "InputAIStrings";
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        // Seed sample AI strings if folder is empty (rule 110)
        string[] txtFiles = Directory.GetFiles(inputFolder, "*.txt");
        if (txtFiles.Length == 0)
        {
            string[] sampleAi = new[]
            {
                "(01)00123456789012", // GTIN-14
                "(01)01234567890128", // GTIN-14 with valid check digit
                "(01)00012345678905", // GTIN-14
                "(01)00001234567890", // GTIN-14
                "(01)00000123456789"  // GTIN-14
            };
            for (int i = 0; i < sampleAi.Length; i++)
            {
                string filePath = Path.Combine(inputFolder, $"Sample{i + 1}.txt");
                File.WriteAllText(filePath, sampleAi[i]);
            }
            txtFiles = Directory.GetFiles(inputFolder, "*.txt");
        }

        // Prepare output ZIP file
        string outputZipPath = "GS1DataMatrixBarcodes.zip";
        using (var zipStream = new FileStream(outputZipPath, FileMode.Create))
        using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create, leaveOpen: true))
        {
            // Process each AI string file
            foreach (string txtFile in txtFiles)
            {
                // Read AI string (trim whitespace)
                string aiString = File.ReadAllText(txtFile).Trim();
                if (string.IsNullOrEmpty(aiString))
                {
                    Console.WriteLine($"Skipping empty file: {txtFile}");
                    continue;
                }

                // Generate GS1 DataMatrix barcode
                using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, aiString))
                {
                    // Optional: set colors or dimensions if needed
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                    generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                    // Save barcode image to memory stream
                    using (var ms = new MemoryStream())
                    {
                        generator.Save(ms, BarCodeImageFormat.Png);
                        ms.Position = 0;

                        // Add image to ZIP with same base name but .png extension
                        string entryName = Path.GetFileNameWithoutExtension(txtFile) + ".png";
                        var entry = archive.CreateEntry(entryName, CompressionLevel.Optimal);
                        using (var entryStream = entry.Open())
                        {
                            ms.CopyTo(entryStream);
                        }
                    }
                }

                Console.WriteLine($"Processed: {Path.GetFileName(txtFile)}");
            }
        }

        Console.WriteLine($"All barcodes have been saved to {outputZipPath}");
    }
}