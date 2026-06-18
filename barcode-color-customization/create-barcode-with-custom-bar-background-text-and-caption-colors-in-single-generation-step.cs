using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a custom barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Code128 barcode with custom colors and a caption,
    /// then saves it as a PNG file in the current directory.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output PNG file in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "custom_barcode.png");

        // Initialize a BarcodeGenerator for Code128 symbology with the sample text "123456".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // ----- Appearance settings -----
            // Set the color of the barcode bars.
            generator.Parameters.Barcode.BarColor = Color.Blue;
            // Set the background color of the image.
            generator.Parameters.BackColor = Color.Yellow;
            // Set the color of the human‑readable code text displayed below the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Color = Color.Green;

            // ----- Caption settings (above the barcode) -----
            // Make the caption visible.
            generator.Parameters.CaptionAbove.Visible = true;
            // Set the caption text.
            generator.Parameters.CaptionAbove.Text = "Sample Caption";
            // Set the caption text color.
            generator.Parameters.CaptionAbove.TextColor = Color.Red;

            // Save the generated barcode as a PNG file to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode generated and saved to: {outputPath}");
    }
}