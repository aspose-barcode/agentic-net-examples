using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Feature flag: enable multithreaded reading if true, otherwise single-threaded.
        bool enableMultithreading = true;
        if (args.Length > 0 && bool.TryParse(args[0], out bool parsed))
        {
            enableMultithreading = parsed;
        }

        // Configure processor settings based on the flag.
        BarCodeReader.ProcessorSettings.UseAllCores = enableMultithreading;
        if (!enableMultithreading)
        {
            // Use only half of the available cores, at least one.
            int cores = Math.Max(1, Environment.ProcessorCount / 2);
            BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = cores;
        }

        // Optional: limit additional threads.
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;

        // Prepare a sample barcode image.
        string imagePath = "sample.png";
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set a modest size.
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;
            generator.Save(imagePath);
        }

        // Verify the image exists before attempting to read.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image '{imagePath}' not found.");
            return;
        }

        // Read the barcode using the configured processor settings.
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Type: {result.CodeTypeName}");
                Console.WriteLine($"Text: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
            }
        }

        // Clean up the sample image.
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // Ignore any cleanup errors.
        }
    }
}