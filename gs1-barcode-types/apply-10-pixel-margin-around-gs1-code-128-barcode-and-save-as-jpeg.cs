using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a GS1 Code 128 barcode with a margin and saving it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode and writes it to a file.
    /// </summary>
    static void Main()
    {
        // Define the barcode text. For GS1 Code 128 the AI must be enclosed in parentheses.
        string codeText = "(01)12345678901231";

        // Destination file path for the generated image.
        string outputPath = "gs1code128_margin.jpg";

        // Initialize the barcode generator with the desired symbology and text.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
        {
            // Set a uniform 10‑pixel padding (margin) on all four sides of the barcode.
            generator.Parameters.Barcode.Padding.Left.Pixels   = 10f;
            generator.Parameters.Barcode.Padding.Top.Pixels    = 10f;
            generator.Parameters.Barcode.Padding.Right.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = 10f;

            // Render and save the barcode as a JPEG image.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}