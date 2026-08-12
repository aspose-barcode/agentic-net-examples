// Title: Set DotCode error correction level and redundancy
// Description: Demonstrates configuring DotCode barcode generation with extended encode mode, increased columns, and UTF-8 ECI to improve data integrity.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to adjust DotCode parameters such as EncodeMode, Columns, and ECIEncoding for enhanced error correction and robustness. Developers working with 2D barcodes can use these settings to meet reliability requirements in applications like inventory tracking, product authentication, and data archiving.
// Prompt: Provide configuration to set DotCode error correction level for improved data integrity.
// Tags: dotcode, error-correction, png, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a DotCode barcode with settings that increase error correction and data robustness.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates and saves a DotCode barcode image.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary output directory for the generated image
        string outputDir = Path.Combine(Path.GetTempPath(), "DotCodeExample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the data to encode in the barcode
        string codeText = "SampleDataForDotCode";

        // Initialize the barcode generator for DotCode symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, codeText))
        {
            // Configure extended encode mode to provide higher data robustness
            generator.Parameters.Barcode.DotCode.EncodeMode = DotCodeEncodeMode.Extended;

            // Increase the number of columns to add redundancy (more modules)
            generator.Parameters.Barcode.DotCode.Columns = 30;

            // Optional: specify UTF-8 ECI encoding for better character handling
            generator.Parameters.Barcode.DotCode.ECIEncoding = ECIEncodings.UTF8;

            // Define the full path for the output PNG image
            string outputPath = Path.Combine(outputDir, "dotcode.png");

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Inform the user where the barcode image was saved
            Console.WriteLine($"DotCode barcode saved to: {outputPath}");
        }
    }
}