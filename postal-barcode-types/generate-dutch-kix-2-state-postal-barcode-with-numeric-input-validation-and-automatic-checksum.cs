// Title: Generate Dutch KIX 2‑state Postal Barcode with Validation and Checksum
// Description: Demonstrates creating a Dutch KIX (2‑state postal) barcode from numeric input, validating the data and enabling automatic checksum calculation.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to select a specific symbology (EncodeTypes.DutchKIX), configure checksum options, and output the result as an image file. Developers working with postal barcodes often need to validate numeric data, enable checksum generation, and produce printable graphics; this snippet shows the typical API usage for those scenarios.
// Prompt: Generate a Dutch KIX 2‑state postal barcode with numeric input validation and automatic checksum.
// Tags: barcode, generation, dutch kix, checksum, validation, image, aspose.barcode

using System;
using System.IO;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Dutch KIX 2‑state postal barcode with input validation and automatic checksum.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode and writes status messages to the console.
    /// </summary>
    static void Main()
    {
        // Sample numeric data for Dutch KIX barcode
        string input = "1234567890123";
        string outputPath = "dutchkix.png";

        try
        {
            // Generate the barcode and save it to the specified file
            GenerateDutchKix(input, outputPath);
            Console.WriteLine($"Dutch KIX barcode saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during generation
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a Dutch KIX barcode from numeric data, validates the input, enables checksum, and saves the image.
    /// </summary>
    /// <param name="numericData">The numeric string to encode.</param>
    /// <param name="filePath">The full path where the barcode image will be saved.</param>
    static void GenerateDutchKix(string numericData, string filePath)
    {
        // Validate input: must be non‑empty and contain only digits
        if (string.IsNullOrEmpty(numericData))
            throw new ArgumentException("Input cannot be null or empty.", nameof(numericData));

        if (!numericData.All(char.IsDigit))
            throw new ArgumentException("Input must contain only numeric characters.", nameof(numericData));

        // Ensure the output directory exists
        string directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        // Create the barcode generator for Dutch KIX (2‑state postal) symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.DutchKIX, numericData))
        {
            // Enable automatic checksum generation
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            // Optionally display the checksum in the human‑readable text
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;

            // Save the barcode image (format inferred from file extension)
            generator.Save(filePath);
        }
    }
}