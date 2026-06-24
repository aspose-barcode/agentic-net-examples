using System;
using System.IO;
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
        // Determine the full path for the output PNG file in the current directory.
        string outputPath = Path.Combine(Environment.CurrentDirectory, "barcode.png");

        // Initialize a BarcodeGenerator for Code128 with the specified code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Suppress the default human‑readable code text beneath the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Configure a caption that appears above the barcode.
            generator.Parameters.CaptionAbove.Text = "Order #123456"; // Caption content
            generator.Parameters.CaptionAbove.Visible = true;        // Make the caption visible
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center; // Center align
            generator.Parameters.CaptionAbove.Font.FamilyName = "Arial";        // Font family
            generator.Parameters.CaptionAbove.Font.Size.Point = 12f;           // Font size
            generator.Parameters.CaptionAbove.TextColor = Color.Black;        // Font color

            // Optional: set explicit barcode dimensions instead of auto‑sizing.
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;            // Disable auto size
            generator.Parameters.Barcode.BarHeight.Point = 50f;              // Height of bars
            generator.Parameters.Barcode.XDimension.Point = 2f;              // Width of smallest bar

            // Save the generated barcode image to the specified file path.
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}