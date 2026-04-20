using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Prepare temporary directory for barcode images
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeBenchmark");
        if (!Directory.Exists(tempDir))
        {
            Directory.CreateDirectory(tempDir);
        }

        // Generate sample barcode images
        List<string> imageFiles = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            string filePath = Path.Combine(tempDir, $"barcode_{i}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i}"))
            {
                generator.Save(filePath);
            }
            imageFiles.Add(filePath);
        }

        // Number of repetitions for each benchmark
        int repetitions = 10;

        // Benchmark with UseAllCores = true
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        long elapsedAllCores = RunBenchmark(imageFiles, repetitions);

        // Benchmark with UseAllCores = false (use half of the cores)
        BarCodeReader.ProcessorSettings.UseAllCores = false;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Math.Max(1, Environment.ProcessorCount / 2);
        long elapsedPartialCores = RunBenchmark(imageFiles, repetitions);

        // Output results
        Console.WriteLine($"Decoding with UseAllCores = true  : {elapsedAllCores} ms");
        Console.WriteLine($"Decoding with UseAllCores = false : {elapsedPartialCores} ms");

        // Clean up temporary files
        foreach (string file in imageFiles)
        {
            try
            {
                File.Delete(file);
            }
            catch
            {
                // Ignore any deletion errors
            }
        }

        try
        {
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore any deletion errors
        }
    }

    static long RunBenchmark(List<string> files, int repetitions)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        for (int r = 0; r < repetitions; r++)
        {
            foreach (string file in files)
            {
                using (BarCodeReader reader = new BarCodeReader(file, DecodeType.Code128))
                {
                    // Perform decoding; results are not used further
                    BarCodeResult[] results = reader.ReadBarCodes();
                }
            }
        }

        sw.Stop();
        return sw.ElapsedMilliseconds;
    }
}