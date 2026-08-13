// Title: Generate average confidence report for barcode presets
// Description: This example creates barcode images for several presets, reads them back, and calculates the average confidence score for each preset.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition workflows, covering BarcodeGenerator, BarCodeReader, and QualitySettings. Useful for developers testing barcode readability across different symbologies and evaluating scanner confidence metrics.
// Prompt: Generate a summary report showing average confidence per preset across a test image set.
// Tags: barcode, generation, recognition, confidence, preset, aspose.barcode, png

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates barcode images for predefined presets,
/// reads them back, and reports the average confidence per preset.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates barcodes, scans them, and prints average confidence values.
    /// </summary>
    static void Main()
    {
        // Folder to store generated barcode images
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Define presets: each preset has a name, symbology, and codetext
        var presets = new List<(string PresetName, BaseEncodeType EncodeType, string CodeText)>
        {
            ("Preset1", EncodeTypes.Code128, "CODE128_SAMPLE"),
            ("Preset2", EncodeTypes.QR, "QR_SAMPLE"),
            ("Preset3", EncodeTypes.DataMatrix, "DM_SAMPLE"),
            ("Preset4", EncodeTypes.Pdf417, "PDF417_SAMPLE"),
            ("Preset5", EncodeTypes.Aztec, "AZTEC_SAMPLE")
        };

        // Generate barcode images for each preset
        foreach (var (presetName, encodeType, codeText) in presets)
        {
            string fileName = $"{presetName}_{encodeType.TypeName}.png";
            string filePath = Path.Combine(folderPath, fileName);

            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Use default settings; ensure image is saved as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Dictionary to accumulate confidence sums and counts per preset
        var confidenceData = new Dictionary<string, (float Sum, int Count)>(StringComparer.OrdinalIgnoreCase);

        // Scan generated PNG files
        string[] imageFiles = Directory.GetFiles(folderPath, "*.png");
        foreach (string imageFile in imageFiles)
        {
            // Extract preset name from file name (format: PresetName_Symbology.png)
            string fileName = Path.GetFileNameWithoutExtension(imageFile);
            int underscoreIndex = fileName.IndexOf('_');
            if (underscoreIndex <= 0)
            {
                Console.WriteLine($"Warning: Unable to determine preset for file '{fileName}'. Skipping.");
                continue;
            }
            string presetName = fileName.Substring(0, underscoreIndex);

            // Read barcode from image
            using (var reader = new BarCodeReader(imageFile, DecodeType.AllSupportedTypes))
            {
                // Use normal quality preset for reliable results
                reader.QualitySettings = QualitySettings.NormalQuality;

                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // Convert confidence enum to numeric value
                    float confidenceValue = (float)result.Confidence;

                    if (confidenceData.TryGetValue(presetName, out var data))
                    {
                        data.Sum += confidenceValue;
                        data.Count += 1;
                        confidenceData[presetName] = data;
                    }
                    else
                    {
                        confidenceData[presetName] = (confidenceValue, 1);
                    }
                }
            }
        }

        // Output average confidence per preset
        Console.WriteLine("Average Confidence per Preset:");
        foreach (var kvp in confidenceData)
        {
            string preset = kvp.Key;
            float sum = kvp.Value.Sum;
            int count = kvp.Value.Count;
            float average = count > 0 ? sum / count : 0f;
            Console.WriteLine($"{preset}: {average:F2}");
        }
    }
}