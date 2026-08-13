// Title: Measure impact of limited vs multi decode types on barcode recognition speed
// Description: Demonstrates generating sample barcodes and comparing recognition time when using a specific DecodeType versus MultiDecodeType.
// Category-Description: This example belongs to the Aspose.BarCode recognition performance category, illustrating how to use BarcodeGenerator, BarCodeReader, DecodeType, and MultiDecodeType classes. Developers often need to benchmark decoding speed for different symbologies to optimize scanning applications. The snippet shows typical use cases such as generating test images, configuring decoders, and measuring execution time, useful for performance tuning and CI testing.
// Prompt: Measure the impact of limiting DecodeType versus using MultyDecodeType on overall recognition speed.
// Tags: barcode, decode, multidecode, performance, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates measuring the performance difference between limited <see cref="DecodeType"/>
/// and <see cref="MultiDecodeType"/> when recognizing barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcode images, then measures and prints average recognition times.
    /// </summary>
    static void Main()
    {
        // Prepare a folder for sample barcode images
        string folder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        // Define sample barcodes to generate (type, text, file name)
        var samples = new (BaseEncodeType encode, string text, string fileName)[]
        {
            (EncodeTypes.Code128, "CODE128_SAMPLE", "code128.png"),
            (EncodeTypes.QR, "QR_SAMPLE", "qr.png"),
            (EncodeTypes.DataMatrix, "DATAMATRIX_SAMPLE", "datamatrix.png")
        };

        // Generate barcode images and save them to the folder
        foreach (var sample in samples)
        {
            string path = Path.Combine(folder, sample.fileName);
            using (var generator = new BarcodeGenerator(sample.encode, sample.text))
            {
                generator.Save(path);
            }
        }

        // Define decode types for limited (single) and multi decode scenarios
        var limitedDecodes = new (string name, BaseDecodeType decode)[]
        {
            ("Code128", DecodeType.Code128),
            ("QR", DecodeType.QR),
            ("DataMatrix", DecodeType.DataMatrix)
        };

        // MultiDecodeType that includes all three symbologies
        var multiDecode = new MultiDecodeType(DecodeType.Code128, DecodeType.QR, DecodeType.DataMatrix);

        // Header for the performance comparison output
        Console.WriteLine("Recognition speed comparison (average over 5 runs per image):");

        // Iterate over each sample image and measure both decoding approaches
        foreach (var sample in samples)
        {
            string imagePath = Path.Combine(folder, sample.fileName);
            Console.WriteLine($"\nImage: {sample.fileName}");

            // Find the matching limited decode type based on the file name (without extension)
            var limited = Array.Find(limitedDecodes, d => d.name == Path.GetFileNameWithoutExtension(sample.fileName));
            if (limited.decode == null)
            {
                Console.WriteLine("  No matching limited decode type found.");
                continue;
            }

            // Measure average time for limited (single) decode
            long limitedTotalMs = 0;
            for (int i = 0; i < 5; i++)
            {
                var sw = Stopwatch.StartNew();
                using (var reader = new BarCodeReader(imagePath, limited.decode))
                {
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Force recognition; result is not used further
                    }
                }
                sw.Stop();
                limitedTotalMs += sw.ElapsedMilliseconds;
            }
            double limitedAvg = limitedTotalMs / 5.0;

            // Measure average time for multi decode (all three types)
            long multiTotalMs = 0;
            for (int i = 0; i < 5; i++)
            {
                var sw = Stopwatch.StartNew();
                using (var reader = new BarCodeReader(imagePath, multiDecode))
                {
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Force recognition; result is not used further
                    }
                }
                sw.Stop();
                multiTotalMs += sw.ElapsedMilliseconds;
            }
            double multiAvg = multiTotalMs / 5.0;

            // Output the average times for both approaches
            Console.WriteLine($"  Limited decode ({limited.name}) avg time: {limitedAvg:F2} ms");
            Console.WriteLine($"  Multi decode (Code128+QR+DataMatrix) avg time: {multiAvg:F2} ms");
        }
    }
}