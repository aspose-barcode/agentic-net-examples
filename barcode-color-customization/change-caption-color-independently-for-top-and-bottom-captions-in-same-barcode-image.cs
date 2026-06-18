using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with top and bottom captions using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, configures captions, and saves the image.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for Code128 with the specified data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // ----- Configure the top caption (above the barcode) -----
            generator.Parameters.CaptionAbove.Visible = true;                     // Make the top caption visible
            generator.Parameters.CaptionAbove.Text = "Top Caption";              // Set caption text
            generator.Parameters.CaptionAbove.TextColor = Color.Red;             // Set caption color
            generator.Parameters.CaptionAbove.Font.FamilyName = "Arial";         // Set font family
            generator.Parameters.CaptionAbove.Font.Size.Point = 12f;             // Set font size (points)
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center; // Center-align the caption

            // ----- Configure the bottom caption (below the barcode) -----
            generator.Parameters.CaptionBelow.Visible = true;                     // Make the bottom caption visible
            generator.Parameters.CaptionBelow.Text = "Bottom Caption";           // Set caption text
            generator.Parameters.CaptionBelow.TextColor = Color.Blue;            // Set caption color
            generator.Parameters.CaptionBelow.Font.FamilyName = "Arial";         // Set font family
            generator.Parameters.CaptionBelow.Font.Size.Point = 12f;             // Set font size (points)
            generator.Parameters.CaptionBelow.Alignment = TextAlignment.Center; // Center-align the caption

            // Save the generated barcode image (including captions) to a file.
            string outputPath = "barcode.png";
            generator.Save(outputPath);
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}