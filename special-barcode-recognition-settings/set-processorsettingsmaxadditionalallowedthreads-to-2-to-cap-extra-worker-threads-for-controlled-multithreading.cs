// Title: Limit Additional Worker Threads for Barcode Processing
// Description: Shows how to cap the number of extra worker threads used by Aspose.BarCode's processor settings and then reads barcodes from an image.
// Category-Description: This example belongs to the Aspose.BarCode configuration and recognition category. It demonstrates using the static ProcessorSettings class to control multithreading resources, a common requirement when integrating barcode scanning into high‑throughput or resource‑constrained applications. Developers typically adjust ProcessorSettings to balance performance and CPU usage while using BarCodeReader for image‑based barcode detection.
// Prompt: Set ProcessorSettings.MaxAdditionalAllowedThreads to 2 to cap extra worker threads for controlled multithreading.
// Tags: barcode, multithreading, configuration, barcodereader, processorsettings, aspose.barcode

using System;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates setting a limit on additional worker threads for barcode processing
/// and optionally reads barcodes from a sample image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Configures thread limits and performs barcode reading if a sample image is present.
    /// </summary>
    static void Main()
    {
        // Set the maximum number of additional worker threads to 2.
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 2;
        Console.WriteLine("ProcessorSettings.MaxAdditionalAllowedThreads set to " + BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads);

        // Path to a sample image that may contain barcodes.
        string sampleImage = "sample.png";

        // Check if the sample image exists before attempting to read.
        if (System.IO.File.Exists(sampleImage))
        {
            // Initialize the barcode reader with the image file.
            using (var reader = new BarCodeReader(sampleImage))
            {
                // Iterate through all detected barcodes and output their type and text.
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected barcode: Type={result.CodeTypeName}, Text={result.CodeText}");
                }
            }
        }
        else
        {
            // Inform the user that the sample image was not found.
            Console.WriteLine($"Sample image '{sampleImage}' not found. Skipping barcode reading.");
        }
    }
}