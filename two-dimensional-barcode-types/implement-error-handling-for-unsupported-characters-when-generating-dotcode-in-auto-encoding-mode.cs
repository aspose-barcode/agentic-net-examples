// Title: Generate DotCode barcode with error handling for unsupported characters
// Description: Demonstrates creating a DotCode barcode using Aspose.BarCode, setting auto‑encoding mode with an incompatible ECI encoding, and handling errors when the input contains characters that cannot be represented.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on DotCode symbology. It showcases the use of BarcodeGenerator, EncodeTypes, DotCodeEncodeMode, and ECIEncodings to control encoding behavior. Developers often need to generate DotCode for inventory or tracking applications and must handle unsupported characters gracefully, making this pattern useful for robust barcode creation.
// Prompt: Implement error handling for unsupported characters when generating DotCode in Auto encoding mode.
// Tags: dotcode, barcode, error-handling, auto-encoding, eci, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a DotCode barcode and handling unsupported characters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a DotCode barcode from sample text and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Sample text containing characters not supported by ISO-8859-1 (e.g., Japanese kanji)
        string codeText = "犬Right狗";

        // Output file in the temporary directory
        string outputPath = Path.Combine(Path.GetTempPath(), "dotcode_sample.png");

        // Generate the barcode and handle any encoding issues
        GenerateDotCode(codeText, outputPath);
    }

    /// <summary>
    /// Generates a DotCode barcode using auto‑encoding mode with a specific ECI encoding.
    /// Handles InvalidCodeException when the input contains characters that cannot be encoded.
    /// </summary>
    /// <param name="text">The text to encode into the barcode.</param>
    /// <param name="outputFile">The full path where the PNG image will be saved.</param>
    static void GenerateDotCode(string text, string outputFile)
    {
        // Initialize a BarcodeGenerator for DotCode with the provided text
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, text))
        {
            // Configure auto encode mode and set an ECI encoding that cannot represent the characters
            generator.Parameters.Barcode.DotCode.EncodeMode = DotCodeEncodeMode.Auto;
            generator.Parameters.Barcode.DotCode.ECIEncoding = ECIEncodings.ISO_8859_1;

            try
            {
                // Attempt to save the barcode image as PNG
                generator.Save(outputFile, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode saved to: {outputFile}");
            }
            catch (InvalidCodeException ex)
            {
                // Handle unsupported characters gracefully
                Console.WriteLine($"Unsupported character encountered: {ex.Message}");
            }
            catch (Exception ex)
            {
                // General error handling for any other issues
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}