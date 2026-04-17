using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Sample size (small for safe execution)
        const int barcodeCount = 10;

        // Start timing
        var stopwatch = Stopwatch.StartNew();

        // Generate barcodes in parallel
        Parallel.For(0, barcodeCount, i =>
        {
            // Each iteration creates its own generator instance
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, $"CODE{i:D5}"))
            {
                // Generate the barcode image
                using (var bitmap = generator.GenerateBarCodeImage())
                {
                    // Force image encoding by saving to a memory stream (no file I/O)
                    using (var ms = new MemoryStream())
                    {
                        bitmap.Save(ms, ImageFormat.Png);
                    }
                }
            }
        });

        // Stop timing
        stopwatch.Stop();

        Console.WriteLine($"Generated {barcodeCount} DataMatrix barcodes in {stopwatch.ElapsedMilliseconds} ms.");
    }
}