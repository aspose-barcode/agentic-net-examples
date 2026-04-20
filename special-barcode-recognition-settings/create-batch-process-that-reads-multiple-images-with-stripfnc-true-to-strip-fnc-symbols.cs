using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Define input folder for images
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "InputImages");
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        // Seed sample images if folder is empty (up to 3 samples)
        string[] existingFiles = Directory.GetFiles(inputFolder, "*.png");
        if (existingFiles.Length == 0)
        {
            for (int i = 1; i <= 3; i++)
            {
                string samplePath = Path.Combine(inputFolder, $"Sample{i}.png");
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1Code128, "(02)04006664241007(37)1(400)7019590754"))
                {
                    generator.Save(samplePath);
                }
            }
        }

        // Get all image files (png, jpg, jpeg, bmp) in the folder
        string[] imageFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in imageFiles)
        {
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            if (extension != ".png" && extension != ".jpg" && extension != ".jpeg" && extension != ".bmp")
                continue; // skip non‑image files

            if (!File.Exists(filePath))
                continue; // safety check

            // Read barcodes from the image with StripFNC enabled
            using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.Code128))
            {
                // Enable stripping of FNC characters
                reader.BarcodeSettings.StripFNC = true;

                BarCodeResult[] results = reader.ReadBarCodes();
                if (results.Length == 0)
                {
                    Console.WriteLine($"No barcodes found in file: {Path.GetFileName(filePath)}");
                }
                else
                {
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"File: {Path.GetFileName(filePath)} | Type: {result.CodeTypeName} | CodeText: {result.CodeText}");
                    }
                }
            }
        }
    }
}