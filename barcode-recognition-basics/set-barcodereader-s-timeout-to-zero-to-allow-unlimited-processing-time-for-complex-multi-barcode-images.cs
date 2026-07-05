// Title: Unlimited Barcode Processing with BarCodeReader Timeout
// Description: Demonstrates setting BarCodeReader.Timeout to zero to allow unlimited processing time when reading complex images containing multiple barcodes.
// Prompt: Set BarCodeReader's TimeOut to zero to allow unlimited processing time for complex multi‑barcode images.
// Tags: barcode, timeout, multibarcode, aspose, csharp

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that reads all barcodes from an image using Aspose.BarCode.
/// It sets the reader's <c>Timeout</c> to zero, which disables the time limit
/// and enables processing of complex multi‑barcode images without interruption.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Path to the image containing multiple barcodes.
        const string imagePath = "multi_barcodes.png";

        // Verify that the image file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Create a BarCodeReader for the specified image file.
        using (var reader = new BarCodeReader(imagePath))
        {
            // Set the timeout to zero to allow unlimited processing time.
            reader.Timeout = 0;

            // Configure the reader to detect all supported barcode types.
            reader.BarCodeReadType = DecodeType.AllSupportedTypes;

            // Iterate through all detected barcodes and output their type and text.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }
    }
}