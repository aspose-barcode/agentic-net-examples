// Title: Read JPEG Barcodes with HighQuality Preset
// Description: Demonstrates creating a BarCodeReader for a JPEG image and applying the HighQuality preset to balance speed and accuracy.
// Prompt: Create a BarCodeReader instance that reads JPEG images and applies HighQuality preset for balanced speed.
// Tags: barcode, recognition, jpeg, highquality, aspose, csharp

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that reads barcodes from a JPEG image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Loads a JPEG file, configures the reader,
    /// and outputs detected barcode information to the console.
    /// </summary>
    static void Main()
    {
        // Path to the JPEG image containing barcodes
        string imagePath = "sample.jpg";

        // Verify that the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize BarCodeReader for the specified image and enable detection of all supported barcode types
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Apply the HighQuality preset – provides a good trade‑off between speed and robustness
            reader.QualitySettings = QualitySettings.HighQuality;

            // Iterate through all detected barcodes and display their details
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text   : {result.CodeText}");
                Console.WriteLine($"Confidence  : {result.Confidence}");
                Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
                Console.WriteLine();
            }
        }
    }
}