using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode using the Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Generates a barcode, configures its appearance,
    /// saves it as a PNG file, and writes a confirmation message to the console.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for Code128 with the specified data string.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the barcode's bar color to blue.
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the background color of the barcode image to white.
            generator.Parameters.BackColor = Color.White;

            // Enable and configure a caption displayed above the barcode.
            generator.Parameters.CaptionAbove.Visible = true;               // Show the caption.
            generator.Parameters.CaptionAbove.Text = "Sample Caption";     // Caption text.
            generator.Parameters.CaptionAbove.TextColor = Color.Red;       // Caption text color.

            // Save the generated barcode image as a PNG file.
            generator.Save("code128.png");
        }

        // Output a confirmation message to the console.
        Console.WriteLine("Barcode generated and saved as code128.png");
    }
}