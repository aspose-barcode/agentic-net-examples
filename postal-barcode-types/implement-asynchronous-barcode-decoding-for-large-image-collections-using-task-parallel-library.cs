// Title: Asynchronous Barcode Decoding for Large Image Collections Using TPL
// Description: Demonstrates generating sample barcode images and decoding them asynchronously with the Task Parallel Library to efficiently handle many files.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Typical scenarios include batch processing of scanned documents, inventory management, and bulk image analysis where developers need high‑throughput barcode extraction using multi‑core CPUs.
// Prompt: Implement asynchronous barcode decoding for large image collections using Task Parallel Library.
// Tags: barcode, decoding, async, task parallel library, aspose.barcode, generation, recognition, image processing

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Sample program that generates a few barcode images and then decodes them asynchronously
/// using the Task Parallel Library (TPL). The example demonstrates how to configure
/// Aspose.BarCode for multi‑core processing and how to clean up temporary resources.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates sample barcodes, runs asynchronous
    /// decoding tasks, and cleans up temporary files.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static async Task Main(string[] args)
    {
        // --------------------------------------------------------------------
        // Create a dedicated temporary folder for sample barcodes
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // --------------------------------------------------------------------
        // Define barcode specifications to generate (type, text, file name)
        // --------------------------------------------------------------------
        var specs = new List<(BaseEncodeType encodeType, string codeText, string fileName)>
        {
            (EncodeTypes.Code128, "ABC123456", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.DataMatrix, "DM12345", "datamatrix.png")
        };

        var barcodeFiles = new List<string>();

        // --------------------------------------------------------------------
        // Generate sample barcode images and collect their file paths
        // --------------------------------------------------------------------
        foreach (var spec in specs)
        {
            string filePath = Path.Combine(tempFolder, spec.fileName);
            using (var generator = new BarcodeGenerator(spec.encodeType, spec.codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // --------------------------------------------------------------------
        // Configure Aspose.BarCode to use all available CPU cores for reading
        // --------------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

        // --------------------------------------------------------------------
        // Asynchronously decode all barcode images using TPL
        // --------------------------------------------------------------------
        var decodeTasks = new List<Task>();
        foreach (string file in barcodeFiles)
        {
            // Each file is processed on a thread‑pool thread
            decodeTasks.Add(Task.Run(() => DecodeBarcodeAsync(file)));
        }

        // Wait for all decoding tasks to complete
        await Task.WhenAll(decodeTasks);

        // --------------------------------------------------------------------
        // Clean up temporary files and folder
        // --------------------------------------------------------------------
        foreach (string file in barcodeFiles)
        {
            try { File.Delete(file); } catch { }
        }
        try { Directory.Delete(tempFolder, true); } catch { }
    }

    /// <summary>
    /// Reads a barcode image file and writes the decoded results to the console.
    /// </summary>
    /// <param name="imagePath">Full path to the barcode image.</param>
    static void DecodeBarcodeAsync(string imagePath)
    {
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        try
        {
            // Initialize the reader for all supported barcode types
            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                BarCodeResult[] results = reader.ReadBarCodes();
                foreach (var result in results)
                {
                    Console.WriteLine($"File: {Path.GetFileName(imagePath)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                }
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Failed to read '{imagePath}': {ex.Message}");
        }
    }
}