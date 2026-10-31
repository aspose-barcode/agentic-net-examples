// Title: Read barcode count from image using Aspose.BarCode
// Description: Demonstrates how to load an image, detect all supported barcodes, and read the FoundCount property to know how many barcodes were found.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category. It shows how to use BarCodeReader with DecodeType.AllSupportedTypes to scan an image, retrieve the total number of detected barcodes via the FoundCount property, and enumerate each result. Developers working with barcode detection, image processing, or inventory automation often need to count and list barcodes in a picture, making this pattern a common starting point.
// Prompt: Read the FoundCount property to verify the total number of barcodes detected in the source image.
// Tags: barcode detection, foundcount, aspose.barcode, decodeall, image processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that reads an image, detects all supported barcodes,
/// and displays the total count along with each barcode's type and text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts an optional image path argument, falls back to "sample.png",
    /// then uses BarCodeReader to detect barcodes and prints the FoundCount.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may be the image file path.</param>
    static void Main(string[] args)
    {
        // Determine image path: use first argument or default sample file
        string imagePath = args.Length > 0 ? args[0] : "sample.png";

        // Verify that the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a BarCodeReader to detect all supported barcode types in the image
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Perform the recognition process
            reader.ReadBarCodes();

            // Output total number of detected barcodes using the FoundCount property
            Console.WriteLine($"FoundCount: {reader.FoundCount}");

            // Iterate through each detected barcode and display its type and decoded text
            foreach (BarCodeResult result in reader.FoundBarCodes)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }
    }
}