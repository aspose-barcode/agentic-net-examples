// Title: Barcode Recognition with Timeout and Logging
// Description: Demonstrates setting a 200 ms recognition timeout when reading a barcode and handling cases where the timeout is exceeded.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, illustrating how to configure the BarCodeReader's Timeout property, execute barcode detection, and capture RecognitionAbortedException for time‑critical scenarios. Developers commonly use these APIs to process images quickly, enforce performance limits, and log timeout events in batch or real‑time processing pipelines.
// Prompt: Set a recognition timeout of 200 milliseconds and log any barcodes that exceed the limit.
// Tags: barcode, recognition, timeout, logging, code128, aspose.barcode, barcodereader, barcodereader.timeout, exception handling

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, reading with a timeout, and cleanup of temporary resources.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, attempts to read it with a 200 ms timeout,
    /// logs results or timeout exceptions, and then cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeTimeoutDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a simple Code128 barcode and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Initialize the barcode reader with the generated image
        using (BarCodeReader reader = new BarCodeReader(barcodePath))
        {
            // Set the recognition timeout to 200 milliseconds
            reader.Timeout = 200; // milliseconds

            try
            {
                // Attempt to read barcodes from the image
                BarCodeResult[] results = reader.ReadBarCodes();

                // Check if any barcodes were detected
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcodes were detected.");
                }
                else
                {
                    // Log each detected barcode's type and text
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"Detected: {result.CodeTypeName} - {result.CodeText}");
                    }
                }
            }
            catch (RecognitionAbortedException ex)
            {
                // Log timeout information when recognition exceeds the specified limit
                Console.WriteLine($"Recognition aborted after exceeding timeout of 200 ms. Execution time: {ex.ExecutionTime} ms");
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
}