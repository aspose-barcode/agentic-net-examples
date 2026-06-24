using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode and saving it as an image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file name for the generated barcode image
        const string outputPath = "barcode.png";

        // Create a BarcodeGenerator for Code128 with the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Enable automatic font size calculation for the human‑readable text
            generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Auto;

            // Save the generated barcode image to the specified path
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}