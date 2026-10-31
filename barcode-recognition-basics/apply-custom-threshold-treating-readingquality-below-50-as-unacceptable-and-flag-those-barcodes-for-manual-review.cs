// Title: Barcode generation and quality evaluation with manual review flag
// Description: Generates sample barcodes, reads them using Aspose.BarCode, and flags any barcode whose reading quality is below 50 for manual review.
// Category-Description: This example demonstrates core Aspose.BarCode operations: barcode generation (BarcodeGenerator) and recognition (BarCodeReader, BarCodeResult). It shows how to create barcodes of various symbologies, assess their reading quality, and implement custom quality thresholds—common tasks for developers building automated scanning solutions that require quality control and exception handling.
// Prompt: Apply a custom threshold treating ReadingQuality below 50 as unacceptable and flag those barcodes for manual review.
// Tags: barcode generation, barcode recognition, reading quality, manual review, aspose.barcode, c#, code128, qr, interleaved2of5

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating barcodes, reading them, and flagging low‑quality scans for manual review.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates temporary barcodes, evaluates their reading quality, and cleans up.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for sample barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // List to hold generated barcode image file paths
        List<string> barcodeFiles = new List<string>();

        // Generate sample barcode images of different symbologies
        GenerateBarcode(tempFolder, "Code128Sample", EncodeTypes.Code128, "ABC123456");
        GenerateBarcode(tempFolder, "QrSample", EncodeTypes.QR, "https://example.com");
        GenerateBarcode(tempFolder, "Interleaved2of5Sample", EncodeTypes.Interleaved2of5, "12345678");

        // Collect generated file paths (PNG files) into the list
        foreach (string file in Directory.GetFiles(tempFolder, "*.png"))
        {
            barcodeFiles.Add(file);
        }

        // Read each barcode and evaluate its reading quality
        foreach (string filePath in barcodeFiles)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Use BarCodeReader to decode all supported barcode types in the image
            using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    double quality = result.ReadingQuality;

                    // Flag barcodes with quality below 50; otherwise mark as OK
                    string message = quality < 50
                        ? $"[FLAG] Low quality ({quality}) - manual review required. File: {Path.GetFileName(filePath)}, Type: {result.CodeTypeName}, Text: {result.CodeText}"
                        : $"[OK] Quality ({quality}) - File: {Path.GetFileName(filePath)}, Type: {result.CodeTypeName}, Text: {result.CodeText}";

                    Console.WriteLine(message);
                }
            }
        }

        // Clean up temporary folder; ignore any errors as OS will eventually remove it
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // If deletion fails, ignore – folder will be cleaned up by the OS later
        }
    }

    /// <summary>
    /// Generates a barcode image using the specified symbology and saves it as PNG.
    /// </summary>
    /// <param name="folder">Destination folder for the image file.</param>
    /// <param name="fileName">Base name for the image file (without extension).</param>
    /// <param name="encodeType">Symbology to encode.</param>
    /// <param name="codeText">Text to encode in the barcode.</param>
    private static void GenerateBarcode(string folder, string fileName, BaseEncodeType encodeType, string codeText)
    {
        string filePath = Path.Combine(folder, fileName + ".png");
        using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
        {
            // No additional parameters needed; default settings provide adequate quality
            generator.Save(filePath, BarCodeImageFormat.Png);
        }
    }
}