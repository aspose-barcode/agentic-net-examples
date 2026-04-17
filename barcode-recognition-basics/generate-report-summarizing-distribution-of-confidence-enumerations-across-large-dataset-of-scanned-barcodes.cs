using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Directory to store temporary barcode images
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Prepare a list of barcode specifications (type, text, file name)
        var barcodeSpecs = new List<(BaseEncodeType type, string text, string fileName)>
        {
            (EncodeTypes.Code128, "12345", "code128_moderate.png"), // 1D -> Moderate confidence
            (EncodeTypes.Code39, "ABCDE", "code39_moderate.png"),   // 1D -> Moderate confidence
            (EncodeTypes.QR, "Hello World", "qr_strong.png"),     // 2D -> Strong confidence
            (EncodeTypes.DataMatrix, "DataMatrix", "datamatrix_strong.png"), // 2D -> Strong confidence
            (EncodeTypes.EAN13, "1234567890128", "ean13_strong.png") // 2D (actually 1D but with checksum) -> Strong confidence
        };

        // Generate barcode images
        foreach (var spec in barcodeSpecs)
        {
            string filePath = Path.Combine(outputDir, spec.fileName);
            using (var generator = new BarcodeGenerator(spec.type, spec.text))
            {
                // Use default settings; ensure image is saved in PNG format
                generator.Save(filePath);
            }
        }

        // Counters for each confidence level
        var confidenceCounts = new Dictionary<BarCodeConfidence, int>
        {
            { BarCodeConfidence.None, 0 },
            { BarCodeConfidence.Moderate, 0 },
            { BarCodeConfidence.Strong, 0 }
        };

        // Recognize each generated barcode and aggregate confidence values
        foreach (var spec in barcodeSpecs)
        {
            string filePath = Path.Combine(outputDir, spec.fileName);
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                // Read all barcodes in the image
                BarCodeResult[] results = reader.ReadBarCodes();
                foreach (BarCodeResult result in results)
                {
                    // Increment the appropriate confidence counter
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

        // Output the distribution summary
        Console.WriteLine("BarCode Confidence Distribution:");
        foreach (var kvp in confidenceCounts)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
    }
}