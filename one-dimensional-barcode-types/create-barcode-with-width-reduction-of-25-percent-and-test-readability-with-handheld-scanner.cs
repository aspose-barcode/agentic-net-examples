// Title: Generate Code128 barcode with 25% width reduction and verify readability
// Description: Creates a Code128 barcode image with a 25 percent width reduction and then reads it back to confirm it can be scanned with a handheld device.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates using BarcodeGenerator to customize barcode dimensions and BarCodeReader to decode the generated image. Developers often need to adjust barcode size for specific printing constraints and validate scanability across devices.
// Prompt: Create a barcode with width reduction of 25 percent and test readability with a handheld scanner.
// Tags: code128, width-reduction, png, generation, recognition, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating a Code128 barcode with a 25 percent width reduction,
/// saving it as a PNG file, and then reading it back to verify scanner readability.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it, and validates it.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "barcode.png";

        // Create a Code128 barcode with the sample text "1234567890".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Apply a 25% width reduction to the barcode bars.
            generator.Parameters.Barcode.BarWidthReduction.Point = 0.25f;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image file was successfully created.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine($"Failed to create barcode image at '{outputPath}'.");
            return;
        }

        // Initialize a barcode reader to decode the saved image.
        using (var reader = new BarCodeReader(outputPath, DecodeType.AllSupportedTypes))
        {
            // Use a high‑performance quality preset for faster reading.
            reader.QualitySettings = QualitySettings.HighPerformance;

            // Iterate through all detected barcodes in the image.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
                Console.WriteLine($"Reading Quality: {result.ReadingQuality}%");
            }
        }
    }
}