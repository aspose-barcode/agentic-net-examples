// Title: Asynchronous barcode reading with Aspose.BarCode
// Description: Generates sample barcode images and reads them asynchronously to maintain UI responsiveness.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates how to use the BarcodeGenerator to create barcodes and the BarCodeReader to decode them, employing asynchronous patterns (Task.Run) to avoid blocking the UI thread. Developers commonly need to process large numbers of images or handle user‑uploaded files without freezing the application, making async barcode reading a typical requirement.
// Prompt: Use asynchronous BarCodeReader methods to read uploaded files while preserving UI responsiveness.
// Tags: barcode, generation, recognition, async, aspose.barcode, csharp

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating barcode images and reading them asynchronously using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates sample barcodes, reads them asynchronously,
    /// and cleans up temporary files.
    /// </summary>
    static async Task Main(string[] args)
    {
        // Create a unique temporary folder for storing generated barcode images.
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample barcode data: type and associated text.
        var samples = new List<(BaseEncodeType encodeType, string text)>
        {
            (EncodeTypes.Code128, "Sample123"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.Pdf417, "PDF417 Sample Text")
        };

        var barcodeFiles = new List<string>();

        // Generate barcode images and collect their file paths.
        foreach (var (encodeType, text) in samples)
        {
            string filePath = Path.Combine(tempFolder, $"{encodeType}_{Guid.NewGuid().ToString("N")}.png");
            using (var generator = new BarcodeGenerator(encodeType, text))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Asynchronously read each generated barcode file.
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                // Initialize the reader for all supported barcode types.
                using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                {
                    // Perform the read operation on a background thread to avoid blocking.
                    BarCodeResult[] results = await Task.Run(() => reader.ReadBarCodes());

                    if (results.Length == 0)
                    {
                        Console.WriteLine($"No barcodes detected in {Path.GetFileName(file)}");
                    }
                    else
                    {
                        Console.WriteLine($"Barcodes in {Path.GetFileName(file)}:");
                        foreach (var result in results)
                        {
                            Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                        }
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                // Handle cases where the file format is unsupported.
                Console.WriteLine($"Skipping unsupported file: {file}");
            }
            catch (Exception ex)
            {
                // Log any unexpected errors during processing.
                Console.WriteLine($"Error processing {file}: {ex.Message}");
            }
        }

        // Optional cleanup of the temporary folder.
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any errors that occur during cleanup.
        }
    }
}