using System;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        const int iterations = 100;
        const string codeText = "CODE39TEST";

        // Benchmark with checksum enabled
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, codeText))
        {
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            var sw = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                using (Bitmap img = generator.GenerateBarCodeImage())
                {
                    // Image generated; no further processing needed.
                }
            }
            sw.Stop();
            Console.WriteLine($"Checksum enabled: {sw.ElapsedMilliseconds} ms for {iterations} generations");
        }

        // Benchmark with checksum disabled
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, codeText))
        {
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;

            var sw = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                using (Bitmap img = generator.GenerateBarCodeImage())
                {
                    // Image generated; no further processing needed.
                }
            }
            sw.Stop();
            Console.WriteLine($"Checksum disabled: {sw.ElapsedMilliseconds} ms for {iterations} generations");
        }
    }
}