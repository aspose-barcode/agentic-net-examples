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
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "InputTiff");
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        // Create sample TIFF files if none exist
        string[] tiffFiles = Directory.GetFiles(inputFolder, "*.tif");
        if (tiffFiles.Length == 0)
        {
            for (int i = 1; i <= 3; i++)
            {
                string samplePath = Path.Combine(inputFolder, $"Sample{i}.tif");
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, $"591234567{i}"))
                {
                    // Use NTable interpreting type for customer information
                    generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.NTable;
                    generator.Save(samplePath, BarCodeImageFormat.Tiff);
                }
            }
            tiffFiles = Directory.GetFiles(inputFolder, "*.tif");
        }

        // Process each TIFF image
        foreach (string filePath in tiffFiles)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.AustraliaPost))
            {
                // Apply NTable interpreting type for recognition
                reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.NTable;

                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                    Console.WriteLine($"  Barcode Type: {result.CodeType}");
                    Console.WriteLine($"  Code Text: {result.CodeText}");
                }
            }
        }
    }
}