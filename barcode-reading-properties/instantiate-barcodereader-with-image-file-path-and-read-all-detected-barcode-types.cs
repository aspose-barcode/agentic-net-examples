// Title: Read All Barcode Types from an Image
// Description: Demonstrates how to instantiate BarCodeReader with an image file path and read every supported barcode type present in the image.
// Prompt: Instantiate BarCodeReader with an image file path and read all detected barcode types.
// Tags: barcode, symbology, read, alltypes, console, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that reads all supported barcode types from a given image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Instantiates a <see cref="BarCodeReader"/> and outputs detected barcodes.
    /// </summary>
    static void Main()
    {
        // Path to the image containing barcodes
        string imagePath = "sample.png";

        // Verify that the image file exists before attempting to read
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Create the reader for the image file inside a using block to ensure proper disposal
        using (BarCodeReader reader = new BarCodeReader(imagePath))
        {
            // Configure the reader to detect all supported barcode types
            reader.BarCodeReadType = DecodeType.AllSupportedTypes;

            // Perform the recognition and retrieve all results
            BarCodeResult[] results = reader.ReadBarCodes();

            // Iterate through each detected barcode and display its type and decoded text
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                Console.WriteLine($"BarCode CodeText: {result.CodeText}");
            }
        }
    }
}