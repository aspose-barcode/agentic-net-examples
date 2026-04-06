using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Folder to store generated barcode images
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(folderPath);

        // Number of barcodes per type
        const int countPerType = 10;

        // Generate Code128 barcodes (moderate confidence)
        for (int i = 0; i < countPerType; i++)
        {
            string fileName = Path.Combine(folderPath, $"code128_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"CODE128_{i}"))
            {
                generator.Save(fileName);
            }
        }

        // Generate QR barcodes (strong confidence)
        for (int i = 0; i < countPerType; i++)
        {
            string fileName = Path.Combine(folderPath, $"qr_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, $"QR_{i}"))
            {
                generator.Save(fileName);
            }
        }

        // Prepare dictionary to hold confidence distribution
        var confidenceCounts = new Dictionary<BarCodeConfidence, int>
        {
            { BarCodeConfidence.None, 0 },
            { BarCodeConfidence.Moderate, 0 },
            { BarCodeConfidence.Strong, 0 }
        };

        // Process each generated image
        foreach (string file in Directory.GetFiles(folderPath, "*.png"))
        {
            using (var reader = new BarCodeReader(file, DecodeType.Code128, DecodeType.QR))
            {
                // Read all barcodes in the image
                BarCodeResult[] results = reader.ReadBarCodes();

                if (results.Length == 0)
                {
                    // No barcode recognized – count as None
                    confidenceCounts[BarCodeConfidence.None]++;
                }
                else
                {
                    foreach (BarCodeResult result in results)
                    {
                        // Increment the appropriate confidence bucket
                        if (confidenceCounts.ContainsKey(result.Confidence))
                        {
                            confidenceCounts[result.Confidence]++;
                        }
                        else
                        {
                            // In case a new enum value appears in future versions
                            confidenceCounts[result.Confidence] = 1;
                        }
                    }
                }
            }
        }

        // Output the summary
        Console.WriteLine("BarCode Confidence Distribution:");
        foreach (var kvp in confidenceCounts)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
    }
}