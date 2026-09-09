// Title: Generate Planet Barcodes with Unsupported Character Handling
// Description: Shows how to generate Planet barcodes while detecting unsupported characters, logging a warning, and skipping those entries.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and generation parameters to create barcode images. Developers often need to validate code text, handle invalid inputs, and produce image files for supported symbologies. The snippet demonstrates typical error handling patterns for barcode creation in .NET applications.
// Prompt: Handle cases where customer information uses unsupported characters by logging a warning and skipping generation.
// Tags: barcode, planet, error-handling, generation, png, aspnet, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating Planet barcodes and handling unsupported characters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes for sample texts, logs warnings for invalid inputs, and saves valid images.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for generated barcode images
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Sample code texts: one valid, one containing unsupported characters for the Planet symbology
        string[] codeTexts = { "1234567", "1234567WRONG" };

        // Iterate over each code text and attempt barcode generation
        foreach (string codeText in codeTexts)
        {
            // Build the full file path for the output PNG image
            string filePath = Path.Combine(outputFolder, $"Planet_{codeText}.png");
            try
            {
                // Initialize the barcode generator with Planet symbology and the current code text
                using (var generator = new BarcodeGenerator(EncodeTypes.Planet, codeText))
                {
                    // Configure the generator to throw an exception when the code text is invalid
                    generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

                    // Save the generated barcode image to the specified file in PNG format
                    generator.Save(filePath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated barcode saved to: {filePath}");
                }
            }
            catch (Exception ex)
            {
                // Log a warning and skip generation for invalid code texts
                Console.WriteLine($"Warning: Skipping generation for code text \"{codeText}\". Reason: {ex.Message}");
            }
        }

        Console.WriteLine("Processing completed.");
    }
}