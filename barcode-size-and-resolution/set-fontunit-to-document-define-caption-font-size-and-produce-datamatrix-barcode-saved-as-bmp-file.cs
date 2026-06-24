using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a DataMatrix barcode with a caption using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a DataMatrix barcode, adds a caption,
    /// configures its appearance, saves it as a BMP file, and writes a confirmation to the console.
    /// </summary>
    static void Main()
    {
        // Create a BarcodeGenerator for DataMatrix type with the encoded value "ABC123"
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "ABC123"))
        {
            // Enable the caption displayed above the barcode
            generator.Parameters.CaptionAbove.Visible = true;

            // Set the caption text
            generator.Parameters.CaptionAbove.Text = "DataMatrix Example";

            // Configure caption font: Arial, 12 points
            generator.Parameters.CaptionAbove.Font.FamilyName = "Arial";
            generator.Parameters.CaptionAbove.Font.Size.Point = 12f; // Font size 12 points

            // Center the caption horizontally and set its color to black
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;
            generator.Parameters.CaptionAbove.TextColor = Color.Black;

            // Save the generated barcode image as a BMP file
            generator.Save("datamatrix.bmp");
        }

        // Inform the user that the barcode has been saved
        Console.WriteLine("DataMatrix barcode saved as datamatrix.bmp");
    }
}