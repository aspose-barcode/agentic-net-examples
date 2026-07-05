// Title: Barcode recognition with timeout and performance logging
// Description: Demonstrates setting a 200 ms recognition timeout and logging barcodes that exceed this limit.
// Prompt: Set a recognition timeout of 200 milliseconds and log any barcodes that exceed the limit.
// Tags: barcode, timeout, logging, code128, aspose.barcodes, csharp

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a barcode, reads it with a timeout,
/// and logs any barcode whose processing time exceeds the specified limit.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample barcode image, then reads it while measuring
    /// processing time and enforcing a 200 ms timeout.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a sample barcode image to read.
        // --------------------------------------------------------------------
        const string imagePath = "sample.png";
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // Save the generated barcode to a PNG file.
            generator.Save(imagePath);
        }

        // --------------------------------------------------------------------
        // Verify the image exists before attempting recognition.
        // --------------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image '{imagePath}' not found.");
            return;
        }

        // --------------------------------------------------------------------
        // Initialize the reader with all supported symbologies.
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Set the recognition timeout to 200 milliseconds.
            reader.Timeout = 200;

            try
            {
                // Measure processing time for each detected barcode.
                var stopwatch = Stopwatch.StartNew();

                // Iterate through all detected barcodes in the image.
                foreach (var result in reader.ReadBarCodes())
                {
                    long elapsedMs = stopwatch.ElapsedMilliseconds;

                    // Log barcode details.
                    Console.WriteLine($"Detected barcode: Type = {result.CodeTypeName}, Text = {result.CodeText}");

                    // If processing time exceeds the timeout, log a warning.
                    if (elapsedMs > 200)
                    {
                        Console.WriteLine($"Warning: Processing time {elapsedMs} ms exceeds the 200 ms limit for barcode '{result.CodeText}'.");
                    }

                    // Reset the stopwatch for the next barcode.
                    stopwatch.Restart();
                }
            }
            catch (Exception ex)
            {
                // Log any errors, including possible timeout exceptions.
                Console.WriteLine($"Recognition error or timeout: {ex.Message}");
            }
        }
    }
}