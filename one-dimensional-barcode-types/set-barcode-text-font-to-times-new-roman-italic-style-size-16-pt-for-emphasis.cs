using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode using Aspose.BarCode and saving it as an image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode with specific font settings and saves it.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for Code128 with the text "Sample"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample"))
        {
            // Configure the human‑readable text font:
            // - Font family: Times New Roman
            // - Font size: 16 points
            // - Font style: Italic
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Times New Roman";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 16f;
            generator.Parameters.Barcode.CodeTextParameters.Font.Style = FontStyle.Italic;

            // Save the generated barcode image to a PNG file
            generator.Save("barcode.png");
        }

        // Inform the user that the barcode has been generated
        Console.WriteLine("Barcode generated and saved as barcode.png");
    }
}