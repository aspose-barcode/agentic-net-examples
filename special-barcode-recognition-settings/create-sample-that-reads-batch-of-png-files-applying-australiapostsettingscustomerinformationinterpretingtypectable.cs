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
        // Folder that contains PNG barcode images
        string inputFolder = Path.Combine(Environment.CurrentDirectory, "InputBarcodes");
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        // If the folder is empty, generate a few sample Australia Post barcodes with CTable interpreting type
        string[] existingPngs = Directory.GetFiles(inputFolder, "*.png");
        if (existingPngs.Length == 0)
        {
            string[] sampleTexts = { "5912345678ABCde", "5912345678XYZ", "5912345678#123" };
            for (int i = 0; i < sampleTexts.Length; i++)
            {
                string filePath = Path.Combine(inputFolder, $"Sample{i + 1}.png");
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, sampleTexts[i]))
                {
                    // Apply CTable interpreting type
                    generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;
                    // Save as PNG
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }
            }
        }

        // Process each PNG file in the folder
        string[] pngFiles = Directory.GetFiles(inputFolder, "*.png");
        foreach (string pngFile in pngFiles)
        {
            if (!File.Exists(pngFile))
            {
                Console.WriteLine($"File not found: {pngFile}");
                continue;
            }

            using (BarCodeReader reader = new BarCodeReader(pngFile, DecodeType.AustraliaPost))
            {
                // Set interpreting type for decoding
                reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(pngFile)}");
                    Console.WriteLine($"  BarCode Type: {result.CodeType}");
                    Console.WriteLine($"  BarCode CodeText: {result.CodeText}");
                }
            }
        }
    }
}