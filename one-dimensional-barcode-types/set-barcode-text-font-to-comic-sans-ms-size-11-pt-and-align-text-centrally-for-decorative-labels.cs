// Title: Generate a Code128 barcode with custom text font and centered alignment
// Description: Demonstrates how to set the barcode text font to Comic Sans MS, size 11 pt, and center the text for decorative label images.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode appearance using the BarcodeGenerator class. It covers setting font properties, text alignment, and saving the result as an image—common tasks for developers creating branded or decorative labels with barcodes.
// Prompt: Set barcode text font to Comic Sans MS, size 11 pt, and align text centrally for decorative labels.
// Tags: code128, barcode generation, text font, alignment, png, aspose.barcode, aspnet

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode, applies a custom font,
/// centers the text, and saves the result as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the barcode, configures text appearance, and writes the image to a temporary file.
    /// </summary>
    static void Main()
    {
        // Determine a temporary file path for the output PNG image.
        string outputPath = Path.Combine(Path.GetTempPath(), "decorative_label.png");

        // Create a BarcodeGenerator for Code128 with the sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample"))
        {
            // Set the barcode text font to Comic Sans MS, size 11 pt.
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Comic Sans MS";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 11f;

            // Align the barcode text centrally beneath the bars.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Save the generated barcode image as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}