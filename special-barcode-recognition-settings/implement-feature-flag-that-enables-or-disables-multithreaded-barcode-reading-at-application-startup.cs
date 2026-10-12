// Title: Feature Flag for Multithreaded Barcode Reading
// Description: Demonstrates how to enable or disable multithreaded barcode reading using a startup flag.
// Category-Description: This example belongs to the Aspose.BarCode reading category, showcasing the use of BarCodeReader and its ProcessorSettings to control multithreading. Developers often need to toggle multithreaded decoding for performance tuning or resource constraints. The sample covers barcode generation, reading, and cleanup, illustrating typical workflows for Code128 barcodes.
// Prompt: Implement a feature flag that enables or disables multithreaded barcode reading at application startup.
// Tags: barcode, symbology, reading, multithreading, feature flag, aspose.barcode, code128, image generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that generates a Code128 barcode, optionally enables multithreaded
/// reading via a feature flag, reads the barcode, and cleans up temporary files.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Parses command‑line arguments to set the multithreading flag,
    /// creates a temporary barcode image, reads it, and outputs the result.
    /// </summary>
    /// <param name="args">Command‑line arguments; use "disable" to turn off multithreading.</param>
    static void Main(string[] args)
    {
        // Determine whether multithreaded reading should be enabled.
        // The flag defaults to true and can be disabled by passing "disable" as the first argument.
        bool enableMultithread = true;
        if (args.Length > 0 && args[0].Equals("disable", StringComparison.OrdinalIgnoreCase))
        {
            enableMultithread = false;
        }

        // Create a unique temporary folder for the sample barcode image.
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a simple Code128 barcode image and save it as PNG.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Apply the multithreading setting based on the feature flag.
        BarCodeReader.ProcessorSettings.UseAllCores = enableMultithread;

        // Read the barcode using the selected decode type (Code128).
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            var results = reader.ReadBarCodes();
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Attempt to clean up the temporary folder and its contents.
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any exceptions that occur during cleanup.
        }
    }
}