// Title: Read All Barcode Types from an Image using Aspose.BarCode
// Description: Demonstrates how to instantiate BarCodeReader with an image file path and retrieve every detected barcode type.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, illustrating the use of BarCodeReader and DecodeType to scan images for all supported barcode symbologies. Typical scenarios include batch processing of scanned documents, inventory verification, and automated data capture where multiple barcode formats may appear. Developers often need quick, code‑first solutions to enumerate and decode any barcode present in an image.
// Prompt: Instantiate BarCodeReader with an image file path and read all detected barcode types.
// Tags: barcode symbology, read, all types, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that reads all supported barcode types from an image file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts an optional image path argument, validates the file, and prints detected barcodes.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may be the image file path.</param>
    static void Main(string[] args)
    {
        // Determine image path: use first argument or fallback to a default file name.
        string imagePath = args.Length > 0 ? args[0] : "sample.png";

        // Verify that the file exists before attempting to read.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Create a BarCodeReader that scans the image for all supported barcode types.
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Iterate through all detected barcodes and output their type and decoded text.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                Console.WriteLine($"BarCode CodeText: {result.CodeText}");
                Console.WriteLine(); // Blank line for readability.
            }
        }
    }
}