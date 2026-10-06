// Title: DataMatrix barcode generation with exception handling for oversized CodeText
// Description: Demonstrates how to generate DataMatrix barcodes and catch exceptions when the provided CodeText exceeds the capacity of the selected symbol size.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on DataMatrix symbology. It showcases the use of BarcodeGenerator, EncodeTypes, BarCodeImageFormat, and DataMatrixVersion classes to create barcodes, adjust symbol versions, and handle errors when the input text is too long for a chosen symbol. Developers often need to validate CodeText length against symbol capacity and gracefully handle generation failures.
// Prompt: Handle exception when CodeText exceeds maximum capacity for the selected DataMatrix symbol size.
// Tags: datamatrix, barcode, exception handling, aspose.barcodes, generation, png, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates DataMatrix barcode generation with explicit exception handling
/// for cases where the CodeText exceeds the capacity of the selected symbol version.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates two DataMatrix barcodes using different
    /// symbol versions and captures any exceptions caused by oversized CodeText.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a temporary output directory for the generated images
        string outputDir = Path.Combine(Path.GetTempPath(), "DataMatrixDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define file paths for the small and large barcode images
        string smallVersionPath = Path.Combine(outputDir, "DataMatrixSmall.png");
        string largeVersionPath = Path.Combine(outputDir, "DataMatrixLarge.png");

        // Prepare a CodeText that is longer than what a small DataMatrix symbol can hold
        string longText = new string('A', 50);

        // ------------------------------------------------------------
        // Attempt generation with a small DataMatrix version (10x10)
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, longText))
        {
            // Set the symbol version to the smallest ECC200 size
            generator.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_10x10;
            // Enable throwing an exception when the CodeText does not fit the symbol
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

            try
            {
                // Try to save the barcode; this is expected to fail
                generator.Save(smallVersionPath, BarCodeImageFormat.Png);
                Console.WriteLine("Barcode generated with small version (unexpected).");
            }
            catch (Exception ex)
            {
                // Capture and display the exception message
                Console.WriteLine($"Exception for small version: {ex.Message}");
            }
        }

        // ------------------------------------------------------------
        // Attempt generation with a large DataMatrix version (144x144)
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, longText))
        {
            // Set the symbol version to a size that can accommodate the long text
            generator.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_144x144;
            // Keep exception throwing enabled for consistency
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

            try
            {
                // Save the barcode; this should succeed
                generator.Save(largeVersionPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode generated with large version at {largeVersionPath}");
            }
            catch (Exception ex)
            {
                // If an unexpected error occurs, display it
                Console.WriteLine($"Exception for large version: {ex.Message}");
            }
        }

        // Indicate that the process has completed
        Console.WriteLine("Done.");
    }
}