// Title: Independent Top and Bottom Caption Colors in a Barcode Image
// Description: Demonstrates how to set different text colors for the top and bottom captions of a Code128 barcode and save it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator and its Parameters to customize caption appearance. Developers often need to adjust caption visibility, text, font, and color independently for branding or readability purposes. The snippet highlights typical API classes like BarcodeGenerator, EncodeTypes, BarCodeImageFormat, and caption parameter objects, useful for creating customized barcode images in .NET applications.
// Prompt: Change the caption color independently for top and bottom captions in the same barcode image.
// Tags: code128, caption, color, png, barcodegenerator, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode with separate colors for top and bottom captions and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates an output folder, configures barcode captions, and saves the image.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary output folder for the generated barcode image
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Define the full file path for the resulting PNG image
        string outputPath = Path.Combine(outputFolder, "BarcodeWithCaptions.png");

        // Initialize the barcode generator with Code128 symbology and the desired data string
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Configure the top caption: make it visible, set text, color, and font size
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Top Caption";
            generator.Parameters.CaptionAbove.TextColor = Color.Green;
            generator.Parameters.CaptionAbove.Font.Size.Point = 12f;

            // Configure the bottom caption: make it visible, set text, color, and font size
            generator.Parameters.CaptionBelow.Visible = true;
            generator.Parameters.CaptionBelow.Text = "Bottom Caption";
            generator.Parameters.CaptionBelow.TextColor = Color.Blue;
            generator.Parameters.CaptionBelow.Font.Size.Point = 12f;

            // Save the generated barcode with captions to a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image
        Console.WriteLine("Barcode image saved to: " + outputPath);
    }
}