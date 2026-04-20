using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Common;

class Program
{
    static void Main(string[] args)
    {
        // Sample image paths – replace with real paths or pass via command line
        List<string> imagePaths = new List<string>
        {
            @"sample1.png",
            @"sample2.png",
            @"sample3.png",
            @"sample4.png",
            @"sample5.png"
        };

        // If arguments are provided, use them as image paths
        if (args.Length > 0)
        {
            imagePaths.Clear();
            imagePaths.AddRange(args);
        }

        // Configure processor settings for barcode recognition
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount;
        BarCodeReader.ProcessorSettings.UseAllCores = false;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Math.Max(1, Environment.ProcessorCount / 2);

        // Parallel processing options – limit degree of parallelism to available cores
        ParallelOptions parallelOptions = new ParallelOptions
        {
            MaxDegreeOfParallelism = Environment.ProcessorCount
        };

        // Process each image in parallel
        Parallel.ForEach(imagePaths, parallelOptions, path =>
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"File not found: {path}");
                return;
            }

            try
            {
                using (BarCodeReader reader = new BarCodeReader(path))
                {
                    // Optional: set a timeout to avoid hanging on large images
                    reader.Timeout = 5000; // milliseconds

                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"{Path.GetFileName(path)}: {result.CodeText} ({result.CodeTypeName})");
                    }

                    if (reader.FoundCount == 0)
                    {
                        Console.WriteLine($"{Path.GetFileName(path)}: No barcodes detected.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing {path}: {ex.Message}");
            }
        });
    }
}