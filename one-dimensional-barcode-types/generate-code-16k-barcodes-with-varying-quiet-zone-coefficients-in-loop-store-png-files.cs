// Title: Generate Code 16K barcodes with varying quiet zone coefficients
// Description: Demonstrates creating Code 16K barcodes with different left and right quiet‑zone coefficients and saving them as PNG files.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters such as X‑dimension and quiet‑zone coefficients using the BarcodeGenerator class. Typical use cases include batch creation of barcodes with varying layout requirements for packaging, labeling, or testing. Developers often need to adjust quiet‑zone settings to meet scanner specifications, and this snippet shows a loop‑based approach for generating multiple variants.
// Prompt: Generate Code 16K barcodes with varying quiet zone coefficients in loop, store PNG files.
// Tags: code16k, quietzone, barcode generation, png, aspose.barcode, loop, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating Code 16K barcodes with varying quiet‑zone coefficients and saving them as PNG images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates an output folder, iterates over quiet‑zone coefficient values, generates barcodes, and saves them.
    /// </summary>
    static void Main()
    {
        // Determine a unique temporary output directory
        string outputDir = Path.Combine(Path.GetTempPath(), "Code16K_" + Guid.NewGuid().ToString("N"));
        // Ensure the directory exists
        Directory.CreateDirectory(outputDir);
        // Text to encode in the barcode
        string codeText = "Aspose.BarCode";

        // Loop over left quiet‑zone coefficient values (10 to 12)
        for (int left = 10; left <= 12; left++)
        {
            // Loop over right quiet‑zone coefficient values (1 to 3)
            for (int right = 1; right <= 3; right++)
            {
                // Create a barcode generator for Code16K with the specified text
                using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
                {
                    // Set the X‑dimension (pixel width of the smallest bar)
                    generator.Parameters.Barcode.XDimension.Pixels = 2;
                    // Apply left and right quiet‑zone coefficients
                    generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = left;
                    generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = right;

                    // Build the file path that includes coefficient values
                    string filePath = Path.Combine(outputDir, $"Code16K_L{left}_R{right}.png");
                    // Save the generated barcode as a PNG image
                    generator.Save(filePath, BarCodeImageFormat.Png);
                } // generator disposed here
            }
        }

        // Inform the user about the generated files
        Console.WriteLine($"Generated 9 barcode images in {outputDir}");
    }
}