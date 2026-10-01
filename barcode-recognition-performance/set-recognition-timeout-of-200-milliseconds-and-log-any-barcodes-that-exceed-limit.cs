// Title: Barcode Recognition with Timeout and Logging
// Description: Demonstrates generating a Code128 barcode, reading it with a 200 ms timeout, and logging results or timeout failures.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes and BarCodeReader to decode them, highlighting how to configure a recognition timeout. Developers often need to limit scan duration in real‑time applications, and this pattern illustrates typical API usage for such scenarios, making it searchable for barcode timeout handling.
// Prompt: Set a recognition timeout of 200 milliseconds and log any barcodes that exceed the limit.
// Tags: barcode, code128, timeout, recognition, logging, aspose.barcode, generation, reading

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode, attempts to read it with a timeout,
/// and logs the outcome.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Generates a barcode image, reads it with a 200 ms timeout,
    /// and writes recognition results or timeout information to the console.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path of the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a simple Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file exists before attempting to read
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Barcode image was not created.");
            return;
        }

        // Set up the barcode reader with a 200 ms timeout
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            reader.Timeout = 200; // timeout in milliseconds

            try
            {
                // Attempt to read barcodes
                BarCodeResult[] results = reader.ReadBarCodes();

                // Log each recognized barcode
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
                    Console.WriteLine();
                }

                // Inform if no barcodes were recognized within the timeout
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcodes were recognized within the timeout.");
                }
            }
            catch (RecognitionAbortedException ex)
            {
                // Log that the recognition exceeded the timeout limit
                Console.WriteLine($"Recognition aborted after exceeding timeout of {reader.Timeout} ms.");
                Console.WriteLine($"ExecutionTime: {ex.ExecutionTime} ms");
            }
        }

        // Clean up temporary files
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}