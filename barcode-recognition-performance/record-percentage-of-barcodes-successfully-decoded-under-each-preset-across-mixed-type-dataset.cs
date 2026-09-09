// Title: Barcode Generation and Recognition with Quality Presets Evaluation
// Description: Demonstrates generating several barcode symbologies, saving them as PNG images, and measuring the decoding success rate using different quality preset settings.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and QualitySettings presets to control recognition performance. Developers often need to balance speed and accuracy when processing mixed‑type barcode datasets; this snippet illustrates how to evaluate those trade‑offs across common symbologies.
// Prompt: Record the percentage of barcodes successfully decoded under each preset across a mixed‑type dataset.
// Tags: barcode, generation, recognition, qualitysettings, png, aspose.barcode, symbology, decode, preset

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates a set of barcode images of various symbologies, then evaluates
/// the decoding success rate using different Aspose.BarCode quality presets.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary barcode files, runs
    /// recognition with several presets, reports success percentages, and
    /// cleans up the temporary data.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // List to hold generated file paths
        List<string> barcodeFiles = new List<string>();

        // Define sample barcodes (symbology and code text)
        var samples = new List<(BaseEncodeType type, string text)>
        {
            (EncodeTypes.Code128, "Sample123"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.DataMatrix, "DM12345"),
            (EncodeTypes.Aztec, "AztecDemo"),
            (EncodeTypes.Pdf417, "PDF417Sample")
        };

        // Generate barcode images and store their file paths
        foreach (var (type, text) in samples)
        {
            string filePath = Path.Combine(tempFolder, $"{type}_{Guid.NewGuid().ToString("N")}.png");
            using (var generator = new BarcodeGenerator(type, text))
            {
                // Set a modest X‑dimension for better readability
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Define recognition presets to be tested
        var presets = new List<(string name, QualitySettings settings)>
        {
            ("HighPerformance", QualitySettings.HighPerformance),
            ("NormalQuality", QualitySettings.NormalQuality),
            ("HighQuality", QualitySettings.HighQuality),
            ("MaxQuality", QualitySettings.MaxQuality)
        };

        // Evaluate each preset against the generated barcode set
        foreach (var (presetName, presetSettings) in presets)
        {
            int successCount = 0;

            foreach (string file in barcodeFiles)
            {
                if (!File.Exists(file))
                {
                    Console.WriteLine($"File not found: {file}");
                    continue;
                }

                // Initialize the reader with all supported decode types
                using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                {
                    // Apply the current quality preset
                    reader.QualitySettings = presetSettings;

                    // Attempt to read barcodes from the image
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // Count as success if at least one barcode is decoded
                    if (results != null && results.Length > 0)
                    {
                        successCount++;
                    }
                }
            }

            // Calculate and display the success percentage for the preset
            double percentage = (double)successCount / barcodeFiles.Count * 100.0;
            Console.WriteLine($"{presetName}: {percentage:F2}% ({successCount}/{barcodeFiles.Count}) successfully decoded.");
        }

        // Clean up temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to delete temporary folder: {ex.Message}");
        }
    }
}