// Title: Multithreaded Barcode Reading Feature Flag Demo
// Description: Demonstrates how to enable or disable multithreaded barcode reading using a startup feature flag.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of BarCodeReader and its ProcessorSettings to control threading. Developers often need to toggle multithreading for performance tuning or resource constraints; this snippet shows command‑line configuration of the UseAllCores and UseOnlyThisCoresCount properties.
// Prompt: Implement a feature flag that enables or disables multithreaded barcode reading at application startup.
// Tags: barcode symbology, multithreading, feature flag, aspose.barcode, code128, image generation, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample console application that demonstrates a feature flag for enabling or disabling
/// multithreaded barcode reading using Aspose.BarCode's <see cref="BarCodeReader"/>.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Accepts an optional command‑line argument ("true" or "false")
    /// to control whether barcode reading should use all CPU cores.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument toggles multithreading.</param>
    static void Main(string[] args)
    {
        // Determine whether multithreading is enabled via a feature flag.
        // Default is true; can be overridden by a command‑line argument.
        bool enableMultithreading = true;
        if (args.Length > 0 && bool.TryParse(args[0], out bool parsed))
        {
            enableMultithreading = parsed;
        }

        // Path to the sample barcode image.
        string imagePath = "sample.png";

        // Generate a sample barcode image if it does not already exist.
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
        }

        // Configure processor settings based on the feature flag.
        // These settings affect all BarCodeReader instances.
        BarCodeReader.ProcessorSettings.UseAllCores = enableMultithreading;
        if (!enableMultithreading)
        {
            // When multithreading is disabled, restrict processing to a single core.
            BarCodeReader.ProcessorSettings.UseAllCores = false;
            BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 1;
        }

        // Verify the image file exists before attempting to read.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Read the barcode using BarCodeReader.
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
                Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
            }
        }
    }
}