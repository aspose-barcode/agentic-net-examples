// Title: Multithreaded Barcode Reading Demo
// Description: Demonstrates generating a Code128 barcode image and reading it with optional multithreaded processing based on a startup flag.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, illustrating how to use BarcodeGenerator to create barcodes and BarCodeReader with ProcessorSettings to control multithreading. Typical use cases include batch processing of images where performance can be tuned by enabling or disabling multi‑core execution. Developers often need to configure thread pools and processor settings to balance speed and resource usage.
// Prompt: Implement a feature flag that enables or disables multithreaded barcode reading at application startup.
// Tags: barcode generation, barcode recognition, multithreading, code128, aspose.barcode, .net

using System;
using System.IO;
using System.Threading;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation and conditional multithreaded barcode reading using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Generates a Code128 barcode, optionally enables multithreaded reading,
    /// reads the barcode, outputs the result, and cleans up temporary files.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument can be a boolean to enable/disable multithreading.</param>
    static void Main(string[] args)
    {
        // Determine whether multithreading should be enabled based on command‑line input.
        bool enableMultithreading = true;
        if (args.Length > 0 && bool.TryParse(args[0], out bool flag))
        {
            enableMultithreading = flag;
        }

        // Configure the .NET thread pool to match the number of logical processors.
        ThreadPool.SetMinThreads(Environment.ProcessorCount, Environment.ProcessorCount);
        ThreadPool.SetMaxThreads(Environment.ProcessorCount * 2, Environment.ProcessorCount * 2);

        // Create a temporary folder to store the generated barcode image.
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "sample.png");

        // Generate a Code128 barcode and save it as a PNG file.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Initialize the barcode reader for the generated image.
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Apply multithreading settings if the feature flag is enabled.
            if (enableMultithreading)
            {
                BarCodeReader.ProcessorSettings.UseAllCores = true;
                BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;
                BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;
            }

            // Read barcodes from the image.
            var results = reader.ReadBarCodes();

            // Output each detected barcode's type and text.
            foreach (var result in results)
            {
                Console.WriteLine($"Detected barcode: Type={result.CodeTypeName}, Text={result.CodeText}");
            }
        }

        // Clean up temporary files and directories.
        try
        {
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
            if (Directory.Exists(tempFolder))
            {
                Directory.Delete(tempFolder, true);
            }
        }
        catch
        {
            // Cleanup failures are ignored.
        }
    }
}