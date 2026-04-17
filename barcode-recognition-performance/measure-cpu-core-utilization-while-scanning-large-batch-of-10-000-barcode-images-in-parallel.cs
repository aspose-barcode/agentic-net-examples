using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        const int sampleCount = 5; // safe sample size
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        // Generate sample barcode images if they do not exist
        for (int i = 1; i <= sampleCount; i++)
        {
            string filePath = Path.Combine(inputFolder, $"Sample{i}.png");
            if (!File.Exists(filePath))
            {
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
                {
                    generator.CodeText = $"Sample{i}";
                    generator.Save(filePath);
                }
            }
        }

        string[] files = Directory.GetFiles(inputFolder, "*.png");
        if (files.Length == 0)
        {
            Console.WriteLine("No barcode images found.");
            return;
        }

        // Configure processor settings for maximum parallelism
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;

        var process = Process.GetCurrentProcess();
        TimeSpan cpuStart = process.TotalProcessorTime;
        var sw = Stopwatch.StartNew();

        // Parallel barcode reading
        ParallelOptions options = new ParallelOptions
        {
            MaxDegreeOfParallelism = Environment.ProcessorCount
        };

        Parallel.ForEach(files, options, file =>
        {
            using (var reader = new BarCodeReader(file))
            {
                // Restrict to Code128 for this example
                reader.BarCodeReadType = DecodeType.Code128;
                foreach (var result in reader.ReadBarCodes())
                {
                    // Process result (here we just access properties to avoid optimization removal)
                    string _ = result.CodeText;
                }
            }
        });

        sw.Stop();
        TimeSpan cpuEnd = process.TotalProcessorTime;

        double elapsedSeconds = sw.Elapsed.TotalSeconds;
        double cpuSeconds = (cpuEnd - cpuStart).TotalSeconds;
        double utilization = (cpuSeconds / (elapsedSeconds * Environment.ProcessorCount)) * 100.0;

        Console.WriteLine($"Processed {files.Length} images in {elapsedSeconds:F2} seconds.");
        Console.WriteLine($"CPU time used: {cpuSeconds:F2} seconds.");
        Console.WriteLine($"Average CPU utilization per core: {utilization:F2}%.");
    }
}