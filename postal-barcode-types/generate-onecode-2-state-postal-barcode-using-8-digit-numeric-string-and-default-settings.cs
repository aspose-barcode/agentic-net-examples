// Title: Generate OneCode 2‑state Postal Barcode
// Description: Creates a OneCode 2‑state postal barcode from an 8‑digit numeric string and saves it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator with EncodeTypes.OneCode. Developers commonly generate postal barcodes for mail sorting and tracking, adjusting size parameters via the Parameters.Barcode API. The snippet shows default settings, image format selection, and error handling, useful for quick integration in C# applications.
// Prompt: Generate a OneCode 2‑state postal barcode using an 8‑digit numeric string and default settings.
// Tags: onecode, postal barcode, barcode generation, png, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a OneCode 2‑state postal barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode and saves it to a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the 8‑digit numeric string to encode.
        string codeText = "12345678";

        // Build the full output path for the generated PNG image.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "OneCode.png");

        try
        {
            // Initialize the barcode generator with OneCode symbology and the provided text.
            using (var generator = new BarcodeGenerator(EncodeTypes.OneCode, codeText))
            {
                // Optionally adjust size parameters; defaults are used otherwise.
                generator.Parameters.Barcode.XDimension.Pixels = 4;   // Width of a single module.
                generator.Parameters.Barcode.BarHeight.Pixels = 50; // Height of the barcode.

                // Save the generated barcode image in PNG format.
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"OneCode barcode saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during generation.
            Console.WriteLine($"Failed to generate OneCode barcode: {ex.Message}");
        }
    }
}