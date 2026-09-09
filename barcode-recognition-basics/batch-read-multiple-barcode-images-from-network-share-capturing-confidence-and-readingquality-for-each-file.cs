// Title: Batch barcode generation and reading with confidence metrics
// Description: Demonstrates generating multiple barcode images, storing them in a temporary folder, and reading them back to capture confidence and reading quality values for each file.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for extracting information, including confidence and reading quality, from images. Typical scenarios include bulk processing of barcode assets, quality assessment, and automated verification in enterprise applications. Developers often need to batch‑process images, evaluate scan reliability, and handle various symbologies using the Aspose.BarCode API.
// Prompt: Batch read multiple barcode images from a network share, capturing Confidence and ReadingQuality for each file.
// Tags: barcode, batch, confidence, readingquality, generation, recognition, aspose.barcode, symbology

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates creating a set of barcode images, reading them back, and reporting confidence and reading quality metrics.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes, reads them, and outputs detailed results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch operation
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // List to hold the full paths of generated barcode image files
        List<string> barcodeFiles = new List<string>();

        // Sample data: each tuple contains the symbology, the text to encode, and the target file name
        var samples = new (BaseEncodeType encode, string text, string fileName)[]
        {
            (EncodeTypes.Code128, "Sample128", "code128.png"),
            (EncodeTypes.QR, "SampleQR", "qr.png"),
            (EncodeTypes.DataMatrix, "DM12345", "datamatrix.png"),
            (EncodeTypes.Pdf417, "PDF417Sample", "pdf417.png"),
            (EncodeTypes.Aztec, "AztecSample", "aztec.png")
        };

        // -----------------------------------------------------------------
        // Generate barcode images and store their paths in the list
        // -----------------------------------------------------------------
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(batchFolder, sample.fileName);
            using (var generator = new BarcodeGenerator(sample.encode, sample.text))
            {
                // Save each barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        Console.WriteLine("Batch barcode reading results:");

        // -----------------------------------------------------------------
        // Read each generated barcode image and display its properties
        // -----------------------------------------------------------------
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();

                    if (results.Length == 0)
                    {
                        Console.WriteLine($"No barcode detected in file: {Path.GetFileName(file)}");
                        continue;
                    }

                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"File: {Path.GetFileName(file)}");
                        Console.WriteLine($"  CodeType: {result.CodeTypeName}");
                        Console.WriteLine($"  CodeText: {result.CodeText}");
                        Console.WriteLine($"  Confidence: {result.Confidence}");
                        Console.WriteLine($"  ReadingQuality: {result.ReadingQuality}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Handles image loading failures (e.g., unsupported format)
                Console.WriteLine($"Failed to read file '{Path.GetFileName(file)}': {ex.Message}");
            }
        }

        // -----------------------------------------------------------------
        // Cleanup: delete the temporary folder and its contents
        // -----------------------------------------------------------------
        try
        {
            Directory.Delete(batchFolder, true);
        }
        catch
        {
            // If cleanup fails, ignore – the OS will eventually remove the temp folder
        }
    }
}