// Title: 2‑State Planet Barcode Generation with Numeric Validation
// Description: Demonstrates generating a Planet (2‑state) barcode while enforcing numeric-only input, raising an exception for invalid characters.
// Category-Description: Shows how to use Aspose.BarCode's BarcodeGenerator for 2‑state symbologies, configuring the ThrowExceptionWhenCodeTextIncorrect parameter to validate code text. Typical use cases include generating Planet barcodes for inventory or tracking where only numeric data is allowed. Developers often need to catch validation errors to ensure data integrity before saving barcode images.
// Prompt: Validate numeric input for a 2‑state barcode generator and raise an exception for non‑numeric characters.
// Tags: planet barcode,2-state symbology,validation,exception handling,aspose.barcode,generation,png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates numeric validation for a 2‑state Planet barcode generator using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes for valid and invalid inputs, showing exception handling.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary output directory for the generated barcode images.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        Console.WriteLine($"Output directory: {outputDir}");

        // Define test cases: one with a valid numeric code and one with an invalid alphanumeric code.
        var testCases = new[]
        {
            new { Name = "Valid", Code = "123456" },
            new { Name = "Invalid", Code = "12AB34" }
        };

        // Process each test case.
        foreach (var test in testCases)
        {
            // Determine the file path for the generated barcode image.
            string filePath = Path.Combine(outputDir, $"{test.Name}_Planet.png");
            try
            {
                // Initialize the barcode generator for the Planet (2‑state) symbology with the provided code.
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Planet, test.Code))
                {
                    // Enable strict validation: an exception is thrown if the code contains non‑numeric characters.
                    generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

                    // Generate the barcode image.
                    using (Bitmap bitmap = generator.GenerateBarCodeImage())
                    {
                        // Save the image as PNG.
                        bitmap.Save(filePath, ImageFormat.Png);
                    }
                }
                Console.WriteLine($"{test.Name} barcode generated successfully: {filePath}");
            }
            catch (Exception ex)
            {
                // Output the validation error for the invalid test case.
                Console.WriteLine($"{test.Name} barcode generation failed: {ex.Message}");
            }
        }
    }
}