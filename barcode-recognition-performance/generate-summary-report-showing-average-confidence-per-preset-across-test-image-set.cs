// Title: Barcode confidence summary report
// Description: Generates several barcode images, reads them, and reports the average confidence per barcode type.
// Prompt: Generate a summary report showing average confidence per preset across a test image set.
// Tags: barcode symbology, confidence analysis, summary report, aspose.barcode, csharp

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing; // required for Aspose.BarCode image handling

/// <summary>
/// Demonstrates how to generate barcodes, read them, and calculate the average confidence per preset.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcode images, reads them, and prints average confidence values.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory to store generated barcode images.
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        if (!Directory.Exists(tempDir))
            Directory.CreateDirectory(tempDir);

        // Define a small set of barcode presets to generate and test.
        var presets = new List<(BaseEncodeType EncodeType, string CodeText, string FileName)>
        {
            (EncodeTypes.Code128, "ABC123", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.DataMatrix, "DM12345", "datamatrix.png"),
            (EncodeTypes.Pdf417, "PDF417 Sample Text", "pdf417.png")
        };

        // Generate barcode images for each preset.
        foreach (var preset in presets)
        {
            string filePath = Path.Combine(tempDir, preset.FileName);
            using (var generator = new BarcodeGenerator(preset.EncodeType, preset.CodeText))
            {
                // Use default settings; ensure image is saved as PNG.
                generator.Save(filePath);
            }
        }

        // Dictionary to accumulate confidence values per preset (using CodeTypeName as key).
        var confidenceMap = new Dictionary<string, List<int>>(StringComparer.OrdinalIgnoreCase);

        // Recognize each generated image and collect confidence data.
        foreach (var preset in presets)
        {
            string filePath = Path.Combine(tempDir, preset.FileName);
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Warning: File not found - {filePath}");
                continue;
            }

            using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    string presetName = result.CodeTypeName; // e.g., "Code128", "QR", etc.
                    int confidenceValue = (int)result.Confidence; // Enum underlying int.

                    if (!confidenceMap.ContainsKey(presetName))
                        confidenceMap[presetName] = new List<int>();

                    confidenceMap[presetName].Add(confidenceValue);
                }
            }
        }

        // Output average confidence per preset.
        Console.WriteLine("Average Confidence per Preset:");
        foreach (var kvp in confidenceMap)
        {
            string presetName = kvp.Key;
            List<int> values = kvp.Value;
            double average = values.Count > 0 ? (double)values.Sum() / values.Count : 0.0;
            Console.WriteLine($"{presetName}: {average:F2}");
        }

        // Cleanup temporary files (optional).
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignore any cleanup errors.
        }
    }
}