// Title: Validate and Set MaxAdditionalAllowedThreads for Aspose.BarCode Processor
// Description: Demonstrates how to validate a requested thread count against system limits and apply it to Aspose.BarCode's ProcessorSettings. The example also generates and reads a QR barcode.
// Category-Description: This example belongs to the Aspose.BarCode processing configuration category, illustrating the use of BarCodeReader.ProcessorSettings to control multithreading. It shows typical usage of environment-derived limits, the BarcodeGenerator for creating barcodes, and BarCodeReader for decoding. Developers often need to tune thread settings for performance and resource management in high‑throughput barcode scanning scenarios.
// Prompt: Write a validation routine ensuring ProcessorSettings.MaxAdditionalAllowedThreads does not exceed system limits.
// Tags: barcode, threading, validation, aspose.barcode, qr, generation, recognition

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates validation of ProcessorSettings.MaxAdditionalAllowedThreads and basic QR barcode generation and recognition using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample application.
    /// </summary>
    static void Main()
    {
        // Sample requested value (could be from args or config)
        int requestedThreads = 100;
        ValidateAndSetMaxAdditionalAllowedThreads(requestedThreads);

        // Create a temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a simple QR barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Read the generated barcode using all supported decode types
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            BarCodeResult[] results = reader.ReadBarCodes();
            Console.WriteLine($"Barcodes found: {results.Length}");
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Clean up temporary files and directory
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }

    /// <summary>
    /// Validates the requested thread count against a system-derived limit and applies it to BarCodeReader.ProcessorSettings.
    /// </summary>
    /// <param name="requested">The desired number of additional allowed threads.</param>
    static void ValidateAndSetMaxAdditionalAllowedThreads(int requested)
    {
        // Example system-derived limit: twice the number of logical processors
        int systemLimit = Environment.ProcessorCount * 2;
        int finalValue = requested;

        if (requested > systemLimit)
        {
            Console.WriteLine($"Requested MaxAdditionalAllowedThreads ({requested}) exceeds system limit ({systemLimit}). Capping to limit.");
            finalValue = systemLimit;
        }
        else if (requested < 0)
        {
            Console.WriteLine($"Requested MaxAdditionalAllowedThreads ({requested}) is negative. Setting to 0.");
            finalValue = 0;
        }

        // Apply the validated value to the Aspose.BarCode processor settings
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = finalValue;
        Console.WriteLine($"ProcessorSettings.MaxAdditionalAllowedThreads set to {finalValue}.");
    }
}