using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code128 barcode with a width reduction,
/// saving it to a file, and then reading it back to verify correctness.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, saves it, reads it back, and validates the result.
    /// </summary>
    static void Main()
    {
        // Output file path for the generated barcode image
        const string outputPath = "barcode.png";

        // Text to encode in the barcode
        const string codeText = "1234567890";

        // ------------------------------------------------------------
        // Generate a Code128 barcode with a 30 % width reduction
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Apply a 30‑point reduction to the bar width (approximately 30 %)
            generator.Parameters.Barcode.BarWidthReduction.Point = 30f;

            // Save the generated barcode image to the specified file
            generator.Save(outputPath);
        }

        // ------------------------------------------------------------
        // Read the generated barcode and verify its readability
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(outputPath, DecodeType.AllSupportedTypes))
        {
            // Attempt to read all barcodes from the image
            var results = reader.ReadBarCodes();

            // Check if at least one barcode was read and if it matches the original text
            if (results.Length > 0 && results[0].CodeText == codeText)
            {
                Console.WriteLine("Barcode read successfully: " + results[0].CodeText);
            }
            else
            {
                Console.WriteLine("Failed to read the barcode.");
            }
        }
    }
}