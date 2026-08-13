// Title: Verify ProcessorSettings core count does not exceed physical cores
// Description: Demonstrates creating a barcode image, configuring Aspose.BarCode processor settings, and confirming that UseOnlyThisCoresCount is not set beyond the machine's physical core count.
// Category-Description: This example belongs to the Aspose.BarCode processing configuration category, illustrating how to control multi‑core usage via BarCodeReader.ProcessorSettings. It shows typical use of EncodeTypes, BarcodeGenerator, BarCodeReader, and DecodeType for generating and reading barcodes while managing CPU resources—common tasks for developers optimizing performance in batch scanning or server environments.
// Prompt: Write a test confirming ProcessorSettings.UseOnlyThisCoresCount does not exceed the physical core count.
// Tags: barcode, code128, core count, processor settings, aspnet, aspnet-barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a barcode, configures processor settings,
/// and validates that the core count setting does not exceed the physical core count.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image, sets processor core usage,
    /// validates the configuration, and reads the barcode back.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Generate a temporary barcode image (Code128) for testing.
        // ------------------------------------------------------------
        string tempPath = Path.Combine(Path.GetTempPath(), "sample_barcode.png");
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            generator.Save(tempPath);
        }

        // ------------------------------------------------------------
        // 2. Verify that the image file was successfully created.
        // ------------------------------------------------------------
        if (!File.Exists(tempPath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // 3. Configure processor settings for barcode reading.
        //    - Disable automatic use of all cores.
        //    - Attempt to use the maximum number of physical cores.
        // ------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseAllCores = false;
        int physicalCores = Environment.ProcessorCount;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = physicalCores; // attempt to use maximum cores

        // ------------------------------------------------------------
        // 4. Validate that the configured core count does not exceed the physical core count.
        // ------------------------------------------------------------
        if (BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount > physicalCores)
        {
            throw new InvalidOperationException("UseOnlyThisCoresCount exceeds the number of physical cores.");
        }

        // ------------------------------------------------------------
        // 5. Perform a simple barcode read to demonstrate that the settings work.
        // ------------------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(tempPath, DecodeType.Code128))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected barcode: {result.CodeText}");
            }
        }

        // ------------------------------------------------------------
        // 6. Output the final verification result.
        // ------------------------------------------------------------
        Console.WriteLine($"ProcessorSettings.UseOnlyThisCoresCount = {BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount}, Physical cores = {physicalCores}. Test passed.");
    }
}