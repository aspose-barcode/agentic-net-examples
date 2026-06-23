using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with a custom font and saving it as an SVG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, applies custom font settings,
    /// and saves the result to an SVG file while handling potential exceptions.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated SVG barcode.
        string outputPath = "custom_font_barcode.svg";

        // Initialize a BarcodeGenerator for Code128 with the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the human‑readable text (code text) font family to Arial.
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";

            // Set the font size to 12 points.
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Attempt to save the barcode as an SVG file.
            // The try‑catch block handles any exceptions, such as those caused by evaluation‑license restrictions.
            try
            {
                generator.Save(outputPath, BarCodeImageFormat.Svg);
                Console.WriteLine($"Barcode saved to {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save SVG: {ex.Message}");
            }
        }
    }
}