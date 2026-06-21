using System;
using System.IO;
using System.Collections.Generic;
using System.Linq; // Required for Sum()
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating, recognizing, and evaluating confidence levels of various barcode symbologies using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcode images for a set of presets,
    /// reads them back to collect confidence values, and outputs the average confidence per preset.
    /// </summary>
    static void Main()
    {
        // Define a small set of barcode presets (symbologies) to test.
        BaseEncodeType[] presets = new BaseEncodeType[]
        {
            EncodeTypes.Code128,
            EncodeTypes.QR,
            EncodeTypes.DataMatrix,
            EncodeTypes.Pdf417,
            EncodeTypes.Aztec
        };

        // Create a temporary folder for generated barcode images.
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        if (!Directory.Exists(tempFolder))
        {
            Directory.CreateDirectory(tempFolder);
        }

        // Dictionary to hold confidence values per preset (key = preset name, value = list of confidence ints).
        var confidenceMap = new Dictionary<string, List<int>>();

        // Iterate over each barcode preset.
        foreach (BaseEncodeType preset in presets)
        {
            // Use the preset's type name as a dictionary key.
            string presetName = preset.TypeName;
            confidenceMap[presetName] = new List<int>();

            // Generate a barcode image for the current preset.
            string imagePath = Path.Combine(tempFolder, $"{presetName}.png");
            using (var generator = new BarcodeGenerator(preset, "Test123"))
            {
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }

            // Verify the image was successfully created.
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"Failed to create image for preset {presetName}.");
                continue;
            }

            // Recognize the barcode and collect confidence values.
            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // Confidence is an enum; cast to int for averaging.
                    int confidenceValue = (int)result.Confidence;
                    confidenceMap[presetName].Add(confidenceValue);
                }
            }

            // Clean up the generated image file.
            try
            {
                File.Delete(imagePath);
            }
            catch
            {
                // Ignore cleanup errors.
            }
        }

        // Output average confidence per preset.
        Console.WriteLine("Average Confidence per Preset:");
        foreach (var kvp in confidenceMap)
        {
            string preset = kvp.Key;
            List<int> values = kvp.Value;
            double average = values.Count > 0 ? (double)values.Sum() / values.Count : 0.0;
            Console.WriteLine($"{preset}: {average:F2}");
        }

        // Remove the temporary folder and its contents.
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors.
        }
    }
}