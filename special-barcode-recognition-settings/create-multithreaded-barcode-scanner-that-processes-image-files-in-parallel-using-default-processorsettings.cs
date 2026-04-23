using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Determine folder containing images (first argument or default)
        string imagesFolder = args.Length > 0 ? args[0] : "SampleImages";

        if (!Directory.Exists(imagesFolder))
        {
            Console.WriteLine($"Folder not found: {imagesFolder}");
            return;
        }

        // Get up to 10 image files of common formats
        var imageFiles = Directory.GetFiles(imagesFolder)
            .Where(f => f.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".tif", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".tiff", StringComparison.OrdinalIgnoreCase))
            .Take(10)
            .ToList();

        if (imageFiles.Count == 0)
        {
            Console.WriteLine("No image files found to process.");
            return;
        }

        // Enable multithreaded processing using all cores
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        // Optionally limit additional threads (example)
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;

        // Process images in parallel
        ParallelOptions parallelOptions = new ParallelOptions
        {
            MaxDegreeOfParallelism = Environment.ProcessorCount
        };

        Parallel.ForEach(imageFiles, parallelOptions, filePath =>
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                return;
            }

            using (var reader = new BarCodeReader(filePath))
            {
                // Read all barcodes in the image
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"{Path.GetFileName(filePath)} - Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
        });

        Console.WriteLine("Barcode scanning completed.");
    }
}