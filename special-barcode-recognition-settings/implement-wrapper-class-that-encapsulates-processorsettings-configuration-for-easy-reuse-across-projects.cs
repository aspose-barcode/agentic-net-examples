// Title: Demonstrate configuring Aspose.BarCode ProcessorSettings via a reusable wrapper
// Description: Shows how to generate a Code128 barcode, read it with default settings, then apply custom processor settings through a wrapper class for easy reuse.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It illustrates the use of BarcodeGenerator, BarCodeReader, and the static ProcessorSettings class to control multi‑core processing. Developers often need to tune performance or resource usage when scanning large batches of images; encapsulating these settings in a wrapper simplifies configuration across projects and promotes consistent behavior.
// Prompt: Implement a wrapper class that encapsulates ProcessorSettings configuration for easy reuse across projects.
// Tags: barcode symbology, generation, recognition, processorsettings, wrapper, csharp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Encapsulates configuration of <see cref="BarCodeReader.ProcessorSettings"/> for reuse.
/// </summary>
public static class ProcessorSettingsWrapper
{
    /// <summary>
    /// Applies the specified processor settings to the static <see cref="BarCodeReader.ProcessorSettings"/> instance.
    /// </summary>
    /// <param name="useAllCores">If true, all CPU cores are used for barcode recognition.</param>
    /// <param name="onlyThisCoresCount">Number of cores to restrict processing to when <paramref name="useAllCores"/> is false.</param>
    /// <param name="maxAdditionalThreads">Maximum number of additional threads allowed for processing.</param>
    public static void Apply(bool useAllCores, int onlyThisCoresCount, int maxAdditionalThreads)
    {
        // Configure the global processor settings used by BarCodeReader instances.
        BarCodeReader.ProcessorSettings.UseAllCores = useAllCores;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = onlyThisCoresCount;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = maxAdditionalThreads;
    }
}

class Program
{
    /// <summary>
    /// Demonstrates barcode generation, reading with default settings, applying custom processor settings,
    /// and reading again to show the effect of the configuration.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the demo files.
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "code128.png");

        // Generate a Code128 barcode image and save it as PNG.
        BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345");
        generator.Save(imagePath, BarCodeImageFormat.Png);
        Console.WriteLine($"Barcode image saved to: {imagePath}");

        // -----------------------------------------------------------------
        // Read the barcode using the default processor settings.
        // -----------------------------------------------------------------
        Console.WriteLine("\nReading with default ProcessorSettings:");
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            BarCodeResult[] results = reader.ReadBarCodes();
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }

        // -----------------------------------------------------------------
        // Apply custom processor settings (single‑core mode) via the wrapper.
        // -----------------------------------------------------------------
        ProcessorSettingsWrapper.Apply(useAllCores: false, onlyThisCoresCount: 1, maxAdditionalThreads: 0);
        Console.WriteLine("\nProcessorSettings configured for single‑core recognition.");

        // -----------------------------------------------------------------
        // Read the barcode again with the new processor settings.
        // -----------------------------------------------------------------
        Console.WriteLine("\nReading with custom ProcessorSettings:");
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            BarCodeResult[] results = reader.ReadBarCodes();
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }

        // Cleanup temporary files (optional).
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup is not critical for the demo.
        }
    }
}