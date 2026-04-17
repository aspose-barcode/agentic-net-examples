using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Prepare temporary folder for barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeBenchmark");
        if (!Directory.Exists(tempFolder))
        {
            Directory.CreateDirectory(tempFolder);
        }

        // Sample barcode texts with intentional wrong checksum (EAN13 expects 13 digits)
        string[] codeTexts = new[]
        {
            "1234567890123",
            "1234567890124",
            "1234567890125",
            "1234567890126",
            "1234567890127"
        };

        // Generate barcode images
        string[] imagePaths = new string[codeTexts.Length];
        for (int i = 0; i < codeTexts.Length; i++)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, codeTexts[i]))
            {
                generator.Save(filePath);
            }
            imagePaths[i] = filePath;
        }

        // Benchmark with AllowIncorrectBarcodes = false
        var swFalse = Stopwatch.StartNew();
        foreach (string path in imagePaths)
        {
            if (!File.Exists(path))
                continue;

            using (var reader = new BarCodeReader(path, DecodeType.EAN13))
            {
                // Ensure the setting is explicitly disabled
                reader.QualitySettings.AllowIncorrectBarcodes = false;
                foreach (var result in reader.ReadBarCodes())
                {
                    // No output needed; just force enumeration
                }
            }
        }
        swFalse.Stop();

        // Benchmark with AllowIncorrectBarcodes = true
        var swTrue = Stopwatch.StartNew();
        foreach (string path in imagePaths)
        {
            if (!File.Exists(path))
                continue;

            using (var reader = new BarCodeReader(path, DecodeType.EAN13))
            {
                // Enable recognition of incorrect barcodes
                reader.QualitySettings.AllowIncorrectBarcodes = true;
                foreach (var result in reader.ReadBarCodes())
                {
                    // No output needed; just force enumeration
                }
            }
        }
        swTrue.Stop();

        // Display results
        Console.WriteLine($"Recognition time with AllowIncorrectBarcodes = false: {swFalse.ElapsedMilliseconds} ms");
        Console.WriteLine($"Recognition time with AllowIncorrectBarcodes = true : {swTrue.ElapsedMilliseconds} ms");

        // Cleanup temporary files
        foreach (string path in imagePaths)
        {
            if (File.Exists(path))
                File.Delete(path);
        }
        if (Directory.Exists(tempFolder))
            Directory.Delete(tempFolder, true);
    }
}