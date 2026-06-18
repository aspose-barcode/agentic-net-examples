using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with a caption placed above it
/// and saving the result as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a barcode, configures its caption,
    /// saves the image, and writes a confirmation to the console.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 using the sample text "123456"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Enable the caption that appears above the barcode
            generator.Parameters.CaptionAbove.Visible = true;

            // Set the caption text and its color
            generator.Parameters.CaptionAbove.Text = "Top Caption";
            generator.Parameters.CaptionAbove.TextColor = Color.Red;

            // Optional: configure the caption's font family, size, and alignment
            generator.Parameters.CaptionAbove.Font.FamilyName = "Arial";
            generator.Parameters.CaptionAbove.Font.Size.Point = 12f;
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;

            // Save the generated barcode image as a PNG file named "barcode.png"
            generator.Save("barcode.png");
        }

        // Inform the user that the barcode has been saved
        Console.WriteLine("Barcode saved as barcode.png");
    }
}