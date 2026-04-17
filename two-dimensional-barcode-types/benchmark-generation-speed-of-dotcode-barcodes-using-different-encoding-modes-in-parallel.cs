using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main(string[] args)
    {
        // Define the encoding modes to benchmark
        var modes = new List<DotCodeEncodeMode>
        {
            DotCodeEncodeMode.Auto,
            DotCodeEncodeMode.Binary,
            DotCodeEncodeMode.ECI,
            DotCodeEncodeMode.Extended
        };

        // Sample text to encode
        const string sampleText = "Sample123";

        // Number of barcodes generated per mode (kept small for quick execution)
        const int countPerMode = 5;

        // Store elapsed time per mode
        var results = new ConcurrentDictionary<DotCodeEncodeMode, long>();

        // Run benchmarks in parallel for each mode
        Parallel.ForEach(modes, mode =>
        {
            var stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < countPerMode; i++)
            {
                string fileName = $"DotCode_{mode}_{i}.png";

                switch (mode)
                {
                    case DotCodeEncodeMode.Auto:
                        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, sampleText))
                        {
                            generator.Parameters.Barcode.DotCode.DotCodeEncodeMode = DotCodeEncodeMode.Auto;
                            generator.Save(fileName, BarCodeImageFormat.Png);
                        }
                        break;

                    case DotCodeEncodeMode.Binary:
                        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode))
                        {
                            generator.Parameters.Barcode.DotCode.DotCodeEncodeMode = DotCodeEncodeMode.Binary;
                            // Binary mode works with raw bytes
                            byte[] bytes = Encoding.UTF8.GetBytes(sampleText);
                            generator.SetCodeText(bytes);
                            generator.Save(fileName, BarCodeImageFormat.Png);
                        }
                        break;

                    case DotCodeEncodeMode.ECI:
                        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, sampleText))
                        {
                            generator.Parameters.Barcode.DotCode.DotCodeEncodeMode = DotCodeEncodeMode.ECI;
                            generator.Parameters.Barcode.DotCode.ECIEncoding = ECIEncodings.UTF8;
                            generator.Save(fileName, BarCodeImageFormat.Png);
                        }
                        break;

                    case DotCodeEncodeMode.Extended:
                        // Build extended codetext using the helper builder
                        var builder = new DotCodeExtCodetextBuilder();
                        builder.AddFNC1FormatIdentifier();
                        builder.AddECICodetext(ECIEncodings.UTF8, sampleText);
                        string extendedText = builder.GetExtendedCodetext();

                        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, extendedText))
                        {
                            generator.Parameters.Barcode.DotCode.DotCodeEncodeMode = DotCodeEncodeMode.Extended;
                            generator.Save(fileName, BarCodeImageFormat.Png);
                        }
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(mode), $"Unsupported mode: {mode}");
                }
            }

            stopwatch.Stop();
            results[mode] = stopwatch.ElapsedMilliseconds;
        });

        // Output benchmark results
        foreach (var kvp in results)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value} ms for {countPerMode} generations");
        }
    }
}