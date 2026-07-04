// Title: Barcode Generation with Interpolation Mode and Validation
// Description: Generates a Code128 barcode using interpolation auto-size at 300 dpi, saves it as PNG, then reads it back to confirm readability.
// Prompt: Validate barcode readability after applying Interpolation mode at 300 dpi by scanning the saved image.
// Tags: code128, interpolation, 300dpi, png, barcode generation, barcode recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating a barcode with interpolation auto‑size at 300 dpi,
/// saving it as an image, and then verifying that the barcode can be read back.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, saves it, and validates its readability.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "barcode.png";

        // --------------------------------------------------------------------
        // Generate the barcode image
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Apply the Interpolation auto‑size mode so the image is scaled automatically.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set the image resolution to 300 dpi for high‑quality output.
            generator.Parameters.Resolution = 300f;

            // Specify explicit image dimensions (in points) because BarHeight is ignored in Interpolation mode.
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Verify that the image file was created successfully
        // --------------------------------------------------------------------
        if (!File.Exists(outputPath))
        {
            Console.WriteLine($"Error: Barcode image not found at '{outputPath}'.");
            return;
        }

        // --------------------------------------------------------------------
        // Read and validate the barcode from the saved image
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(outputPath, DecodeType.AllSupportedTypes))
        {
            bool found = false;

            // Iterate through all detected barcodes in the image.
            foreach (var result in reader.ReadBarCodes())
            {
                found = true;
                Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Decoded Text: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
                Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
            }

            // Inform the user if no barcode was detected.
            if (!found)
            {
                Console.WriteLine("No barcode detected in the image.");
            }
        }
    }
}