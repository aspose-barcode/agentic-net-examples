// Title: Left-align human‑readable text for UPC‑A barcodes
// Description: Demonstrates generating multiple UPC‑A barcodes with the human‑readable text aligned to the left using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure CodeTextParameters (Alignment, Location) for barcode images. It shows typical usage of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce PNG files, a common requirement for developers creating printable or displayable barcodes in .NET applications.
// Prompt: Align barcode text left for a series of UPC‑A barcodes by setting TextAlignment.Left.
// Tags: upc-a, text-alignment, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a set of UPC‑A barcode images with left‑aligned human‑readable text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates an output folder, iterates over sample UPC‑A codes,
    /// configures text alignment, and saves each barcode as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output directory for generated barcode images
        string outputDir = "Barcodes";

        // Create the directory if it does not already exist
        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Sample UPC‑A codes (each code must contain exactly 12 digits)
        string[] upcCodes = new string[]
        {
            "012345678905",
            "123456789012",
            "036000291452",
            "070123456789",
            "041000005264"
        };

        // Process each UPC‑A code
        foreach (string code in upcCodes)
        {
            // Initialize a barcode generator for the UPC‑A symbology with the current code
            using (var generator = new BarcodeGenerator(EncodeTypes.UPCA, code))
            {
                // Align the human‑readable text to the left side of the barcode
                generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Left;

                // Ensure the text appears below the bars (explicitly set for clarity)
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

                // Build the full file path for the PNG image
                string filePath = Path.Combine(outputDir, $"{code}.png");

                // Save the generated barcode image in PNG format
                generator.Save(filePath, BarCodeImageFormat.Png);

                // Output a confirmation message to the console
                Console.WriteLine($"Saved barcode for {code} to {filePath}");
            }
        }
    }
}