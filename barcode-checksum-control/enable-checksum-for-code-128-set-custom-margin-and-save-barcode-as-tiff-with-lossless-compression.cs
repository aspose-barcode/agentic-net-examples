// Title: Generate Code 128 barcode with checksum, custom margin, and lossless TIFF output
// Description: Demonstrates enabling checksum for Code 128, applying a uniform margin, and saving the result as a TIFF image using lossless compression.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters such as checksum, padding, and image format. It uses the BarcodeGenerator class together with EncodeTypes and BarCodeImageFormat to create high‑quality barcodes. Developers often need to customize appearance and output format for printing or archival purposes.
// Prompt: Enable checksum for Code 128, set custom margin, and save the barcode as TIFF with lossless compression.
// Tags: code128, checksum, margin, tiff, lossless, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to generate a Code 128 barcode with checksum enabled,
/// apply custom margins, and save the image as a lossless TIFF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures its parameters,
    /// and writes the output file to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path
        string outputPath = "code128.tif";

        // Ensure the output directory exists
        string directory = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Initialize the barcode generator for Code128 with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Enable checksum calculation for Code128
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Apply a uniform margin of 10 points on all sides
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 10f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Save the barcode as a lossless TIFF image
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to: {Path.GetFullPath(outputPath)}");
    }
}