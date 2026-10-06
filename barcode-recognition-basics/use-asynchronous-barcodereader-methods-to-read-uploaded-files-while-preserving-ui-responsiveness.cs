// Title: Asynchronous Barcode Reading from Generated Images
// Description: Demonstrates generating several barcode images and reading them asynchronously to keep the UI responsive.
// Category-Description: This example belongs to the Aspose.BarCode reading category, showcasing how to use BarCodeReader with asynchronous patterns. It covers generating barcodes, saving them, and processing multiple files concurrently using Task.WhenAll. Developers often need to read barcodes from user‑uploaded files without blocking the UI thread, and this snippet illustrates the typical API classes (BarcodeGenerator, BarCodeReader, DecodeType) and async task handling.
// Prompt: Use asynchronous BarCodeReader methods to read uploaded files while preserving UI responsiveness.
// Tags: barcode, async, read, decode, aspnet, aspose.barcode, task, ui-responsiveness

using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates asynchronous barcode reading using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, reads them asynchronously, and cleans up temporary files.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static async Task Main(string[] args)
    {
        // Create a unique temporary folder for sample barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the barcode symbologies to generate
        var encodeTypes = new List<BaseEncodeType>
        {
            EncodeTypes.Code128,
            EncodeTypes.QR,
            EncodeTypes.DataMatrix
        };

        var generatedFiles = new List<string>();

        // Generate sample barcode images and store their file paths
        int index = 1;
        foreach (var encode in encodeTypes)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{index}.png");
            using (var generator = new BarcodeGenerator(encode, $"Sample{index}"))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            generatedFiles.Add(filePath);
            index++;
        }

        // Asynchronously read each generated barcode file
        var readTasks = new List<Task>();
        foreach (string file in generatedFiles)
        {
            readTasks.Add(ReadBarcodeAsync(file));
        }

        // Wait for all read operations to complete
        await Task.WhenAll(readTasks);

        // Clean up temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors (e.g., files still in use)
        }
    }

    /// <summary>
    /// Reads barcodes from the specified file asynchronously.
    /// </summary>
    /// <param name="filePath">Full path to the barcode image file.</param>
    /// <returns>A task representing the asynchronous read operation.</returns>
    private static Task ReadBarcodeAsync(string filePath)
    {
        return Task.Run(() =>
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                return;
            }

            // Initialize the reader for all supported barcode types
            using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                BarCodeResult[] results = reader.ReadBarCodes();
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"{Path.GetFileName(filePath)}: {result.CodeTypeName} - {result.CodeText}");
                }
            }
        });
    }
}