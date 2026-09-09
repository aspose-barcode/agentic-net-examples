// Title: Read all barcode types from an image using Aspose.BarCode
// Description: Demonstrates how to instantiate BarCodeReader with a file path and retrieve every supported barcode type found in the image.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, illustrating the use of BarCodeReader and DecodeType.AllSupportedTypes to detect multiple symbologies in a single image. Developers commonly need to process scanned documents, receipts, or product images and extract any barcode present, using the Aspose.BarCode API classes such as BarCodeReader, BarCodeResult, and DecodeType.
// Prompt: Instantiate BarCodeReader with an image file path and read all detected barcode types.
// Tags: barcode, recognition, allsupportedtypes, aspose.barcode, c#, console

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that reads all supported barcode types from a given image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Loads an image, checks its existence, and prints any detected barcodes.
    /// </summary>
    static void Main()
    {
        // Path to the image that may contain barcodes.
        string imagePath = "sample.png";

        // Verify that the file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Create a BarCodeReader that scans for all supported barcode symbologies.
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Perform the detection and retrieve results.
            BarCodeResult[] results = reader.ReadBarCodes();

            // If no barcodes were found, inform the user.
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
            }
            else
            {
                // Output each detected barcode's type and decoded text.
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }
        }
    }
}