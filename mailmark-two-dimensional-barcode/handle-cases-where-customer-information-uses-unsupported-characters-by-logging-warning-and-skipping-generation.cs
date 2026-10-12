// Title: Generate Planet Barcodes with Validation and Skipping Invalid Inputs
// Description: Demonstrates creating Planet barcodes, validating code text, logging warnings for unsupported characters, and skipping those entries.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.Planet, configure strict validation via ThrowExceptionWhenCodeTextIncorrect, and handle errors gracefully. Developers often need to generate barcodes in batch while ensuring input data conforms to symbology rules; this pattern shows typical use cases such as logging and skipping invalid records.
// Prompt: Handle cases where customer information uses unsupported characters by logging a warning and skipping generation.
// Tags: barcode, planet, validation, error-handling, generation, png, aspnet, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of Planet barcodes with strict validation,
/// logging warnings for unsupported characters, and skipping invalid entries.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates an output folder, iterates over sample
    /// code texts, generates barcodes for valid inputs, and logs warnings for invalid ones.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary output folder for the generated barcode images
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Sample code texts: some contain unsupported characters for Planet barcode (only digits allowed)
        var codeTexts = new List<string>
        {
            "1234567",          // valid
            "12345AB",          // invalid characters
            "9876543",          // valid
            "12#34*6",          // invalid characters
            "7654321"           // valid
        };

        // Process each code text individually
        foreach (var text in codeTexts)
        {
            // Build the full file path for the output PNG image
            string filePath = Path.Combine(outputFolder, $"Planet_{text}.png");
            try
            {
                // Initialize the barcode generator with Planet symbology and the current text
                using (var generator = new BarcodeGenerator(EncodeTypes.Planet, text))
                {
                    // Enable strict validation: throw exception on invalid characters
                    generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

                    // Save the generated barcode image as PNG
                    generator.Save(filePath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated barcode for '{text}' at: {filePath}");
                }
            }
            catch (Exception ex)
            {
                // Log a warning and skip generation for this entry
                Console.WriteLine($"Warning: Unable to generate barcode for '{text}'. Reason: {ex.Message}");
            }
        }

        Console.WriteLine("Processing completed.");
    }
}