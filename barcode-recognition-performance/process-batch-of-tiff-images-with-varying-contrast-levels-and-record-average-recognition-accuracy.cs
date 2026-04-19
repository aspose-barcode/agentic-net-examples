using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Folder containing TIFF images
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "InputTiffs");
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        // Ensure there are sample TIFF files to process
        string[] tiffFiles = Directory.GetFiles(inputFolder, "*.tif");
        if (tiffFiles.Length == 0)
        {
            for (int i = 1; i <= 5; i++)
            {
                string filePath = Path.Combine(inputFolder, $"sample{i}.tif");
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i}"))
                {
                    generator.Save(filePath, BarCodeImageFormat.Tiff);
                }
            }
            tiffFiles = Directory.GetFiles(inputFolder, "*.tif");
        }

        long totalConfidence = 0;
        int barcodeCount = 0;

        foreach (string file in tiffFiles)
        {
            // Use HighQuality preset to improve recognition on varied contrast images
            using (BarCodeReader reader = new BarCodeReader(file, DecodeType.Code128))
            {
                reader.QualitySettings = QualitySettings.HighQuality;
                BarCodeResult[] results = reader.ReadBarCodes();

                foreach (BarCodeResult result in results)
                {
                    // Confidence enum values can be cast to int (None=0, Moderate=80, Strong=100)
                    totalConfidence += (int)result.Confidence;
                    barcodeCount++;
                }
            }
        }

        double averageConfidence = barcodeCount > 0 ? (double)totalConfidence / barcodeCount : 0.0;
        Console.WriteLine($"Processed {barcodeCount} barcodes across {tiffFiles.Length} TIFF images.");
        Console.WriteLine($"Average recognition confidence: {averageConfidence:F2}");
    }
}