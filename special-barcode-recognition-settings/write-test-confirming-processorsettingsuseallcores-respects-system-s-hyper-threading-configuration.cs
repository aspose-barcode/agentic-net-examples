// Title: Demonstrate ProcessorSettings.UseAllCores with Code128 barcode
// Description: Generates a Code128 barcode, saves it as PNG, then reads it using Aspose.BarCode while toggling the UseAllCores setting to show core utilization.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, illustrating how to control multi‑core processing via ProcessorSettings. It showcases BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and the ProcessorSettings API for managing CPU core usage—common tasks for developers optimizing performance in high‑throughput scanning scenarios.
// Prompt: Write a test confirming ProcessorSettings.UseAllCores respects the system's hyper‑threading configuration.
// Tags: code128, generation, recognition, png, barcodegenerator, barcodereader, processorsettings

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode, saves it as an image,
/// and demonstrates the effect of ProcessorSettings.UseAllCores on barcode reading performance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, verifies the file,
    /// and reads it twice: once with all CPU cores enabled and once with a limited core count.
    /// </summary>
    static void Main()
    {
        // Define the output path for the generated barcode image.
        string imagePath = "barcode.png";

        // Generate a simple Code128 barcode and save it to the specified file.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            generator.Save(imagePath);
        }

        // Verify that the image file was successfully created.
        if (!System.IO.File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Display the default ProcessorSettings.UseAllCores value.
        Console.WriteLine($"Default UseAllCores: {BarCodeReader.ProcessorSettings.UseAllCores}");

        // Enable the use of all processor cores for barcode reading.
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        Console.WriteLine($"After setting UseAllCores = true: {BarCodeReader.ProcessorSettings.UseAllCores}");

        // Read the barcode using all available cores.
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"[AllCores] Detected CodeText: {result.CodeText}");
            }
        }

        // Disable UseAllCores and limit the number of cores used for processing.
        BarCodeReader.ProcessorSettings.UseAllCores = false;
        int limitedCores = Math.Max(1, Environment.ProcessorCount / 2);
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = limitedCores;
        Console.WriteLine($"After disabling UseAllCores: {BarCodeReader.ProcessorSettings.UseAllCores}");
        Console.WriteLine($"Limited cores count: {BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount}");

        // Read the barcode again, this time using the limited core count.
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"[LimitedCores] Detected CodeText: {result.CodeText}");
            }
        }
    }
}