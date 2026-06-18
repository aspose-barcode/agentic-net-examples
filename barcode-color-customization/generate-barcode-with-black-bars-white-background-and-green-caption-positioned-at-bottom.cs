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
        // Define the file path where the generated barcode image will be saved.
        string outputPath = "barcode.png";

        // Initialize a BarcodeGenerator for Code128 format with the specified code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the color of the barcode bars to black.
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Set the background color of the image to white.
            generator.Parameters.BackColor = Color.White;

            // Configure a caption that appears below the barcode.
            generator.Parameters.CaptionBelow.Visible = true;                     // Make the caption visible.
            generator.Parameters.CaptionBelow.Text = "Sample Caption";           // Set caption text.
            generator.Parameters.CaptionBelow.Font.FamilyName = "Arial";         // Choose font family.
            generator.Parameters.CaptionBelow.Font.Size.Point = 12f;             // Set font size.
            generator.Parameters.CaptionBelow.TextColor = Color.Green;          // Set caption text color.
            generator.Parameters.CaptionBelow.Alignment = TextAlignment.Center; // Center-align the caption.

            // Save the generated barcode image to the specified file path.
            generator.Save(outputPath);
        }

        // Inform the user that the barcode has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}