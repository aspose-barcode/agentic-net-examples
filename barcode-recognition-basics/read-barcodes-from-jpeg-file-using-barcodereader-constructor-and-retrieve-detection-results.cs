// Title: Read Barcodes from JPEG using BarCodeReader
// Description: Demonstrates how to load a JPEG image, detect all supported barcodes, and output their type, text, and location.
// Prompt: Read barcodes from a JPEG file using BarCodeReader constructor and retrieve detection results.
// Tags: barcode, read, jpeg, aspose, barcodereader, detection, console

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that reads all supported barcodes from a JPEG image
/// and prints their type, decoded text, and bounding region to the console.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Path to the JPEG image containing barcodes.
        const string imagePath = "sample.jpg";

        // Verify that the file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize the BarCodeReader for all supported barcode types.
        // The using statement ensures the reader is disposed properly.
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Iterate through all detected barcodes in the image.
            foreach (var result in reader.ReadBarCodes())
            {
                // Output the barcode type (symbology) and the decoded text.
                Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                Console.WriteLine($"BarCode Text: {result.CodeText}");

                // Retrieve and display the bounding rectangle of the detected barcode.
                var rect = result.Region.Rectangle;
                Console.WriteLine($"Region - X:{rect.X}, Y:{rect.Y}, Width:{rect.Width}, Height:{rect.Height}");

                Console.WriteLine(); // Blank line for readability between results.
            }
        }
    }
}