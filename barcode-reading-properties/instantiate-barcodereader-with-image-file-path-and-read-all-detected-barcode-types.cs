// Title: Read all barcode types from an image using BarCodeReader
// Description: Demonstrates how to instantiate Aspose.BarCode.BarCodeReader with an image file path and retrieve all detected barcode types.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the use of BarCodeReader and DecodeType.AllSupportedTypes to detect multiple symbologies in a single image. Typical use cases include batch processing of scanned documents, inventory verification, and automated data capture where various barcode formats may appear. Developers often need to enumerate detected barcodes and extract their type and text for further processing.
// Prompt: Instantiate BarCodeReader with an image file path and read all detected barcode types.
// Tags: barcode, barcode recognition, decode type, all supported types, aspose.barcode, c#, console

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates reading all supported barcode types from an image file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts an optional image path argument, validates the file, and prints detected barcode types and texts.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may be the image file path.</param>
    static void Main(string[] args)
    {
        // Determine the image path: use the first argument if provided, otherwise default to "sample.png".
        string imagePath = args.Length > 0 ? args[0] : "sample.png";

        // Verify that the specified image file exists before attempting to read.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a BarCodeReader that scans for all supported barcode types in the image.
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Read all barcodes detected in the image.
            BarCodeResult[] results = reader.ReadBarCodes();

            // If no barcodes were found, inform the user.
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
            }
            else
            {
                // Iterate through each detected barcode and output its type and decoded text.
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
        }
    }
}