using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Prepare output folder
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Sample texts to encode
        List<string> originalTexts = new List<string>
        {
            "ABC123",
            "DEF456",
            "GHI789",
            "JKL012",
            "MNO345"
        };

        // Generate barcode images
        for (int i = 0; i < originalTexts.Count; i++)
        {
            string filePath = Path.Combine(outputFolder, $"barcode{i}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, originalTexts[i]))
            {
                generator.Save(filePath);
            }
        }

        // Read barcodes with different quality presets
        List<string> readTexts = new List<string>();
        for (int i = 0; i < originalTexts.Count; i++)
        {
            string filePath = Path.Combine(outputFolder, $"barcode{i}.png");
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.Code128))
            {
                // Switch quality preset mid‑batch
                if (i < 2)
                {
                    reader.QualitySettings = QualitySettings.NormalQuality;
                }
                else
                {
                    reader.QualitySettings = QualitySettings.HighPerformance;
                }

                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    readTexts.Add(result.CodeText);
                }
            }
        }

        // Verify that read results match the original texts
        bool allMatch = true;
        for (int i = 0; i < originalTexts.Count; i++)
        {
            string expected = originalTexts[i];
            string actual = i < readTexts.Count ? readTexts[i] : "(missing)";
            if (!string.Equals(expected, actual, StringComparison.Ordinal))
            {
                allMatch = false;
                Console.WriteLine($"Mismatch at index {i}: expected '{expected}', got '{actual}'");
            }
        }

        if (allMatch)
        {
            Console.WriteLine("All barcodes read correctly after switching presets.");
        }
        else
        {
            Console.WriteLine("Some barcodes were not read correctly.");
        }
    }
}