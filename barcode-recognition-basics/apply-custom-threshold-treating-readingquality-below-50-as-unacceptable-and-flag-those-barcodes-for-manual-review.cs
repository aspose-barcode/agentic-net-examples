using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Directory for temporary barcode images
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Sample data to encode
        string[] codes = { "12345", "ABCDE", "987654321" };
        string[] filePaths = new string[codes.Length];

        // Generate barcode images
        for (int i = 0; i < codes.Length; i++)
        {
            string filePath = Path.Combine(outputDir, $"barcode_{i}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codes[i]))
            {
                generator.Save(filePath);
            }
            filePaths[i] = filePath;
        }

        // Recognize and evaluate reading quality
        for (int i = 0; i < filePaths.Length; i++)
        {
            string filePath = filePaths[i];
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.Code128))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    double quality = result.ReadingQuality;
                    Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                    Console.WriteLine($"  Type: {result.CodeTypeName}");
                    Console.WriteLine($"  CodeText: {result.CodeText}");
                    Console.WriteLine($"  ReadingQuality: {quality}");

                    if (quality < 50.0)
                    {
                        Console.WriteLine("  --> Flagged for manual review (quality below threshold)");
                    }
                }
            }
        }

        // Cleanup temporary files (optional)
        // foreach (var path in filePaths) { File.Delete(path); }
    }
}