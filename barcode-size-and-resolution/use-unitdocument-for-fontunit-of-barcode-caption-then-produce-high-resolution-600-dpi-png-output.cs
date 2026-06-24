using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with a caption using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode image and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file name for the generated barcode image.
        const string outputPath = "barcode.png";

        // Create a BarcodeGenerator for Code128 symbology with the specified data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the image resolution to 600 DPI.
            generator.Parameters.Resolution = 600f;

            // Configure the caption that appears above the barcode.
            generator.Parameters.CaptionAbove.Text = "Sample Caption";          // Caption text
            generator.Parameters.CaptionAbove.Font.FamilyName = "Arial";       // Font family
            generator.Parameters.CaptionAbove.Font.Size.Point = 12f;           // Font size in points
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center; // Center alignment

            // Save the generated barcode image to the specified path.
            generator.Save(outputPath);
        }
    }
}