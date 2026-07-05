// Title: Switching between HighPerformance and HighQuality barcode reading presets
// Description: Demonstrates generating a Code128 barcode, then reading it twice using Aspose.BarCode's HighPerformance and HighQuality quality settings.
// Prompt: Write documentation examples demonstrating how to switch between HighPerformance and HighQuality presets.
// Tags: barcode symbology, generation, recognition, highperformance, highquality, aspnet, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that shows how to generate a barcode and read it using different quality presets.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode image, then reads it with HighPerformance and HighQuality settings.
    /// </summary>
    static void Main()
    {
        // Define the full path for the generated barcode image
        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "sample.png");

        // -------------------------------------------------
        // Generate a Code128 barcode and save it to a PNG file
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Save the barcode image to the specified path
            generator.Save(imagePath);
        }

        // -------------------------------------------------
        // Read the barcode using the HighPerformance preset
        // -------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Set the reader to prioritize speed over accuracy
            reader.QualitySettings = QualitySettings.HighPerformance;

            // Iterate through all detected barcodes and output their text
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine("HighPerformance: " + result.CodeText);
            }
        }

        // -------------------------------------------------
        // Read the same barcode using the HighQuality preset
        // -------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Set the reader to prioritize accuracy over speed
            reader.QualitySettings = QualitySettings.HighQuality;

            // Iterate through all detected barcodes and output their text
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine("HighQuality: " + result.CodeText);
            }
        }
    }
}