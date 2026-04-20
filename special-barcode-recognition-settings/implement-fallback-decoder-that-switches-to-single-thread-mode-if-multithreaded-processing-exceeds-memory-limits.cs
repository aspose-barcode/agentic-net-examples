using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        const string imagePath = "sample.png";

        // Ensure a barcode image exists for the demo
        if (!File.Exists(imagePath))
        {
            GenerateSampleBarcode(imagePath);
        }

        // Try multithreaded decoding first
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Math.Max(1, Environment.ProcessorCount / 2);
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;

        try
        {
            DecodeBarcodes(imagePath);
        }
        catch (OutOfMemoryException)
        {
            // Fallback to single‑thread mode when memory limit is hit
            Console.WriteLine("Out of memory detected. Switching to single‑thread mode.");

            BarCodeReader.ProcessorSettings.UseAllCores = false;
            BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 1;
            BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 0;

            // Retry decoding with the new settings
            DecodeBarcodes(imagePath);
        }
    }

    // Generates a simple Code128 barcode image
    private static void GenerateSampleBarcode(string path)
    {
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Demo123"))
        {
            generator.Save(path);
        }
    }

    // Performs barcode decoding and prints results
    private static void DecodeBarcodes(string path)
    {
        using (var reader = new BarCodeReader(path, DecodeType.Code128, DecodeType.QR))
        {
            // Optional: set a timeout to avoid hanging on very large images
            reader.Timeout = 5000;

            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }
    }
}