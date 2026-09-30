// Title: Generate Code128 barcode with black bars, white background, and green bottom caption
// Description: Demonstrates how to create a Code128 barcode image using Aspose.BarCode, customize bar and background colors, and add a green caption positioned below the barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and image formatting classes. Typical use cases include creating printable barcodes with custom styling, adding descriptive captions, and exporting to common image formats. Developers often need to adjust colors, fonts, and caption placement to match branding or UI requirements.
// Prompt: Generate a barcode with black bars, white background, and green caption positioned at the bottom.
// Tags: code128, barcode, color, caption, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode with custom colors and a green caption placed below the barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates the barcode, applies styling, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Initialize the barcode generator for Code128 with the sample text "123456"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the barcode bars (foreground) to black
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Set the image background to white
            generator.Parameters.BackColor = Color.White;

            // Configure a caption that appears below the barcode
            generator.Parameters.CaptionBelow.Text = "Green Caption";
            generator.Parameters.CaptionBelow.TextColor = Color.Green;

            // Optional: adjust the caption font size
            generator.Parameters.CaptionBelow.Font.Size.Point = 12f;

            // Save the generated barcode image in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}