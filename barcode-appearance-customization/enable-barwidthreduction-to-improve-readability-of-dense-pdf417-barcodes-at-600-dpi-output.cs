using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a dense PDF417 barcode and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a PDF417 barcode with high resolution
    /// and bar width reduction, then saves it to a file.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the PDF417 barcode.
        string codeText = "This is a sample PDF417 barcode with a relatively long text to make it dense and test bar width reduction.";
        // Define the output file path for the generated PNG image.
        string outputPath = "pdf417.png";

        // Initialize the barcode generator for PDF417 with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, codeText))
        {
            // Set a high resolution (600 DPI) to improve image quality.
            generator.Parameters.Resolution = 600f;

            // Reduce bar width slightly to compensate for ink spread in dense barcodes.
            generator.Parameters.Barcode.BarWidthReduction.Point = 0.1f;

            // Save the generated barcode as a PNG file at the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user that the barcode has been saved.
        Console.WriteLine($"PDF417 barcode saved to {outputPath}");
    }
}