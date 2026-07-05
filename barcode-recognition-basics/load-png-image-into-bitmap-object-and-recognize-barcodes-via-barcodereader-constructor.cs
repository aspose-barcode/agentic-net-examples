// Title: Barcode Recognition from PNG using Aspose.BarCode
// Description: Loads a PNG image into a Bitmap and uses BarCodeReader to detect and output all supported barcode types found in the image.
// Prompt: Load a PNG image into a Bitmap object and recognize barcodes via BarCodeReader constructor.
// Tags: barcode, recognition, png, aspose, csharp

using System;
using System.IO;
using Aspose.Drawing;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates loading a PNG image and recognizing any barcodes it contains using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Loads the image, creates a reader, and prints detected barcode information.
    /// </summary>
    static void Main()
    {
        // Path to the PNG image file
        const string imagePath = "sample.png";

        // Verify that the image file exists before attempting to load it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Load the PNG image into an Aspose.Drawing.Bitmap instance
        using (Bitmap bitmap = new Bitmap(imagePath))
        // Initialize BarCodeReader with the bitmap, configuring it to detect all supported barcode types
        using (BarCodeReader reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
        {
            // Iterate through all detected barcodes and output their type and decoded text
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                Console.WriteLine($"BarCode Text: {result.CodeText}");
            }
        }
    }
}