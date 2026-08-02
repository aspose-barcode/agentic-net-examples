// Title: Demonstrate barcode generation and reading with AllowIncorrectBarcodes
// Description: This example generates a Code128 barcode, saves it as PNG, then reads it while allowing potentially incorrect barcodes for debugging.
// Category-Description: Shows how to use Aspose.BarCode generation and recognition APIs, focusing on QualitySettings.AllowIncorrectBarcodes. Developers working with barcode validation, debugging unreadable barcodes, or handling low‑quality scans can reference this pattern. Key classes include BarcodeGenerator, BarCodeReader, and QualitySettings.
// Prompt: Enable QualitySettings.AllowIncorrectBarcodes to capture potentially unreadable barcodes during debugging sessions.
// Tags: barcode generation, barcode recognition, allowincorrectbarcodes, code128, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a Code128 barcode image,
/// reads it back with relaxed quality settings, and cleans up the file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads it with
    /// <c>QualitySettings.AllowIncorrectBarcodes</c> enabled, and outputs the results.
    /// </summary>
    static void Main()
    {
        // Define the barcode text to encode.
        const string codeText = "1234567890";

        // Define the output image file path.
        const string imagePath = "sample_barcode.png";

        // ------------------------------------------------------------
        // Generate a Code128 barcode and save it as a PNG image.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Read the generated barcode with relaxed quality settings.
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Allow detection of barcodes that may be unreadable or malformed.
            reader.QualitySettings.AllowIncorrectBarcodes = true;

            // Iterate through all detected barcodes (there should be only one).
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeType}");
                Console.WriteLine($"Detected Text: {result.CodeText}");
            }
        }

        // ------------------------------------------------------------
        // Optional cleanup: delete the generated image file.
        // ------------------------------------------------------------
        if (File.Exists(imagePath))
        {
            try
            {
                File.Delete(imagePath);
            }
            catch
            {
                // Suppress any errors that occur during file deletion.
            }
        }
    }
}