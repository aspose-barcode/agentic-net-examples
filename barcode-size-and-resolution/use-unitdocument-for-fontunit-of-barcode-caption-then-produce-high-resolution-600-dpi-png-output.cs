// Title: Generate 600 dpi Code128 barcode with caption using Document font unit
// Description: Demonstrates creating a Code128 barcode, setting a 600 dpi resolution, and adding a caption whose font size is specified in Document units, then saving as a high‑resolution PNG.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to configure barcode parameters such as resolution, caption visibility, and font measurement units. It uses the BarcodeGenerator class together with EncodeTypes, BarCodeImageFormat, and the FontUnit properties to produce high‑quality barcode images. Developers often need to generate barcodes for print media where DPI and precise font sizing are critical.
// Prompt: Use Unit.Document for FontUnit of barcode caption, then produce high‑resolution 600 dpi PNG output.
// Tags: code128, barcode, high-resolution, png, caption, fontunit, document, resolution, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode with a caption,
/// sets the image resolution to 600 dpi, and saves the result as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode_600dpi.png");

        // Create a BarcodeGenerator for Code128 with the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the image resolution to 600 dpi for high‑quality output.
            generator.Parameters.Resolution = 600f;

            // Enable and configure the caption that appears above the barcode.
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Sample Caption";

            // Specify the font size using Document units (12 points in this case).
            generator.Parameters.CaptionAbove.Font.Size.Document = 12f; // 12 Document units

            // Save the generated barcode as a PNG image with the specified resolution.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}