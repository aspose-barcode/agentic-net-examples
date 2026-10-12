// Title: ProcessorSettings Wrapper Example for Aspose.BarCode
// Description: Demonstrates how to encapsulate Aspose.BarCode processor settings in a reusable wrapper class and use it to generate and read a Code128 barcode.
// Category-Description: This example belongs to the Aspose.BarCode processing configuration category. It shows how to work with the ProcessorSettings API to control multithreading behavior, and combines it with barcode generation (BarcodeGenerator) and recognition (BarCodeReader). Developers often need to tune performance settings for high‑throughput scanning scenarios, and a wrapper class simplifies reuse across projects.
// Prompt: Implement a wrapper class that encapsulates ProcessorSettings configuration for easy reuse across projects.
// Tags: barcode symbology, generation, recognition, processor settings, aspose.barcode, csharp

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Wrapper for configuring Aspose.BarCode <see cref="BarCodeReader.ProcessorSettings"/>.
/// Provides a simple way to set multithreading options in a single object that can be reused across projects.
/// </summary>
public class ProcessorSettingsWrapper
{
    /// <summary>
    /// Gets or sets a value indicating whether all available CPU cores should be used.
    /// </summary>
    public bool UseAllCores { get; set; }

    /// <summary>
    /// Gets or sets the number of cores to use when <see cref="UseAllCores"/> is false.
    /// </summary>
    public int UseOnlyThisCoresCount { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of additional threads allowed for processing.
    /// </summary>
    public int MaxAdditionalAllowedThreads { get; set; }

    /// <summary>
    /// Applies the configured settings to <see cref="BarCodeReader.ProcessorSettings"/>.
    /// </summary>
    public void Apply()
    {
        BarCodeReader.ProcessorSettings.UseAllCores = UseAllCores;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = UseOnlyThisCoresCount;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = MaxAdditionalAllowedThreads;
    }
}

class Program
{
    /// <summary>
    /// Entry point of the example. Configures processor settings, generates a barcode, reads it back, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Configure processor settings via the wrapper
        var settingsWrapper = new ProcessorSettingsWrapper
        {
            UseAllCores = false,
            UseOnlyThisCoresCount = 1,
            MaxAdditionalAllowedThreads = 0
        };
        settingsWrapper.Apply();

        // Create a temporary directory for the barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "sample.png");

        // Generate a simple Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Set up the reader to decode Code128 barcodes
        BaseDecodeType decodeType = DecodeType.Code128;
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            // Measure recognition time
            Stopwatch sw = Stopwatch.StartNew();
            reader.ReadBarCodes();
            sw.Stop();

            // Output results
            Console.WriteLine($"Barcodes read: {reader.FoundCount}, Recognition time: {sw.ElapsedMilliseconds} ms");
            foreach (var result in reader.FoundBarCodes)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Attempt to delete temporary files and directory
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored - cleanup failure should not crash the program
        }
    }
}