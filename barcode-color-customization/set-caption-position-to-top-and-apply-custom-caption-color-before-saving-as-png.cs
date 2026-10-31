// Title: Set Caption Position to Top with Custom Color and Save as PNG
// Description: Demonstrates how to place a caption above a Code128 barcode, customize its color, and save the result as a PNG image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to configure barcode appearance with the BarcodeGenerator class. It covers caption positioning, font styling, and image export—common tasks for developers creating printable or on‑screen barcodes with custom annotations.
// Prompt: Set the caption position to top and apply a custom caption color before saving as PNG.
// Tags: barcode, caption, png, aspose.barcode, code128, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode with a top caption,
/// applies a custom caption color, and saves the image as PNG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated PNG file.
        string outPath = Path.Combine(outputDir, "barcode_with_caption.png");

        // Initialize the barcode generator for Code128 with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Enable the caption above the barcode and set its properties.
            generator.Parameters.CaptionAbove.Visible = true;               // Show caption above the barcode.
            generator.Parameters.CaptionAbove.Text = "Top Caption";        // Caption text.
            generator.Parameters.CaptionAbove.TextColor = Color.Blue;      // Custom caption color.
            generator.Parameters.CaptionAbove.Font.Size.Point = 12f;       // Font size.

            // Save the barcode image as PNG.
            generator.Save(outPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"Barcode saved to {outPath}");
    }
}