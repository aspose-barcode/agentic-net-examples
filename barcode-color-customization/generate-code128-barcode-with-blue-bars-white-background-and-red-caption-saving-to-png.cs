// Title: Generate Code128 barcode with custom colors and caption
// Description: Demonstrates creating a Code128 barcode with blue bars, white background, and a red caption, then saving it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to customize barcode appearance using the BarcodeGenerator class. It covers setting bar colors, background, and caption properties—common tasks for developers needing branded or readable barcodes in images, PDFs, or reports.
// Prompt: Generate a Code128 barcode with blue bars, white background, and red caption, saving to PNG.
// Tags: barcode symbology, generation, png, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode with customized colors and a caption,
/// then saves the result as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode and writes a confirmation message to the console.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for Code128 with the sample text "1234567890"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the color of the barcode bars to blue
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the image background color to white
            generator.Parameters.BackColor = Color.White;

            // Enable and configure a caption displayed above the barcode
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Code128 Barcode";
            generator.Parameters.CaptionAbove.TextColor = Color.Red;
            generator.Parameters.CaptionAbove.Font.Size.Point = 12f; // Font size in points
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;

            // Save the generated barcode image as a PNG file
            generator.Save("code128.png");
        }

        // Inform the user that the barcode has been generated
        Console.WriteLine("Barcode generated and saved as code128.png");
    }
}