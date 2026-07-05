// Title: Read FoundCount Property to Verify Detected Barcodes
// Description: Demonstrates how to use Aspose.BarCode to read an image, detect all supported barcodes, and retrieve the total count via the FoundCount property.
// Prompt: Read the FoundCount property to verify the total number of barcodes detected in the source image.
// Tags: barcode, detection, foundcount, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that reads an image, detects all supported barcodes,
/// and reports the total number of barcodes found using the FoundCount property.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Path to the image containing barcodes
        string imagePath = "sample.png";

        // Verify that the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize the barcode reader for all supported symbologies
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Perform barcode detection on the image
            reader.ReadBarCodes();

            // Retrieve the total number of detected barcodes via the FoundCount property
            int totalBarcodes = reader.FoundCount;
            Console.WriteLine($"Total barcodes detected: {totalBarcodes}");

            // Optionally, output each detected barcode's text value
            for (int i = 0; i < totalBarcodes; i++)
            {
                Console.WriteLine($"Barcode {i + 1}: {reader.FoundBarCodes[i].CodeText}");
            }
        }
    }
}