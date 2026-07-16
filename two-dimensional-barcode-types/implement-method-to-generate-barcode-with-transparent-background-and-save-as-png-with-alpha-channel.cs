// Title: Generate Code128 barcode with transparent background and save as PNG
// Description: Demonstrates creating a Code128 barcode with a transparent background and saving it as a PNG image that retains the alpha channel.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode appearance using the BarcodeGenerator class. It shows setting the background color to transparent and exporting to PNG, a common requirement for overlaying barcodes on UI elements or documents without obscuring underlying graphics. Developers often need to customize colors, formats, and image properties when integrating barcodes into applications.
// Prompt: Implement method to generate barcode with transparent background and save as PNG with alpha channel.
// Tags: code128, generate, png, transparent, background, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode with a transparent background
/// and saves it as a PNG file preserving the alpha channel.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        const string outputPath = "transparent_barcode.png";

        // Initialize the barcode generator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Configure the barcode to have a transparent background.
            generator.Parameters.BackColor = Color.Transparent;

            // Save the barcode as a PNG file; PNG format retains the alpha channel.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user that the barcode has been saved successfully.
        Console.WriteLine($"Barcode saved to '{outputPath}' with transparent background.");
    }
}