// Title: Generate Code 128 barcode with checksum, custom margins, and TIFF lossless output
// Description: Demonstrates enabling checksum for a Code 128 barcode, applying uniform padding, and saving the image as a lossless TIFF file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters such as checksum, padding, and image format using the BarcodeGenerator class. Typical use cases include creating high‑quality barcodes for printing or archival storage where lossless image formats are required. Developers often need to adjust margins and enable checksum to meet scanning standards and compliance.
// Prompt: Enable checksum for Code 128, set custom margin, and save the barcode as TIFF with lossless compression.
// Tags: code128, checksum, margin, tiff, lossless, aspose.barcode, barcode generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation with checksum, custom margins, and TIFF output.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a Code 128 barcode, configures its parameters, and saves it as a TIFF file.
    /// </summary>
    static void Main()
    {
        // Text to encode in the barcode
        string codeText = "1234567890";

        // Initialize the generator with Code128 symbology and the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Enable checksum calculation for the barcode
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Set uniform padding (margin) of 10 points on all sides
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 10f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Define output file path
            string outputPath = "code128.tiff";

            // Save the barcode as a lossless TIFF image
            generator.Save(outputPath, BarCodeImageFormat.Tiff);

            // Inform the user where the file was saved
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}