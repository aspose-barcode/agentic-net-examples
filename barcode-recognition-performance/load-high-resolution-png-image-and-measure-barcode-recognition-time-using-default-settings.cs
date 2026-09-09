// Title: Measure barcode recognition time for a high‑resolution PNG image
// Description: Demonstrates loading a high‑resolution PNG file and using Aspose.BarCode to recognize all supported barcode types while timing the operation.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating how to use BarCodeReader with default settings to process image files. It shows typical usage of DecodeType, BarCodeReader, and BarCodeResult classes for developers who need to quickly evaluate recognition performance on high‑resolution images.
// Prompt: Load a high‑resolution PNG image and measure barcode recognition time using default settings.
// Tags: barcode recognition, png, performance, decode type, barcodereader, aspnet, csharp

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates loading a high‑resolution PNG image and measuring barcode recognition time using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Reads the image, performs barcode detection, and outputs timing and results.
    /// </summary>
    static void Main()
    {
        // Path to the high‑resolution PNG image
        string imagePath = "high_res_barcode.png";

        // Verify that the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize BarCodeReader to detect all supported barcode types using default settings
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Start the stopwatch to measure recognition time
            Stopwatch watch = Stopwatch.StartNew();

            // Perform barcode detection
            BarCodeResult[] results = reader.ReadBarCodes();

            // Stop the stopwatch after detection completes
            watch.Stop();

            // Output the elapsed time in milliseconds
            Console.WriteLine($"Recognition time: {watch.ElapsedMilliseconds} ms");

            // Output the total number of barcodes found
            Console.WriteLine($"Barcodes found: {reader.FoundCount}");

            // Iterate through each detected barcode and display its type and text
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }
    }
}