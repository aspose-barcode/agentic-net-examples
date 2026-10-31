// Title: Validate and Adjust MaxAdditionalAllowedThreads for Aspose.BarCode
// Description: Demonstrates how to verify that ProcessorSettings.MaxAdditionalAllowedThreads does not exceed a calculated system limit, adjusts it if necessary, and then generates and reads a simple barcode.
// Category-Description: This example belongs to the Aspose.BarCode processing settings category, illustrating how to manage threading limits using BarCodeReader.ProcessorSettings. It shows typical use cases such as validating thread counts before barcode generation and recognition, a common requirement for developers optimizing performance and resource usage in high‑throughput scanning applications.
// Prompt: Write a validation routine ensuring ProcessorSettings.MaxAdditionalAllowedThreads does not exceed system limits.
// Tags: barcode symbology, thread management, validation, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that validates the maximum allowed additional threads for barcode processing,
/// generates a Code128 barcode, reads it back, and cleans up temporary resources.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Determine a safe system limit for additional threads (example: twice the processor count)
        int systemThreadLimit = Environment.ProcessorCount * 2;

        // Intentionally set a value that may exceed the limit to demonstrate validation
        int desiredThreads = Environment.ProcessorCount * 3;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = desiredThreads;
        Console.WriteLine($"Requested MaxAdditionalAllowedThreads: {desiredThreads}");

        // Validate the setting and adjust it if it exceeds the calculated system limit
        if (BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads > systemThreadLimit)
        {
            Console.WriteLine($"Warning: MaxAdditionalAllowedThreads exceeds system limit ({systemThreadLimit}). Adjusting to limit.");
            BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = systemThreadLimit;
        }

        Console.WriteLine($"Effective MaxAdditionalAllowedThreads: {BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads}");

        // Create a temporary folder to store the sample barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "sample.png");

        // Generate a simple Code128 barcode and save it as a PNG file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Read the generated barcode using the appropriate decode type
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            var results = reader.ReadBarCodes();
            foreach (var result in results)
            {
                Console.WriteLine($"Detected: {result.CodeTypeName} - {result.CodeText}");
            }
        }

        // Clean up temporary files and directory; ignore any errors during cleanup
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Suppress cleanup exceptions
        }
    }
}