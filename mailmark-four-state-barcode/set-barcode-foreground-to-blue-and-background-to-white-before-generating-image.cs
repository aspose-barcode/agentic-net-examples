// Title: Generate Code128 barcode with custom colors
// Description: Demonstrates setting the barcode foreground to blue and background to white before saving as an image.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to customize visual appearance using BarcodeGenerator, EncodeTypes, and color properties. Developers often need to adjust bar and background colors for branding or readability when creating PNG, JPEG, or PDF barcode outputs.
// Prompt: Set barcode foreground to blue and background to white before generating the image.
// Tags: code128, color, foreground, background, png, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode with custom foreground and background colors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode image and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for Code128 symbology with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the barcode bars (foreground) to blue.
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the image background to white.
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode as a PNG file.
            generator.Save("barcode.png");
        }

        // Inform the user that the image has been saved.
        Console.WriteLine("Barcode image saved as barcode.png");
    }
}