// Title: Generate GS1 Code 128 barcode with custom caption and save as JPEG
// Description: Demonstrates creating a GS1 Code 128 barcode, adding a custom human‑readable caption below, and exporting the image as a JPEG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use BarcodeGenerator with EncodeTypes.GS1Code128, configure visual parameters such as X‑dimension, code‑text location, caption, and background color, and save the result in a raster image format. Developers often need to produce GS1‑compliant barcodes for product identification and include additional readable text for labeling purposes.
// Prompt: Generate a GS1 Code 128 barcode, embed custom human‑readable text below, and save as JPEG.
// Tags: gs1,code128,barcode,generation,caption,jpeg,aspose.barcode,aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a GS1 Code 128 barcode with a custom caption and saves it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode, configures visual settings, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Determine the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "GS1Code128.jpg");

        // GS1 Code 128 data (example with Application Identifier (01) GTIN‑14).
        string gs1CodeText = "(01)01234567890128";

        // Initialize the barcode generator for GS1 Code 128 with the specified data.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, gs1CodeText))
        {
            // Set the module (X‑dimension) size to 2 pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Position the standard human‑readable code text below the barcode and set its font size.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Add a custom caption below the barcode and define its font size.
            generator.Parameters.CaptionBelow.Text = "Custom Human‑Readable Text";
            generator.Parameters.CaptionBelow.Font.Size.Point = 12f;

            // Optional: set the background color to white for better contrast.
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode image as a JPEG file.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}