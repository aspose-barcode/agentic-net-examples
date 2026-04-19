using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        const int sampleCount = 5;
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        Directory.CreateDirectory(outputDir);

        // Generate single linear barcodes (Code128)
        string[] singleFiles = new string[sampleCount];
        for (int i = 0; i < sampleCount; i++)
        {
            string codeText = $"CODE128-{i:D4}";
            string filePath = Path.Combine(outputDir, $"single_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;
                generator.Parameters.Barcode.BarHeight.Pixels = 100f;
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Save(filePath);
            }
            singleFiles[i] = filePath;
        }

        // Generate stacked linear barcodes (GS1 DataBar Stacked)
        string[] stackedFiles = new string[sampleCount];
        for (int i = 0; i < sampleCount; i++)
        {
            string codeText = $"1234567{i:D2}";
            string filePath = Path.Combine(outputDir, $"stacked_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.DatabarStacked, codeText))
            {
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;
                generator.Parameters.Barcode.BarHeight.Pixels = 100f;
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Save(filePath);
            }
            stackedFiles[i] = filePath;
        }

        // Benchmark recognition for single barcodes
        double singleTotalMs = 0;
        foreach (string file in singleFiles)
        {
            if (!File.Exists(file))
                continue;

            using (var reader = new BarCodeReader(file, DecodeType.Code128))
            {
                var sw = Stopwatch.StartNew();
                var results = reader.ReadBarCodes();
                sw.Stop();
                singleTotalMs += sw.Elapsed.TotalMilliseconds;

                foreach (var result in results)
                {
                    Console.WriteLine($"Single - Type: {result.CodeTypeName}, Text: {result.CodeText}, Confidence: {result.Confidence}");
                }
            }
        }

        // Benchmark recognition for stacked barcodes
        double stackedTotalMs = 0;
        foreach (string file in stackedFiles)
        {
            if (!File.Exists(file))
                continue;

            using (var reader = new BarCodeReader(file, DecodeType.DatabarStacked))
            {
                var sw = Stopwatch.StartNew();
                var results = reader.ReadBarCodes();
                sw.Stop();
                stackedTotalMs += sw.Elapsed.TotalMilliseconds;

                foreach (var result in results)
                {
                    Console.WriteLine($"Stacked - Type: {result.CodeTypeName}, Text: {result.CodeText}, Confidence: {result.Confidence}");
                }
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Average recognition time (single): {singleTotalMs / sampleCount:F2} ms");
        Console.WriteLine($"Average recognition time (stacked): {stackedTotalMs / sampleCount:F2} ms");
    }
}