using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

namespace BarcodeProcessingApp
{
    class Program
    {
        static void Main()
        {
            // Define working directory
            string workingDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
            Directory.CreateDirectory(workingDir);

            // Sample barcode texts
            string[] sampleTexts = { "ABC123", "987XYZ", "Test456" };
            string[] filePaths = new string[sampleTexts.Length];

            // Generate barcode images
            for (int i = 0; i < sampleTexts.Length; i++)
            {
                string fileName = $"barcode_{i + 1}.png";
                string fullPath = Path.Combine(workingDir, fileName);
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
                {
                    generator.CodeText = sampleTexts[i];
                    generator.Save(fullPath);
                }
                filePaths[i] = fullPath;
            }

            // Read each barcode and display results
            foreach (string imagePath in filePaths)
            {
                using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
                {
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"File: {Path.GetFileName(imagePath)}, Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }
                }
            }
        }
    }
}