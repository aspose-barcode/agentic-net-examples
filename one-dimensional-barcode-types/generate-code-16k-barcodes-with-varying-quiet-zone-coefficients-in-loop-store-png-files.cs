// Title: Generate Code 16K barcodes with varying quiet zone coefficients
// Description: Demonstrates creating Code 16K barcodes with different quiet zone settings and saving them as PNG images.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure Code16K symbology parameters such as aspect ratio and quiet zone coefficients. It shows typical usage of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes for batch barcode creation, a common task for developers needing customized barcode outputs.
// Prompt: Generate Code 16K barcodes with varying quiet zone coefficients in loop, store PNG files.
// Tags: barcode symbology, code16k, quiet zone, png output, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a series of Code 16K barcodes with varying quiet zone coefficients
/// and saves each barcode as a PNG file in an output directory.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output folder, defines quiet zone
    /// coefficient pairs, generates barcodes, and writes them to PNG files.
    /// </summary>
    static void Main()
    {
        // Define the output directory relative to the current working directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Code16K_Output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Quiet zone coefficient pairs (left, right) to be applied to each barcode
        var quietZonePairs = new (int left, int right)[]
        {
            (10, 1),
            (12, 2),
            (14, 3),
            (16, 4),
            (18, 5)
        };

        // Sample codetext; Code16K supports alphanumeric strings
        const string codeText = "SampleCode16K123";

        // Iterate over each coefficient pair and generate a barcode
        foreach (var (leftCoef, rightCoef) in quietZonePairs)
        {
            // Initialize the generator for Code16K symbology with the provided text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
            {
                // Set the aspect ratio (example value: 1.0)
                generator.Parameters.Barcode.Code16K.AspectRatio = 1.0f;

                // Apply the quiet zone coefficients for left and right sides
                generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = leftCoef;
                generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = rightCoef;

                // Build a descriptive file name that includes the coefficient values
                string fileName = $"Code16K_L{leftCoef}_R{rightCoef}.png";
                string filePath = Path.Combine(outputDir, fileName);

                // Save the generated barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Saved: {filePath}");
            }
        }
    }
}