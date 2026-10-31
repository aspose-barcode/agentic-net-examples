using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        const int barcodeCount = 10; // safe sample size for the runner
        var stopwatch = Stopwatch.StartNew();

        Parallel.For(0, barcodeCount, i =>
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, $"Data{i}"))
            {
                // Example configuration
                generator.Parameters.Barcode.XDimension.Pixels = 4;

                using (var memoryStream = new MemoryStream())
                {
                    generator.Save(memoryStream, BarCodeImageFormat.Png);
                    // The generated image is in memory; no further processing needed for the benchmark
                }
            }
        });

        stopwatch.Stop();
        Console.WriteLine($"Generated {barcodeCount} DataMatrix barcodes in {stopwatch.ElapsedMilliseconds} ms.");
    }
}