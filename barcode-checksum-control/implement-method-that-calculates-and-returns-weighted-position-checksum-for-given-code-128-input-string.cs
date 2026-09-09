// Title: Code 128 Weighted‑Position Checksum Calculation and Barcode Generation
// Description: Demonstrates how to compute the weighted‑position checksum for a Code 128 string and generate a barcode image with the checksum displayed.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating checksum handling for Code 128 symbology. It uses the BarcodeGenerator class with EncodeTypes.Code128 and shows how to enable checksum display via Parameters.Barcode.ChecksumAlwaysShow. Developers working with barcode creation often need to validate data integrity and render barcodes with explicit checksum symbols, making this pattern useful for inventory, shipping, and labeling applications.
// Prompt: Implement a method that calculates and returns the weighted‑position checksum for a given Code 128 input string.
// Tags: code128, checksum, barcode, generation, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides functionality to compute a Code 128 weighted‑position checksum and generate a barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Calculates the weighted‑position checksum for a Code 128 string using Code Set B.
    /// </summary>
    /// <param name="text">The input string to checksum (must be valid for Code Set B).</param>
    /// <returns>The checksum value (0‑102) as defined by the Code 128 specification.</returns>
    static int ComputeCode128Checksum(string text)
    {
        // Start Code B value (per Code 128 specification)
        const int startCodeB = 104;
        int checksum = startCodeB;

        // Iterate over each character, applying the weighted position factor (i + 1)
        for (int i = 0; i < text.Length; i++)
        {
            // Convert character to Code Set B value (ASCII 32–127 maps to 0–95)
            int charValue = text[i] - 32;

            // Validate that the character is within the allowed range for Set B
            if (charValue < 0 || charValue > 95)
                throw new ArgumentException($"Character '{text[i]}' at position {i} is not valid for Code128 Set B.");

            // Add weighted value to checksum
            checksum += charValue * (i + 1);
        }

        // Reduce checksum modulo 103 as required by the specification
        checksum %= 103;
        return checksum;
    }

    /// <summary>
    /// Entry point of the example. Computes the checksum for a sample string, displays it,
    /// generates a Code 128 barcode with the checksum shown, and saves the image to a temporary file.
    /// </summary>
    static void Main()
    {
        // Sample text to encode and checksum
        string sampleText = "Aspose1234";

        // Compute and display the weighted‑position checksum
        int checksum = ComputeCode128Checksum(sampleText);
        Console.WriteLine($"Weighted‑position checksum for \"{sampleText}\" is {checksum}");

        // Define output path for the generated barcode image
        string outputPath = Path.Combine(Path.GetTempPath(), "Code128_WithChecksum.png");

        // Create a barcode generator for Code 128 and enable explicit checksum display
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, sampleText))
        {
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}