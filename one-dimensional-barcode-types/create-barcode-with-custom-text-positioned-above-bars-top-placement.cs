using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with a caption above the barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode image and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "barcode.png";

        // Initialize a BarcodeGenerator for Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the data to be encoded in the barcode.
            generator.CodeText = "1234567890";

            // Enable and configure the caption that appears above the barcode.
            generator.Parameters.CaptionAbove.Visible = true;                     // Show the caption.
            generator.Parameters.CaptionAbove.Text = "Custom Text Above";        // Caption text.
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center; // Center align the caption.
            generator.Parameters.CaptionAbove.Font.FamilyName = "Arial";         // Font family.
            generator.Parameters.CaptionAbove.Font.Size.Point = 12f;            // Font size in points.
            generator.Parameters.CaptionAbove.TextColor = Color.Black;          // Caption text color.

            // Save the generated barcode image to the specified file.
            generator.Save(outputPath);
        }

        // Inform the user that the barcode has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}