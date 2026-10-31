// Title: Multithreaded Barcode Reading Feature Flag Demo
// Description: Demonstrates how to toggle multithreaded barcode reading using Aspose.BarCode at application startup, generating a QR code and reading it with configurable threading.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, illustrating the use of BarcodeGenerator, BarCodeReader, and ProcessorSettings for controlling multithreading. Developers often need to optimize performance when processing many images; this snippet shows typical use cases such as enabling all CPU cores or limiting thread count via feature flags.
// Prompt: Implement a feature flag that enables or disables multithreaded barcode reading at application startup.
// Tags: qr, barcode generation, barcode recognition, multithreading, feature flag, aspose.barcode, processor settings

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that generates a QR barcode, then reads it using a configurable
/// multithreading feature flag. Demonstrates Aspose.BarCode generation, recognition,
/// and ProcessorSettings usage.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Sets up a feature flag, creates a temporary QR barcode,
    /// configures multithreading, reads the barcode, and cleans up resources.
    /// </summary>
    static void Main()
    {
        // Feature flag to enable or disable multithreaded barcode reading
        bool enableMultithread = true; // Change to false to disable

        // Create a unique temporary folder for the sample barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "sample.png");

        // Generate a simple QR barcode and save it to the temporary folder
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Aspose"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Configure multithreading settings based on the feature flag
        if (enableMultithread)
        {
            // Enable use of all available cores
            BarCodeReader.ProcessorSettings.UseAllCores = true;
            // Optionally limit the maximum additional threads
            BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 4;
        }
        else
        {
            // Disable multithreading
            BarCodeReader.ProcessorSettings.UseAllCores = false;
            BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 0;
        }

        // Read the generated barcode using the configured settings
        using (var reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            var results = reader.ReadBarCodes();
            foreach (var result in results)
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}, CodeText: {result.CodeText}");
            }
        }

        // Clean up temporary files
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}