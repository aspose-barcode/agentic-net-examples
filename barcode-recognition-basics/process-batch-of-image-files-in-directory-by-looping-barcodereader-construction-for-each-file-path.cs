using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define the folder that will contain barcode images
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");

        // Ensure the folder exists
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // If the folder is empty, generate a few sample barcode images
        string[] existingFiles = Directory.GetFiles(folderPath, "*.png");
        if (existingFiles.Length == 0)
        {
            for (int i = 1; i <= 3; i++)
            {
                string fileName = Path.Combine(folderPath, $"barcode{i}.png");
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
                {
                    generator.CodeText = $"Sample{i}";
                    generator.Save(fileName);
                }
            }
        }

        // Get all PNG files in the folder
        string[] barcodeFiles = Directory.GetFiles(folderPath, "*.png");

        // Process each file with BarCodeReader
        foreach (string filePath in barcodeFiles)
        {
            using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(filePath)} - Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
        }
    }
}