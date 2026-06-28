using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode with custom human‑readable text settings
/// and saving it as a PNG file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, customizes its text appearance, saves the image, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the file path where the generated barcode image will be saved.
        string outputPath = "custom_text_barcode.png";

        // Initialize a BarcodeGenerator for Code128 symbology with the data "1234567890".
        // The generator is wrapped in a using statement to ensure proper disposal.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Configure the font of the human‑readable text displayed below the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Verdana"; // Font family
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 10f;      // Font size in points

            // Align the human‑readable text to the left side of the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Left;

            // Save the generated barcode image to the specified path in PNG format.
            generator.Save(outputPath);
        }

        // Output the location of the saved barcode image to the console.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}