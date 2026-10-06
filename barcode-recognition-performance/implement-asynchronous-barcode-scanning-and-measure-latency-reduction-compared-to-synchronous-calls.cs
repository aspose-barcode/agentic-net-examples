using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeAsyncDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Generate sample barcodes
        int sampleCount = 5;
        string[] files = new string[sampleCount];
        for (int i = 0; i < sampleCount; i++)
        {
            string codeText = "CODE" + (i + 1).ToString("D3");
            string filePath = Path.Combine(tempDir, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            files[i] = filePath;
        }

        // Synchronous reading benchmark
        long syncTicks = 0;
        foreach (string file in files)
        {
            var sw = Stopwatch.StartNew();
            string result = ReadSync(file);
            sw.Stop();
            syncTicks += sw.ElapsedTicks;
            Console.WriteLine($"Sync read: {Path.GetFileName(file)} => {result}");
        }

        // Asynchronous reading benchmark
        long asyncTicks = 0;
        foreach (string file in files)
        {
            var sw = Stopwatch.StartNew();
            string result = ReadAsync(file).GetAwaiter().GetResult();
            sw.Stop();
            asyncTicks += sw.ElapsedTicks;
            Console.WriteLine($"Async read: {Path.GetFileName(file)} => {result}");
        }

        double syncMs = syncTicks * 1000.0 / Stopwatch.Frequency;
        double asyncMs = asyncTicks * 1000.0 / Stopwatch.Frequency;
        Console.WriteLine($"Average sync time: {syncMs / sampleCount:F3} ms");
        Console.WriteLine($"Average async time: {asyncMs / sampleCount:F3} ms");
        Console.WriteLine($"Latency reduction: {(syncMs - asyncMs) / syncMs * 100:F2}%");

        // Cleanup
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // ignore cleanup errors
        }
    }

    static string ReadSync(string imagePath)
    {
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            reader.ReadBarCodes();
            foreach (BarCodeResult result in reader.FoundBarCodes)
            {
                return result.CodeText;
            }
        }
        return null;
    }

    static async Task<string> ReadAsync(string imagePath)
    {
        return await Task.Run(() => ReadSync(imagePath));
    }
}