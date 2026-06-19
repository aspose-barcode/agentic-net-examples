using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating various barcode types, then reading them in parallel using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates sample barcodes, reads them concurrently, and cleans up resources.
    /// </summary>
    static void Main()
    {
        // Prepare a small set of sample barcodes.
        var samples = new List<(BaseEncodeType Type, Bitmap Image)>();

        // Define the barcode types to generate.
        BaseEncodeType[] encodeTypes = new BaseEncodeType[]
        {
            EncodeTypes.Code128,
            EncodeTypes.QR,
            EncodeTypes.DataMatrix,
            EncodeTypes.Pdf417,
            EncodeTypes.EAN13
        };

        // Corresponding text values for each barcode type.
        string[] texts = new string[]
        {
            "ABC123456",
            "https://example.com",
            "DM12345",
            "PDF417Sample",
            "1234567890128" // valid EAN13 with checksum
        };

        // Generate barcode images and store them with their type.
        for (int i = 0; i < encodeTypes.Length; i++)
        {
            using (var generator = new BarcodeGenerator(encodeTypes[i], texts[i]))
            {
                // Generate the barcode image in memory.
                Bitmap bmp = generator.GenerateBarCodeImage();

                // Store the bitmap for later parallel reading.
                samples.Add((encodeTypes[i], bmp));
            }
        }

        // Configure the reader to use all available processor cores.
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

        // Parallel processing of barcode images.
        Parallel.ForEach(samples, sample =>
        {
            // Each thread works with its own bitmap instance.
            using (var reader = new BarCodeReader(sample.Image, DecodeType.AllSupportedTypes))
            {
                // Read all barcodes found in the image.
                foreach (var result in reader.ReadBarCodes())
                {
                    // Output the thread ID, barcode type, and decoded text.
                    Console.WriteLine($"Thread {Task.CurrentId}: Detected {result.CodeTypeName} - \"{result.CodeText}\"");
                }
            }
        });

        // Dispose all generated bitmaps to free unmanaged resources.
        foreach (var sample in samples)
        {
            sample.Image.Dispose();
        }
    }
}