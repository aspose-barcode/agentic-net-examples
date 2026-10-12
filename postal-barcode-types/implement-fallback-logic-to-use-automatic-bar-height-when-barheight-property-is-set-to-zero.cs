// Title: Automatic barcode height fallback example
// Description: Demonstrates how to let Aspose.BarCode automatically determine bar height when BarHeight is set to zero.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and barcode parameters such as XDimension and BarHeight. Developers often need to generate barcodes with dynamic sizing, where automatic height adjustment simplifies layout handling across different output formats.
// Prompt: Implement fallback logic to use automatic bar height when BarHeight property is set to zero.
// Tags: barcode, code128, autoheight, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates automatic bar height fallback when generating a barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode with optional manual height; zero triggers automatic height.
    /// </summary>
    static void Main()
    {
        // Define sample input data
        string codeText = "ASPOSE";

        // Set BarHeight to zero to let the library calculate it automatically
        float barHeight = 0f;

        // Determine a temporary output file path
        string outputPath = Path.Combine(Path.GetTempPath(), "AutoHeightBarcode.png");

        // Generate the barcode image
        GenerateBarcode(codeText, barHeight, outputPath);

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }

    /// <summary>
    /// Generates a barcode image using the specified text and optional bar height.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="barHeight">Desired bar height in pixels; zero enables automatic height.</param>
    /// <param name="outputFile">Full path to save the generated image.</param>
    static void GenerateBarcode(string codeText, float barHeight, string outputFile)
    {
        // Use Code128 symbology for the example
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set a reasonable X-dimension (module width) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Apply BarHeight only if a positive value is provided; otherwise, automatic height is used
            if (barHeight > 0f)
            {
                generator.Parameters.Barcode.BarHeight.Pixels = barHeight;
            }

            // Ensure the output directory exists before saving
            string dir = Path.GetDirectoryName(outputFile);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            // Save the barcode as a PNG image
            generator.Save(outputFile, BarCodeImageFormat.Png);
        }
    }
}