using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode with custom text styling using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a barcode, configures its appearance, and saves it as an image file.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample text "DecorativeLabel"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "DecorativeLabel"))
        {
            // Configure the human‑readable text font to Comic Sans MS, 11 pt
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Comic Sans MS";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 11f;

            // Align the human‑readable text to the center of the barcode
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Save the generated barcode image to a PNG file
            generator.Save("decorative_label.png");
        }

        // Inform the user that the barcode image has been created
        Console.WriteLine("Barcode generated: decorative_label.png");
    }
}