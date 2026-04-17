using System;
using System.IO;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Common;

class Program
{
    static void Main()
    {
        // Directory to store sample barcode images
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodesSample");
        if (!Directory.Exists(tempDir))
        {
            Directory.CreateDirectory(tempDir);
        }

        // Prepare a list of sample image file paths
        var imagePaths = new string[5];
        for (int i = 0; i < imagePaths.Length; i++)
        {
            string filePath = Path.Combine(tempDir, $"barcode_{i + 1}.png");
            imagePaths[i] = filePath;

            // Generate a barcode image if it does not exist
            if (!File.Exists(filePath))
            {
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"CODE{i + 1}"))
                {
                    generator.Save(filePath);
                }
            }
        }

        // Enable multi‑core processing for barcode reading
        BarCodeReader.ProcessorSettings.UseAllCores = true;

        // Thread‑safe collection to aggregate results
        var aggregatedResults = new ConcurrentBag<string>();

        // Read barcodes from all images in parallel
        Parallel.ForEach(imagePaths, imagePath =>
        {
            if (!File.Exists(imagePath))
            {
                // Skip missing files gracefully
                return;
            }

            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    string entry = $"{Path.GetFileName(imagePath)}: {result.CodeTypeName} - {result.CodeText}";
                    aggregatedResults.Add(entry);
                }
            }
        });

        // Output aggregated results
        Console.WriteLine("Aggregated barcode reading results:");
        foreach (var line in aggregatedResults)
        {
            Console.WriteLine(line);
        }
    }
}