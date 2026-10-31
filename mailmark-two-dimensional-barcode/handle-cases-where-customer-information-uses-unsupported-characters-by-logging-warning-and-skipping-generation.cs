// Title: Australia Post Barcode Generation with Validation and Skipping Invalid Codes
// Description: Demonstrates generating Australia Post barcodes, validating customer information, and skipping codes containing unsupported characters.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to use BarcodeGenerator, EncodeTypes, and generation parameters to create barcodes. Typical use cases include producing shipping labels, tracking numbers, and postal codes where strict data validation is required. Developers often need to handle invalid input gracefully, log warnings, and continue processing remaining items.
// Prompt: Handle cases where customer information uses unsupported characters by logging a warning and skipping generation.
// Tags: australiapost, barcode generation, png, barcodegenerator, encodetypes, parameters

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates Australia Post barcodes for a set of sample code texts,
/// validates the content, and skips any codes containing unsupported characters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Prepares the output folder, iterates over sample code texts,
    /// attempts barcode generation with strict validation, and logs warnings for invalid inputs.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for generated barcode images
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Sample Australia Post code texts (some contain unsupported characters)
        var codeTexts = new[]
        {
            "1100000000",            // valid, no customer info
            "5980123456AB",          // valid CTable (letters allowed)
            "5980123456A$",          // invalid for CTable (symbol $)
            "628012345612345",       // valid NTable (digits only)
            "6280123456123A",        // invalid for NTable (letter A)
            "9280123456ABCD",        // valid CTable (up to 5 letters)
            "9280123456ABCD$",       // invalid for CTable (symbol $)
        };

        // Process each code text individually
        foreach (var codeText in codeTexts)
        {
            try
            {
                // Initialize the barcode generator with Australia Post symbology
                using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
                {
                    // Set the encoding table to CTable; invalid characters will trigger an exception
                    generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;

                    // Enable strict validation to throw on incorrect code text
                    generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

                    // Define the output file path (PNG format)
                    string filePath = Path.Combine(outputDir, $"{codeText}.png");

                    // Save the generated barcode image
                    generator.Save(filePath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated barcode: {filePath}");
                }
            }
            catch (Exception ex)
            {
                // Log a warning and skip the current code text if validation fails
                Console.WriteLine($"Warning: Skipping barcode for '{codeText}'. Reason: {ex.Message}");
            }
        }

        Console.WriteLine("Processing completed.");
    }
}