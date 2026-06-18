using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode using Aspose.BarCode and saving it as a TIFF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Code128 barcode with custom padding and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Sample code text for the Code128 barcode.
        string codeText = "1234567890";

        // Create a barcode generator for Code128 with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Enable checksum (mandatory for Code128, but set explicitly for clarity).
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Configure custom margins (padding) in points for each side of the barcode.
            generator.Parameters.Barcode.Padding.Left.Point   = 10f;
            generator.Parameters.Barcode.Padding.Top.Point    = 10f;
            generator.Parameters.Barcode.Padding.Right.Point  = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Define the output file path and save the barcode as a TIFF image.
            // TIFF uses lossless compression by default.
            string outputPath = "code128.tiff";
            generator.Save(outputPath, BarCodeImageFormat.Tiff);

            // Inform the user that the barcode has been saved.
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}