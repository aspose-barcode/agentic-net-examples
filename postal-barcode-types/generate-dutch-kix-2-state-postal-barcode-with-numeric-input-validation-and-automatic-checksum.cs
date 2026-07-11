// Title: Generate Dutch KIX 2‑state postal barcode with checksum
// Description: Demonstrates creating a Dutch KIX barcode from numeric input, validating the data, and automatically adding the required checksum.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator with EncodeTypes.DutchKIX. It shows typical steps such as input validation, enabling checksum, setting colors, and saving the image. Developers working on postal barcode solutions often need these patterns for creating compliant KIX barcodes.
// Prompt: Generate a Dutch KIX 2‑state postal barcode with numeric input validation and automatic checksum.
// Tags: dutch kix, barcode generation, checksum, png, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Dutch KIX 2‑state postal barcode with numeric validation and automatic checksum.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Sample numeric input for Dutch KIX barcode.
        // In a real scenario this could come from arguments or another source.
        string input = "1234567890123";

        // Validate that the input consists only of digits and is not empty.
        if (string.IsNullOrEmpty(input) || !IsAllDigits(input))
        {
            throw new ArgumentException("Input must be a non‑empty numeric string.");
        }

        // Determine the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "kix.png");

        // Create the barcode generator for the Dutch KIX symbology with the validated input.
        using (var generator = new BarcodeGenerator(EncodeTypes.DutchKIX, input))
        {
            // Enable automatic checksum generation required by the KIX specification.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Optional visual settings: black bars on a white background.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the generated barcode image as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Dutch KIX barcode saved to: {outputPath}");
    }

    // Helper method to verify that a string contains only digit characters.
    static bool IsAllDigits(string s)
    {
        foreach (char c in s)
        {
            if (!char.IsDigit(c))
                return false;
        }
        return true;
    }
}