using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "InputImages");
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        // Seed sample barcode images if none exist
        string[] existingFiles = Directory.GetFiles(inputFolder, "*.png");
        if (existingFiles.Length == 0)
        {
            string[] sampleTexts = new[]
            {
                "(02)04006664241007(37)1(400)7019590754",
                "(01)12345678901231(10)ABC123",
                "(01)98765432109876(21)XYZ789"
            };

            for (int i = 0; i < sampleTexts.Length; i++)
            {
                string filePath = Path.Combine(inputFolder, $"Sample{i + 1}.png");
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1Code128, sampleTexts[i]))
                {
                    generator.Save(filePath);
                }
            }
        }

        // Process up to 5 images
        string[] imageFiles = Directory.GetFiles(inputFolder, "*.png");
        int maxFiles = Math.Min(imageFiles.Length, 5);
        for (int i = 0; i < maxFiles; i++)
        {
            string file = imageFiles[i];
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            using (BarCodeReader reader = new BarCodeReader(file, DecodeType.Code128))
            {
                // Keep FNC characters in the result
                reader.BarcodeSettings.StripFNC = false;

                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(file)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                }
            }
        }
    }
}