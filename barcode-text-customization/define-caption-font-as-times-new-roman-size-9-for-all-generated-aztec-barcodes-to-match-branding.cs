using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating an Aztec barcode with custom caption fonts using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates an Aztec barcode, configures caption fonts,
    /// and saves the result to a PNG file.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Aztec type with the provided data string.
        using (var generator = new BarcodeGenerator(EncodeTypes.Aztec, "Sample Aztec"))
        {
            // Configure the font for the caption displayed above the barcode.
            generator.Parameters.CaptionAbove.Font.FamilyName = "Times New Roman";
            generator.Parameters.CaptionAbove.Font.Size.Point = 9f;

            // Configure the font for the caption displayed below the barcode.
            generator.Parameters.CaptionBelow.Font.FamilyName = "Times New Roman";
            generator.Parameters.CaptionBelow.Font.Size.Point = 9f;

            // Set the text for the above caption and make it visible.
            generator.Parameters.CaptionAbove.Text = "Aztec Barcode";
            generator.Parameters.CaptionAbove.Visible = true;

            // Hide the below caption (optional demonstration).
            generator.Parameters.CaptionBelow.Visible = false;

            // Render and save the barcode image as a PNG file.
            generator.Save("aztec.png");
        }

        // Inform the user that the barcode has been generated.
        Console.WriteLine("Aztec barcode generated with caption font Times New Roman, size 9.");
    }
}