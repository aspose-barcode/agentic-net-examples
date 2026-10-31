// Title: Barcode detection count per image across quality presets
// Description: Demonstrates generating sample barcodes, recognizing them with different quality settings, and recording how many barcodes are detected per image.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating images, BarCodeReader for decoding, and QualitySettings presets to control recognition performance and accuracy. Typical use cases include batch processing of scanned documents, quality analysis of barcode scanners, and automated testing of detection consistency across different settings.
// Prompt: Record the number of barcodes detected per image under each preset to analyze consistency.
// Tags: barcode symbology, recognition, quality settings, aspose.barcode, generation, detection, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating barcode images, recognizing them with various quality presets,
/// and recording detection counts per image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, runs recognition with different quality settings,
    /// and outputs detection counts.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images and collect their file paths
        var imageFiles = new List<string>();
        GenerateBarcodeImage(EncodeTypes.Code128, "CODE128_SAMPLE", Path.Combine(tempFolder, "code128.png"));
        imageFiles.Add(Path.Combine(tempFolder, "code128.png"));
        GenerateBarcodeImage(EncodeTypes.QR, "QR_SAMPLE_TEXT", Path.Combine(tempFolder, "qr.png"));
        imageFiles.Add(Path.Combine(tempFolder, "qr.png"));
        GenerateBarcodeImage(EncodeTypes.DataMatrix, "DM_SAMPLE", Path.Combine(tempFolder, "datamatrix.png"));
        imageFiles.Add(Path.Combine(tempFolder, "datamatrix.png"));

        // Define recognition quality presets to be tested
        var presets = new Dictionary<string, QualitySettings>
        {
            { "HighPerformance", QualitySettings.HighPerformance },
            { "NormalQuality", QualitySettings.NormalQuality },
            { "HighQuality", QualitySettings.HighQuality },
            { "MaxQuality", QualitySettings.MaxQuality }
        };

        // Store detection counts per image for each preset
        var results = new Dictionary<string, Dictionary<string, int>>();

        // Iterate over each quality preset
        foreach (var presetEntry in presets)
        {
            string presetName = presetEntry.Key;
            QualitySettings preset = presetEntry.Value;
            var countsPerImage = new Dictionary<string, int>();

            // Process each generated image with the current preset
            foreach (string filePath in imageFiles)
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File not found: {filePath}");
                    countsPerImage[Path.GetFileName(filePath)] = 0;
                    continue;
                }

                try
                {
                    // Initialize the reader with all relevant symbologies
                    using (var reader = new BarCodeReader(
                        filePath,
                        DecodeType.Code128,
                        DecodeType.QR,
                        DecodeType.DataMatrix,
                        DecodeType.Aztec,
                        DecodeType.Pdf417,
                        DecodeType.Planet,
                        DecodeType.MicroQR))
                    {
                        // Apply the current quality setting
                        reader.QualitySettings = preset;

                        // Perform recognition and count detected barcodes
                        var barcodes = reader.ReadBarCodes();
                        countsPerImage[Path.GetFileName(filePath)] = barcodes.Length;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading {filePath} with preset {presetName}: {ex.Message}");
                    countsPerImage[Path.GetFileName(filePath)] = 0;
                }
            }

            // Save the counts for this preset
            results[presetName] = countsPerImage;
        }

        // Output the collected detection data
        Console.WriteLine("Barcode detection counts per image under each quality preset:");
        foreach (var presetResult in results)
        {
            Console.WriteLine($"Preset: {presetResult.Key}");
            foreach (var imageCount in presetResult.Value)
            {
                Console.WriteLine($"  Image: {imageCount.Key} - Detected: {imageCount.Value}");
            }
        }

        // Cleanup temporary files and folder
        try
        {
            foreach (var file in imageFiles)
            {
                if (File.Exists(file))
                    File.Delete(file);
            }
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }

    /// <summary>
    /// Generates a barcode image using the specified encoding type and text.
    /// </summary>
    /// <param name="encodeType">The barcode symbology to encode.</param>
    /// <param name="codeText">The text or data to encode.</param>
    /// <param name="outputPath">The file path where the image will be saved.</param>
    static void GenerateBarcodeImage(BaseEncodeType encodeType, string codeText, string outputPath)
    {
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // No additional parameters needed; default sizing adapts to content
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }
}