// Title: Reset QualitySettings and Process Barcode Image Batch
// Description: Demonstrates generating QR code images, resetting the reader's quality settings to defaults, and decoding them in a batch.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and QualitySettings for controlling read quality. Typical scenarios include batch processing of scanned images where consistent quality defaults are required. Developers often need to reset quality presets before each read operation to ensure predictable results across diverse image sets.
// Prompt: Implement a method that resets all QualitySettings to defaults before processing a new image batch.
// Tags: barcode, qr, batch processing, qualitysettings, generation, recognition, aspose.barcode, c#

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that generates QR code images, resets quality settings, and reads the barcodes in a batch.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a temporary folder, generates sample barcodes, and processes them.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Generate sample barcode images and collect their file paths
        List<string> barcodeFiles = GenerateSampleBarcodes(batchFolder, 3);

        // Process each generated barcode file
        ProcessBatch(barcodeFiles);
    }

    /// <summary>
    /// Generates a specified number of QR code images in the given folder.
    /// </summary>
    /// <param name="folder">Destination folder for the generated images.</param>
    /// <param name="count">Number of barcode images to create.</param>
    /// <returns>List of file paths for the generated barcode images.</returns>
    static List<string> GenerateSampleBarcodes(string folder, int count)
    {
        var files = new List<string>();
        for (int i = 1; i <= count; i++)
        {
            // Build the file path for the current barcode image
            string filePath = Path.Combine(folder, $"barcode_{i}.png");

            // Create a QR code with sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, $"Sample{i}"))
            {
                // Set a modest X-dimension for better readability
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Save the barcode as a PNG file
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            files.Add(filePath);
        }
        return files;
    }

    /// <summary>
    /// Reads each barcode file, resets quality settings to defaults, and outputs decoding results.
    /// </summary>
    /// <param name="files">Collection of barcode image file paths to process.</param>
    static void ProcessBatch(List<string> files)
    {
        foreach (string file in files)
        {
            // Verify that the file exists before attempting to read it
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            // Specify the expected barcode type for faster recognition
            BaseDecodeType decodeType = DecodeType.QR;

            // Initialize the reader for the current file
            using (var reader = new BarCodeReader(file, decodeType))
            {
                // Reset quality settings to the default preset (NormalQuality)
                ResetQualitySettings(reader);

                // Iterate through all detected barcodes in the image
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(file)} | CodeText: {result.CodeText} | Type: {result.CodeTypeName} | Quality: {result.ReadingQuality}");
                }
            }
        }
    }

    /// <summary>
    /// Resets the reader's quality settings to the default NormalQuality preset.
    /// </summary>
    /// <param name="reader">The BarCodeReader instance whose settings are to be reset.</param>
    static void ResetQualitySettings(BarCodeReader reader)
    {
        // Assign the default quality preset
        reader.QualitySettings = QualitySettings.NormalQuality;
    }
}