// Title: Barcode Recognition with Timeout and Logging
// Description: Demonstrates how to generate a Code128 barcode, recognize it with a 200 ms timeout, and log results or timeout events.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, showcasing the use of BarCodeReader for scanning barcodes under time constraints. It highlights key API classes such as BarcodeGenerator, BarCodeReader, and RecognitionAbortedException, which are commonly used for real‑time scanning scenarios where developers need to enforce processing limits and handle timeout exceptions gracefully. Ideal for developers building inventory, point‑of‑sale, or logistics applications that require fast barcode validation.
/// Prompt: Set a recognition timeout of 200 milliseconds and log any barcodes that exceed the limit.
// Tags: barcode, code128, recognition, timeout, logging, aspose.barcode, generation, reading

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode, attempts to read it with a
/// 200 ms timeout, and logs the outcome.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image, reads it with a timeout,
    /// and outputs the results or timeout information to the console.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a Code128 barcode image and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify the image exists before attempting recognition
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Barcode image not found: " + barcodePath);
            return;
        }

        // Initialize the reader with a 200 ms timeout for Code128 decoding
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            reader.Timeout = 200; // timeout in milliseconds

            try
            {
                // Attempt to read barcodes within the specified timeout
                BarCodeResult[] results = reader.ReadBarCodes();
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Found barcode: Type={result.CodeTypeName}, Text={result.CodeText}");
                }
            }
            catch (RecognitionAbortedException ex)
            {
                // Log timeout information when recognition exceeds the allowed time
                Console.WriteLine($"Recognition aborted after exceeding timeout of {reader.Timeout} ms. Execution time: {ex.ExecutionTime} ms");
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