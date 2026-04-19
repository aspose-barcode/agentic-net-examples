using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const int sampleCount = 5;
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "QrOutput");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        Console.WriteLine($"Generating {sampleCount} QR codes in \"{outputFolder}\"");
        for (int i = 1; i <= sampleCount; i++)
        {
            string codeText = $"Sample QR {i}";
            string filePath = Path.Combine(outputFolder, $"qr_{i}.png");

            long memoryBefore = GC.GetTotalMemory(true);
            var stopwatch = Stopwatch.StartNew();

            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                generator.CodeText = codeText;
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    bitmap.Save(filePath, ImageFormat.Png);
                }
            }

            stopwatch.Stop();
            long memoryAfter = GC.GetTotalMemory(true);
            long memoryUsed = memoryAfter - memoryBefore;

            Console.WriteLine($"QR {i}: Saved to {filePath}");
            Console.WriteLine($"  Time elapsed: {stopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine($"  Approx. memory used: {memoryUsed / 1024} KB");
        }

        Console.WriteLine("Batch generation completed.");
    }
}