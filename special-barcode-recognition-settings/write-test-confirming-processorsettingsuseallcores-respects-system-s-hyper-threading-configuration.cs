// Title: Demonstrate ProcessorSettings.UseAllCores with barcode recognition
// Description: Shows how to generate a Code128 barcode, then read it while toggling ProcessorSettings.UseAllCores to observe core usage behavior.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It illustrates the use of BarcodeGenerator, BarCodeReader, and the ProcessorSettings class to control multithreading during barcode processing. Developers often need to optimize performance on multi‑core or hyper‑threaded systems, and this snippet demonstrates typical configuration patterns for such scenarios.
// Prompt: Write a test confirming ProcessorSettings.UseAllCores respects the system's hyper‑threading configuration.
// Tags: barcode symbology, generation, recognition, processor settings, multithreading, csharp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Common;

/// <summary>
/// Example program that generates a Code128 barcode, then reads it using Aspose.BarCode
/// while toggling <c>ProcessorSettings.UseAllCores</c> to demonstrate core usage behavior.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode image, runs recognition with different <c>ProcessorSettings</c>,
    /// and outputs the found barcode counts.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a temporary folder for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeTest");
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "test.png");

        // Generate a simple Code128 barcode and save it to the temporary folder
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            generator.Save(barcodePath);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Test 1: Enable UseAllCores to allow the reader to use all logical processors
        // ------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        Console.WriteLine($"ProcessorSettings.UseAllCores set to: {BarCodeReader.ProcessorSettings.UseAllCores}");
        Console.WriteLine($"Logical processor count (including hyper‑threading): {Environment.ProcessorCount}");

        int foundCountAllCores = ReadBarcodes(barcodePath);
        Console.WriteLine($"FoundCount with UseAllCores=true: {foundCountAllCores}");
        Console.WriteLine();

        // ------------------------------------------------------------
        // Test 2: Disable UseAllCores and limit the number of cores used
        // ------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseAllCores = false;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Math.Max(1, Environment.ProcessorCount / 2);
        Console.WriteLine($"ProcessorSettings.UseAllCores set to: {BarCodeReader.ProcessorSettings.UseAllCores}");
        Console.WriteLine($"ProcessorSettings.UseOnlyThisCoresCount set to: {BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount}");

        int foundCountLimitedCores = ReadBarcodes(barcodePath);
        Console.WriteLine($"FoundCount with limited cores: {foundCountLimitedCores}");

        // Clean up temporary files and folder
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect test result
        }
    }

    /// <summary>
    /// Reads barcodes from the specified image file and returns the number of barcodes found.
    /// </summary>
    /// <param name="imagePath">Path to the image containing barcodes.</param>
    /// <returns>The count of detected barcodes.</returns>
    static int ReadBarcodes(string imagePath)
    {
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Perform recognition
            reader.ReadBarCodes();

            // Return the count of detected barcodes
            return reader.FoundCount;
        }
    }
}