using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Prepare output folder
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Sample data for QR codes
        int count = 5;
        string[] data = new string[count];
        for (int i = 0; i < count; i++)
        {
            data[i] = $"SampleQRData_{i}";
        }

        // Measure rendering time with FontMode.Auto
        Stopwatch sw = new Stopwatch();
        sw.Start();
        for (int i = 0; i < count; i++)
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, data[i]))
            {
                generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Auto;
                string filePath = Path.Combine(outputDir, $"qr_auto_{i}.png");
                generator.Save(filePath);
            }
        }
        sw.Stop();
        long autoTimeMs = sw.ElapsedMilliseconds;

        // Measure rendering time with FontMode.Manual and explicit font settings
        sw.Restart();
        for (int i = 0; i < count; i++)
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, data[i]))
            {
                generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Manual;
                generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;
                string filePath = Path.Combine(outputDir, $"qr_manual_{i}.png");
                generator.Save(filePath);
            }
        }
        sw.Stop();
        long manualTimeMs = sw.ElapsedMilliseconds;

        // Output the benchmark results
        Console.WriteLine($"FontMode.Auto rendering time: {autoTimeMs} ms");
        Console.WriteLine($"FontMode.Manual rendering time: {manualTimeMs} ms");
    }
}