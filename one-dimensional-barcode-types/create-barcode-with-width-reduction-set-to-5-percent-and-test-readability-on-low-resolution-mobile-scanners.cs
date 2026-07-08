// Title: Create Code128 barcode with 5% width reduction for low‑resolution scanner testing
// Description: Demonstrates generating a Code128 barcode with a 5 percent bar‑width reduction and simulating a low‑resolution mobile scanner to verify readability.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, illustrating how to configure barcode appearance (bar‑width reduction) and resolution settings, then read the barcode using high‑quality recognition. Developers often need to adjust bar dimensions and test scanning performance on devices with limited DPI, using classes like BarcodeGenerator, BarCodeReader, and QualitySettings.
// Prompt: Create a barcode with width reduction set to 5 percent and test readability on low‑resolution mobile scanners.
// Tags: code128, barwidthreduction, lowresolution, barcode-generation, barcode-recognition, qualitysettings, png

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode with a 5 percent bar‑width reduction,
/// saves it as a PNG, and then reads it back using high‑quality settings
/// to simulate scanning on a low‑resolution mobile device.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, saves it,
    /// verifies the file, and performs recognition.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "barcode.png";

        // Create a Code128 barcode with sample text "1234567890".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Simulate a low‑resolution mobile scanner by setting the image resolution to 72 DPI.
            generator.Parameters.Resolution = 72f;

            // Apply a 5 percent bar‑width reduction to the barcode.
            generator.Parameters.Barcode.BarWidthReduction.Point = 5f;

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image file was successfully created.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine($"Failed to create barcode image at '{outputPath}'.");
            return;
        }

        // Read the barcode using high‑quality settings to emulate a mobile scanner.
        using (var reader = new BarCodeReader(outputPath, DecodeType.AllSupportedTypes))
        {
            // Enable high‑quality recognition to improve detection of low‑resolution images.
            reader.QualitySettings = QualitySettings.HighQuality;

            // Iterate through all detected barcodes in the image.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
                Console.WriteLine($"Reading Quality: {result.ReadingQuality}");

                // Output the bounding rectangle of the detected barcode region.
                var bounds = result.Region.Rectangle;
                Console.WriteLine($"Region: X={bounds.X}, Y={bounds.Y}, Width={bounds.Width}, Height={bounds.Height}");
            }
        }
    }
}