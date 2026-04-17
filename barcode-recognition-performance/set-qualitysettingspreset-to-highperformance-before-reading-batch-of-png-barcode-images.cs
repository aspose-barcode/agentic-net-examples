using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        var samples = new (BaseEncodeType type, string text, string file)[]
        {
            (EncodeTypes.Code128, "Sample123", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.EAN13, "1234567890128", "ean13.png")
        };

        foreach (var sample in samples)
        {
            string filePath = Path.Combine(inputFolder, sample.file);
            if (!File.Exists(filePath))
            {
                using (var generator = new BarcodeGenerator(sample.type, sample.text))
                {
                    generator.Save(filePath);
                }
            }
        }

        string[] pngFiles = Directory.GetFiles(inputFolder, "*.png");
        if (pngFiles.Length == 0)
        {
            Console.WriteLine("No PNG barcode images found in the folder.");
            return;
        }

        foreach (string file in pngFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            using (var reader = new BarCodeReader(file))
            {
                reader.QualitySettings = QualitySettings.HighPerformance;
                BarCodeResult[] results = reader.ReadBarCodes();

                Console.WriteLine($"Processing file: {Path.GetFileName(file)}");
                if (results.Length == 0)
                {
                    Console.WriteLine("  No barcodes detected.");
                }
                else
                {
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }
                }
            }
        }
    }
}