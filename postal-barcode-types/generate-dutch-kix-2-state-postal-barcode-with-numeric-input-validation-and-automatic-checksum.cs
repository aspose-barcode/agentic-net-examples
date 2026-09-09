// Title: Generate Dutch KIX 2‑state Postal Barcode
// Description: Demonstrates how to create a Dutch KIX 2‑state postal barcode from a numeric string, including input validation and automatic checksum handling.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator with EncodeTypes.DutchKIX. It illustrates typical tasks such as validating numeric input, configuring visual parameters, and saving the result as an image. Developers working with postal barcodes, especially Dutch KIX, can use this pattern for automated label creation.
// Prompt: Generate a Dutch KIX 2‑state postal barcode with numeric input validation and automatic checksum.
// Tags: dutch kix, barcode generation, numeric validation, png, aspose.barcode, generation

using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a Dutch KIX 2‑state postal barcode with numeric validation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates and saves a Dutch KIX barcode image.
    /// </summary>
    static void Main()
    {
        // Sample numeric input for the barcode
        string codeText = "123456";

        // Validate that the input contains only digits
        ValidateNumeric(codeText);

        // Prepare the output directory and file path
        string outputDir = Path.Combine(Path.GetTempPath(), "DutchKIXDemo");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "DutchKIX.png");

        // Generate Dutch KIX barcode using Aspose.BarCode
        using (var generator = new BarcodeGenerator(EncodeTypes.DutchKIX, codeText))
        {
            // Optional visual settings: set module size and bar height
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Parameters.Barcode.BarHeight.Pixels = 50;

            // Save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Dutch KIX barcode saved to: {outputPath}");
    }

    /// <summary>
    /// Ensures the provided text consists solely of numeric characters.
    /// </summary>
    /// <param name="text">The string to validate.</param>
    static void ValidateNumeric(string text)
    {
        if (string.IsNullOrEmpty(text) || !Regex.IsMatch(text, @"^\d+$"))
        {
            throw new ArgumentException("Input must be a non-empty numeric string.");
        }
    }
}