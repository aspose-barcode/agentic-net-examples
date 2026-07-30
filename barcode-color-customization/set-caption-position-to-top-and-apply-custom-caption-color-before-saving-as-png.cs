// Title: Set Caption Position to Top with Custom Color and Save as PNG
// Description: Demonstrates how to place a caption above a Code128 barcode, apply a custom color, and save the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and caption parameters. Developers commonly need to customize barcode appearance—such as adding captions, adjusting fonts, and setting colors—before exporting to image formats like PNG for web or print use. The snippet serves as a reference for creating branded or annotated barcodes in .NET applications.
// Prompt: Set the caption position to top and apply a custom caption color before saving as PNG.
// Tags: code128, caption, png, generation, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode with a top caption,
/// applies a custom caption color, and saves the image as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample text "1234567890".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Enable the caption above the barcode and set its text.
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Top Caption";

            // Apply a custom color (blue) to the caption text.
            generator.Parameters.CaptionAbove.TextColor = Color.Blue;

            // Optional: customize the caption font and alignment.
            generator.Parameters.CaptionAbove.Font.FamilyName = "Arial";
            generator.Parameters.CaptionAbove.Font.Size.Point = 12f;
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;

            // Save the generated barcode image as a PNG file.
            generator.Save("barcode_with_caption.png");
        }

        // Inform the user that the image has been saved.
        Console.WriteLine("Barcode image saved as 'barcode_with_caption.png'.");
    }
}