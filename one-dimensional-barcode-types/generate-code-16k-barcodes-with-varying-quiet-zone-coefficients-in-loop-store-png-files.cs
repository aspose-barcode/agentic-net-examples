// Title: Generate Code 16K barcodes with varying quiet zone coefficients
// Description: Demonstrates creating Code 16K barcodes with different left and right quiet‑zone coefficients and saving each as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure quiet‑zone settings for Code 16K symbology using the BarcodeGenerator class. Developers often need to adjust quiet zones to meet scanner requirements or layout constraints, and this snippet shows typical API usage for setting X‑dimension, quiet‑zone coefficients, and exporting to PNG.
// Prompt: Generate Code 16K barcodes with varying quiet zone coefficients in loop, store PNG files.
// Tags: code16k, barcode, quietzone, png, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating Code 16K barcodes with varying quiet‑zone coefficients and saving them as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates an output directory, iterates over quiet‑zone coefficient combinations,
    /// generates corresponding barcodes, and writes each image to disk.
    /// </summary>
    static void Main()
    {
        // Build a unique temporary folder for the generated images
        string outputDir = Path.Combine(Path.GetTempPath(), "Code16K_QuietZones_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Text to encode in each barcode
        string codeText = "Aspose.Barcode";

        // Loop through desired left quiet‑zone coefficients (10‑12)
        for (int leftCoef = 10; leftCoef <= 12; leftCoef++)
        {
            // Loop through desired right quiet‑zone coefficients (1‑3)
            for (int rightCoef = 1; rightCoef <= 3; rightCoef++)
            {
                // Create a generator for Code 16K with the specified text
                using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
                {
                    // Set the X‑dimension (module width) in pixels
                    generator.Parameters.Barcode.XDimension.Pixels = 2;

                    // Apply the current quiet‑zone coefficients
                    generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = leftCoef;
                    generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = rightCoef;

                    // Build the file name reflecting the coefficient values
                    string fileName = $"Code16K_L{leftCoef}_R{rightCoef}.png";
                    string filePath = Path.Combine(outputDir, fileName);

                    // Save the barcode image as PNG
                    generator.Save(filePath, BarCodeImageFormat.Png);

                    // Output the location of the saved file
                    Console.WriteLine($"Saved: {filePath}");
                }
            }
        }
    }
}