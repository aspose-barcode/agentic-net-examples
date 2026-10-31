// Title: Generate OneCode 2‑state postal barcode from 8‑digit numeric string
// Description: Demonstrates creating a OneCode 2‑state postal barcode using Aspose.BarCode with an 8‑digit numeric input and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on postal symbologies. It shows how to use the BarcodeGenerator class with EncodeTypes.OneCode to produce a OneCode 2‑state barcode, a common requirement for postal automation and mailing systems. Developers often need to generate such barcodes programmatically for batch processing, label printing, or integration with mailing software.
// Prompt: Generate a OneCode 2‑state postal barcode using an 8‑digit numeric string and default settings.
// Tags: onecode, postal barcode, generation, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a OneCode 2‑state postal barcode from an 8‑digit numeric string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the 8‑digit numeric string to encode.
        string codeText = "12345678";

        // Build the output directory path relative to the current working directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        Directory.CreateDirectory(outputDir); // Ensure the directory exists.

        // Full path for the generated PNG image.
        string outputPath = Path.Combine(outputDir, "OneCode_8Digits.png");

        try
        {
            // Initialize the barcode generator with OneCode symbology and the provided text.
            using (var generator = new BarcodeGenerator(EncodeTypes.OneCode, codeText))
            {
                // Save the barcode using default settings to a PNG file.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Barcode generated successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during generation.
            Console.WriteLine($"Failed to generate OneCode barcode: {ex.Message}");
        }
    }
}