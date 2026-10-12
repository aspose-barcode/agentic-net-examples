// Title: Generate Code 16K barcodes with varying quiet zone coefficients
// Description: Demonstrates creating Code 16K barcodes with different left and right quiet‑zone coefficients and saving them as PNG files.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters such as X‑dimension and quiet‑zone coefficients using the BarcodeGenerator class. Typical use cases include batch creation of barcodes with varying layout requirements for printing or digital distribution. Developers often need to adjust quiet zones to meet scanner specifications or visual design constraints.
// Prompt: Generate Code 16K barcodes with varying quiet zone coefficients in loop, store PNG files.
// Tags: code16k, quietzone, barcode generation, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating Code 16K barcodes with varying quiet‑zone coefficients and saving them as PNG images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates an output folder, iterates over quiet‑zone coefficient combinations,
    /// generates corresponding barcodes, and writes them to PNG files.
    /// </summary>
    static void Main()
    {
        // Determine a unique temporary output directory
        string outputDir = Path.Combine(Path.GetTempPath(), "Code16K_" + Guid.NewGuid().ToString("N"));
        // Ensure the directory exists
        Directory.CreateDirectory(outputDir);

        // Text to encode in the barcode
        string codeText = "Aspose.Barcode";

        // Loop over left quiet‑zone coefficient values (10 to 12)
        for (int leftCoef = 10; leftCoef <= 12; leftCoef++)
        {
            // Loop over right quiet‑zone coefficient values (1 to 3)
            for (int rightCoef = 1; rightCoef <= 3; rightCoef++)
            {
                // Create a generator for Code16K with the specified text
                using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
                {
                    // Set the X‑dimension (module width) in pixels
                    generator.Parameters.Barcode.XDimension.Pixels = 2;
                    // Apply the left quiet‑zone coefficient
                    generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = leftCoef;
                    // Apply the right quiet‑zone coefficient
                    generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = rightCoef;

                    // Build the file name based on the current coefficients
                    string filePath = Path.Combine(outputDir, $"Code16K_L{leftCoef}_R{rightCoef}.png");
                    // Save the generated barcode as a PNG image
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }
            }
        }

        // Inform the user where the generated images are stored
        Console.WriteLine($"Generated barcode images saved to: {outputDir}");
    }
}