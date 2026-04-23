using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static async Task Main(string[] args)
    {
        // Prepare a temporary folder for sample barcode images
        string folder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        // Generate a small set of barcode images (5 items)
        var imagePaths = new List<string>();
        for (int i = 1; i <= 5; i++)
        {
            string filePath = Path.Combine(folder, $"barcode{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"CODE{i}"))
            {
                // Example of setting a simple property
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Save(filePath);
            }
            imagePaths.Add(filePath);
        }

        // Asynchronously decode all images using TPL
        var decodeTasks = new List<Task>();
        foreach (string path in imagePaths)
        {
            decodeTasks.Add(Task.Run(() => DecodeBarcodesAsync(path)));
        }

        await Task.WhenAll(decodeTasks);
    }

    // Asynchronous barcode decoding for a single image
    private static async Task DecodeBarcodesAsync(string imagePath)
    {
        // Validate file existence
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Use BarCodeReader with all supported types
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Optionally set a quality preset
            reader.QualitySettings = QualitySettings.HighPerformance;

            // Perform recognition (synchronous call wrapped in Task)
            BarCodeResult[] results = await Task.Run(() => reader.ReadBarCodes());

            // Output results
            if (results.Length == 0)
            {
                Console.WriteLine($"No barcodes detected in {Path.GetFileName(imagePath)}");
            }
            else
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"File: {Path.GetFileName(imagePath)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                }
            }
        }
    }
}