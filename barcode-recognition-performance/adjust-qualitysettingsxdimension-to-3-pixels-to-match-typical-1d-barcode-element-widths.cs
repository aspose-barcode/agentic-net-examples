// Title: Generate Code128 Barcode with Custom XDimension
// Description: Demonstrates setting the XDimension to 3 pixels for a Code128 barcode and saving it as a PNG image.
// Prompt: Adjust QualitySettings.XDimension to 3 pixels to match typical 1D barcode element widths.
// Tags: code128, xdimension, barcode, generation, png, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode with a custom XDimension (module width)
/// and saves the result as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode and writes a confirmation message.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample text "1234567890".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set XDimension to 3 pixels (module width) to match typical 1D barcode element widths.
            generator.Parameters.Barcode.XDimension.Point = 3f;

            // Disable automatic sizing so that the custom XDimension is respected.
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Optionally set a bar height (in points) for better visual appearance.
            generator.Parameters.Barcode.BarHeight.Point = 50f;

            // Save the generated barcode image to a PNG file.
            generator.Save("code128_xdimension_3.png");
        }

        // Inform the user that the barcode has been generated.
        Console.WriteLine("Barcode generated with XDimension set to 3 pixels.");
    }
}