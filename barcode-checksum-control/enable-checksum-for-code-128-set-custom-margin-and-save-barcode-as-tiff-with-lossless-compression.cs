// Title: Code128 Barcode with Checksum, Custom Margin, and TIFF Output
// Description: Generates a Code 128 barcode, enables its checksum, applies a uniform margin, and saves the image as a losslessly compressed TIFF file.
// Prompt: Enable checksum for Code 128, set custom margin, and save the barcode as TIFF with lossless compression.
// Tags: code128, checksum, margin, tiff, aspose.barcode, barcode generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to generate a Code 128 barcode with checksum enabled,
/// apply custom margins, and save the result as a losslessly compressed TIFF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures its parameters,
    /// saves it to disk, and writes a confirmation message to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated TIFF image
        string outputPath = "code128.tif";

        // Initialize a Code128 barcode generator with sample text "1234567890"
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Enable checksum (required for Code128 to ensure data integrity)
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Set a uniform custom margin of 10 points on all sides
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 10f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Save the barcode as a TIFF image using lossless compression
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }

        // Inform the user that the barcode has been saved successfully
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}