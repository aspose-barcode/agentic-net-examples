// Title: ProcessorSettings Core Count Validation Test
// Description: Demonstrates how to verify that BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount never exceeds the machine's physical core count.
// Category-Description: This example belongs to the Aspose.BarCode multi‑threading and performance tuning category. It shows how to work with the ProcessorSettings class to control core usage, a common requirement when optimizing barcode generation and recognition workloads. Developers often need to ensure that configured thread counts respect hardware limits to avoid over‑subscription and degraded performance.
// Prompt: Write a test confirming ProcessorSettings.UseOnlyThisCoresCount does not exceed the physical core count.
// Tags: barcode, multithreading, processor-settings, core-count, test, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that validates ProcessorSettings core count does not exceed physical cores.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that runs the validation test.
    /// </summary>
    static void Main()
    {
        // Determine the number of physical CPU cores available on the host.
        int physicalCores = Environment.ProcessorCount;
        Console.WriteLine($"Physical cores: {physicalCores}");

        // Create a temporary folder to store the generated barcode image.
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a simple QR barcode and save it as a PNG file.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Test123"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Configure ProcessorSettings with a core count that exceeds the physical core count.
        BarCodeReader.ProcessorSettings.UseAllCores = false;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = physicalCores + 2;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 0;

        // Read back the actual configured core count.
        int configuredCores = BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount;
        Console.WriteLine($"Configured cores (requested): {physicalCores + 2}");
        Console.WriteLine($"Configured cores (actual): {configuredCores}");

        // Verify that the actual configured cores do not exceed the physical core count.
        if (configuredCores > physicalCores)
        {
            Console.WriteLine("Test FAILED: UseOnlyThisCoresCount exceeds physical core count.");
        }
        else
        {
            Console.WriteLine("Test PASSED: UseOnlyThisCoresCount does not exceed physical core count.");
        }

        // Perform a simple barcode read to ensure settings are applied without error.
        using (var reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            var results = reader.ReadBarCodes();
            Console.WriteLine($"Barcodes detected: {results.Length}");
        }

        // Clean up temporary files and directories.
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failure should not affect test outcome.
        }
    }
}