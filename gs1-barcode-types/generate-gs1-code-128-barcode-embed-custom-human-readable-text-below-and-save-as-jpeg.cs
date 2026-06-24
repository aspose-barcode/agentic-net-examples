using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a GS1 Code128 barcode and saving it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Output file name for the generated barcode image
        const string outputPath = "gs1code128.jpg";

        // The data to encode; GS1 format with Application Identifier (01) for GTIN
        const string codeText = "(01)12345678901231";

        // Initialize a barcode generator for GS1 Code128 with the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
        {
            // Configure the human‑readable text to appear below the barcode graphic
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Persist the generated barcode to a JPEG file at the specified path
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}