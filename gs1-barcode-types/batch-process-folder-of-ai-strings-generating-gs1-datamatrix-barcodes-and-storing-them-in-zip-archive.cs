// Title: Batch generate GS1 DataMatrix barcodes from AI strings and archive them as PNGs in a ZIP file
// Description: Demonstrates how to read GS1 Application Identifier strings from text files, create GS1 DataMatrix barcodes, and package the resulting PNG images into a ZIP archive.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on batch processing and file output. It showcases the BarcodeGenerator class with EncodeTypes.GS1DataMatrix, configuring XDimension, and saving images using BarCodeImageFormat. Typical use cases include automating label creation, inventory tagging, and bulk barcode production where input data is stored in files.
// Prompt: Batch process a folder of AI strings, generating GS1 DataMatrix barcodes and storing them in a ZIP archive.
// Tags: gs1datamatrix, batch-processing, zip-archive, png, barcode-generation, aspose.barcode, aspnet

using System;
using System.IO;
using System.IO.Compression;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates batch creation of GS1 DataMatrix barcodes from AI string files
/// and packaging the resulting PNG images into a ZIP archive.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample AI string files, creates barcodes,
    /// and stores them in a ZIP archive.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch process
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Create sample AI string files (5 items)
        int sampleCount = 5;
        var inputFiles = new string[sampleCount];
        for (int i = 0; i < sampleCount; i++)
        {
            string filePath = Path.Combine(batchFolder, $"Item{i + 1}.txt");
            // Example GS1 DataMatrix code text: GTIN (01) + serial (21)
            string codeText = $"(01)1234567890123{i}(21)ITEM{i + 1}";
            File.WriteAllText(filePath, codeText);
            inputFiles[i] = filePath;
        }

        // Path for the output ZIP archive
        string zipPath = Path.Combine(batchFolder, "Barcodes.zip");

        // Create ZIP archive and process each input file
        using (var zip = ZipFile.Open(zipPath, ZipArchiveMode.Create))
        {
            foreach (string inputFile in inputFiles)
            {
                if (!File.Exists(inputFile))
                {
                    Console.WriteLine($"Warning: File not found - {inputFile}");
                    continue;
                }

                string codeText;
                try
                {
                    // Read and trim the AI string from the file
                    codeText = File.ReadAllText(inputFile).Trim();
                    if (string.IsNullOrEmpty(codeText))
                    {
                        Console.WriteLine($"Warning: Empty code text in file - {inputFile}");
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Warning: Failed to read file {inputFile}: {ex.Message}");
                    continue;
                }

                try
                {
                    // Initialize the barcode generator for GS1 DataMatrix
                    using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
                    {
                        // Set module size (XDimension) – using pixels as float
                        generator.Parameters.Barcode.XDimension.Pixels = 8f;

                        using (var ms = new MemoryStream())
                        {
                            // Save barcode image to memory stream in PNG format
                            generator.Save(ms, BarCodeImageFormat.Png);
                            ms.Position = 0;

                            // Create entry in ZIP archive with the same base name as the input file
                            string entryName = Path.GetFileNameWithoutExtension(inputFile) + ".png";
                            var entry = zip.CreateEntry(entryName);
                            using (var entryStream = entry.Open())
                            {
                                ms.CopyTo(entryStream);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Warning: Failed to generate barcode for file {inputFile}: {ex.Message}");
                }
            }
        }

        Console.WriteLine($"Barcode ZIP archive created at: {zipPath}");
    }
}