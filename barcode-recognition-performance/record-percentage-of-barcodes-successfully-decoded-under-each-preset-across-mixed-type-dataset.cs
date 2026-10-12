// Title: Barcode decoding success rate across quality presets
// Description: Demonstrates generating a mixed set of barcode images and measuring the percentage of barcodes successfully decoded using each Aspose.BarCode quality preset.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and QualitySettings to control recognition performance. Developers often need to evaluate how different quality presets affect decoding success across various symbologies, especially when processing large mixed‑type datasets.
// Prompt: Record the percentage of barcodes successfully decoded under each preset across a mixed‑type dataset.
// Tags: barcode, symbology, generation, recognition, qualitysettings, aspose.barcode, png

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a set of barcode images of different symbologies, then evaluates decoding success
/// percentages using various Aspose.BarCode quality presets.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary barcode images, runs recognition with
    /// multiple quality settings, reports success rates, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for generated barcode images
        string folder = Path.Combine(Path.GetTempPath(), "BarcodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(folder);

        // Define a mixed set of barcode types, their encoded texts, and friendly names
        var symbologies = new List<(BaseEncodeType encode, string text, string name)>
        {
            (EncodeTypes.Code128, "Code128Test", "Code128"),
            (EncodeTypes.QR, "QRTest123", "QR"),
            (EncodeTypes.DataMatrix, "DMTest", "DataMatrix"),
            (EncodeTypes.Aztec, "AztecTest", "Aztec"),
            (EncodeTypes.Pdf417, "Pdf417Test", "Pdf417")
        };

        var filePaths = new List<string>();

        // Generate barcode images and store their file paths
        foreach (var (encode, text, name) in symbologies)
        {
            string filePath = Path.Combine(folder, $"{name}.png");
            using (var generator = new BarcodeGenerator(encode, text))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            filePaths.Add(filePath);
        }

        // Define recognition quality presets to be evaluated
        var presets = new List<(QualitySettings preset, string name)>
        {
            (QualitySettings.HighPerformance, "HighPerformance"),
            (QualitySettings.NormalQuality, "NormalQuality"),
            (QualitySettings.HighQuality, "HighQuality"),
            (QualitySettings.MaxQuality, "MaxQuality")
        };

        int total = filePaths.Count;

        // Evaluate each preset by attempting to decode all generated barcodes
        foreach (var (preset, name) in presets)
        {
            int successCount = 0;
            foreach (string file in filePaths)
            {
                if (!File.Exists(file))
                    continue;

                using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                {
                    // Apply the current quality setting
                    reader.QualitySettings = preset;
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // Determine if at least one barcode was successfully decoded
                    bool success = false;
                    foreach (var result in results)
                    {
                        if (!string.IsNullOrEmpty(result.CodeText))
                        {
                            success = true;
                            break;
                        }
                    }

                    if (success)
                        successCount++;
                }
            }

            // Calculate and display the success percentage for the current preset
            double percent = (double)successCount / total * 100.0;
            Console.WriteLine($"{name}: {percent:F2}% success ({successCount}/{total})");
        }

        // Cleanup temporary files and folder
        try
        {
            Directory.Delete(folder, true);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}