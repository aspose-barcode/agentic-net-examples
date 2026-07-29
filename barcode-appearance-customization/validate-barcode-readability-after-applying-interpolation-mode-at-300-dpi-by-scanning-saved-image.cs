// Title: Validate barcode readability with interpolation at 300 dpi
// Description: Demonstrates generating a Code128 barcode using interpolation mode at 300 dpi, saving it, and verifying readability by scanning the image.
// Category-Description: This example belongs to the Aspose.BarCode image generation and recognition category. It showcases the BarcodeGenerator for creating high‑resolution barcodes with AutoSizeMode.Interpolation and the BarCodeReader for decoding. Developers use these APIs to produce printable barcodes and ensure they can be read by scanners in real‑world applications.
// Prompt: Validate barcode readability after applying Interpolation mode at 300 dpi by scanning the saved image.
// Tags: code128, interpolation, 300dpi, barcode generation, barcode recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a barcode with interpolation mode at 300 dpi and validating its readability.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode image, saves it, and reads it back to confirm the encoded text.
    /// </summary>
    static void Main()
    {
        const string barcodePath = "sample_barcode.png";
        const string codeText = "ABC1234567890";

        // Generate barcode with Interpolation mode and 300 dpi resolution
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation; // Enable interpolation for smoother scaling
            generator.Parameters.Resolution = 300f; // Set resolution to 300 DPI
            generator.Save(barcodePath); // Save the generated barcode image
        }

        // Verify that the image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine($"Error: Barcode image not found at '{barcodePath}'.");
            return;
        }

        // Read and validate the barcode from the saved image
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            bool found = false;

            // Iterate through all detected barcodes
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeType}");
                Console.WriteLine($"Detected Text: {result.CodeText}");
                Console.WriteLine($"Reading Quality: {result.ReadingQuality}");

                // Check if the decoded text matches the original
                if (result.CodeText == codeText)
                {
                    found = true;
                }
            }

            // Output validation result
            if (found)
            {
                Console.WriteLine("Barcode readability validation succeeded.");
            }
            else
            {
                Console.WriteLine("Barcode readability validation failed: expected text not found.");
            }
        }
    }
}