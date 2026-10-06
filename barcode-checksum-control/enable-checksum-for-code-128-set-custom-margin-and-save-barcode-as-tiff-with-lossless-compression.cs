// Title: Generate Code 128 barcode with checksum, custom margins, and TIFF lossless output
// Description: Demonstrates enabling checksum for a Code 128 barcode, applying custom padding, and saving the result as a losslessly compressed TIFF image.
// Category-Description: This example belongs to the Aspose.BarCode generation series, illustrating how to configure barcode parameters such as checksum and padding using the BarcodeGenerator class. Typical use cases include creating high‑quality barcodes for printing or archival where precise layout and data integrity are required. Developers often need to adjust margins and select appropriate image formats for downstream processing.
// Prompt: Enable checksum for Code 128, set custom margin, and save the barcode as TIFF with lossless compression.
// Tags: code128, checksum, margin, tiff, lossless, aspose.barcode, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code 128 barcode with checksum enabled,
/// applies custom padding, and saves it as a losslessly compressed TIFF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define and create a temporary output directory for the generated barcode image.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Build the full file path for the TIFF output.
        string filePath = Path.Combine(outputDir, "code128.tiff");

        // Initialize the barcode generator with Code 128 symbology and the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Enable checksum calculation for the barcode.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Set custom padding (margin) on all sides in points.
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 10f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Save the barcode image as a TIFF file with lossless compression.
            generator.Save(filePath, BarCodeImageFormat.Tiff);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {filePath}");
    }
}