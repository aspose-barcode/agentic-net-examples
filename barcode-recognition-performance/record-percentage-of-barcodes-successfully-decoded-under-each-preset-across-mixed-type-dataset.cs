using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    // Holds information about each generated barcode
    class BarcodeInfo
    {
        public string FilePath { get; set; }
        public BaseEncodeType EncodeType { get; set; }
        public string CodeText { get; set; }
    }

    static void Main()
    {
        // Prepare a temporary folder for barcode images
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define a small mixed‑type dataset
        var barcodeDefinitions = new List<(BaseEncodeType type, string text)>
        {
            (EncodeTypes.Code128, "ABC123"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.DataMatrix, "DM12345"),
            (EncodeTypes.Pdf417, "PDF417_SAMPLE"),
            (EncodeTypes.EAN13, "123456789012")
        };

        // Generate barcode images and collect their info
        var generatedBarcodes = new List<BarcodeInfo>();
        int index = 0;
        foreach (var (type, text) in barcodeDefinitions)
        {
            string fileName = $"barcode_{index}_{type}.png";
            string filePath = Path.Combine(outputDir, fileName);

            using (var generator = new BarcodeGenerator(type, text))
            {
                // Simple generation with default parameters
                generator.Save(filePath);
            }

            generatedBarcodes.Add(new BarcodeInfo
            {
                FilePath = filePath,
                EncodeType = type,
                CodeText = text
            });

            index++;
        }

        // Define the recognition presets to evaluate
        var presets = new Dictionary<string, QualitySettings>
        {
            { "HighPerformance", QualitySettings.HighPerformance },
            { "NormalQuality", QualitySettings.NormalQuality },
            { "HighQuality", QualitySettings.HighQuality },
            { "MaxQuality", QualitySettings.MaxQuality }
        };

        // Evaluate each preset
        foreach (var presetEntry in presets)
        {
            string presetName = presetEntry.Key;
            QualitySettings preset = presetEntry.Value;

            int successCount = 0;
            int totalCount = generatedBarcodes.Count;

            foreach (var info in generatedBarcodes)
            {
                if (!File.Exists(info.FilePath))
                {
                    // Skip missing files gracefully
                    continue;
                }

                using (var reader = new BarCodeReader(info.FilePath))
                {
                    // Apply the current quality preset
                    reader.QualitySettings = preset;

                    // Perform recognition
                    var results = reader.ReadBarCodes();

                    bool matched = false;
                    foreach (var result in results)
                    {
                        if (result.CodeText == info.CodeText)
                        {
                            matched = true;
                            break;
                        }
                    }

                    if (matched)
                    {
                        successCount++;
                    }
                }
            }

            float percentage = totalCount > 0 ? (successCount * 100f) / totalCount : 0f;
            Console.WriteLine($"{presetName}: {percentage:F2}% ({successCount}/{totalCount}) barcodes decoded successfully.");
        }

        // Cleanup generated files (optional)
        // foreach (var info in generatedBarcodes)
        // {
        //     if (File.Exists(info.FilePath))
        //         File.Delete(info.FilePath);
        // }
    }
}