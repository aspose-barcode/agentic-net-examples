// Title: Generate 600 DPI PNG Barcode with Caption Using Document Font Unit
// Description: This example creates a Code128 barcode, sets a caption with a document‑based font size, and saves the image as a high‑resolution 600 dpi PNG file.
// Category-Description: Demonstrates Aspose.BarCode generation features, focusing on barcode image resolution and caption styling. It uses the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce printable barcodes. Developers often need high‑resolution outputs for labels and packaging, and precise font sizing for captions, making this pattern common in inventory and logistics applications.
// Prompt: Use Unit.Document for FontUnit of barcode caption, then produce high‑resolution 600 dpi PNG output.
// Tags: barcode, code128, generation, png, 600dpi, caption, fontunit, document, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode with a caption using a document‑based font size
/// and saving it as a 600 dpi PNG image.
/// </summary>
public class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures caption and resolution,
    /// then writes the image to disk.
    /// </summary>
    public static void Main()
    {
        // Define the full path for the output PNG file in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode_600dpi.png");

        // Initialize the barcode generator with Code128 symbology and the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the image resolution to 600 DPI for high‑quality output.
            generator.Parameters.Resolution = 600f;

            // Enable the caption above the barcode and set its text.
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Sample Caption";

            // Configure the caption font: Helvetica family and size expressed in Document units.
            generator.Parameters.CaptionAbove.Font.FamilyName = "Helvetica";
            generator.Parameters.CaptionAbove.Font.Size.Document = 12f;

            // Save the generated barcode as a PNG image with the specified resolution.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}