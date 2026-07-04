// Title: Code39 Barcode with Increased XDimension
// Description: Demonstrates how to adjust the XDimension property to widen bars in a Code39 barcode, making it less dense for print media.
// Prompt: Adjust XDimension to increase bar width for a Code39 barcode, reducing visual density for print media.
// Tags: code39, barcode, xdimension, print, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a Code39 barcode with a larger XDimension to reduce visual density,
/// suitable for printing on media where wider bars are preferred.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a Code39 barcode, adjusts its XDimension,
    /// and saves the result as a PNG image.
    /// </summary>
    static void Main()
    {
        // Initialize a Code39 barcode generator with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "CODE39-EXAMPLE"))
        {
            // Disable auto-size mode so that manual XDimension settings take effect.
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Increase the XDimension to make each bar wider (e.g., 2 points).
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Optionally set a reasonable bar height for printing (e.g., 40 points).
            generator.Parameters.Barcode.BarHeight.Point = 40f;

            // Save the generated barcode image to a PNG file.
            generator.Save("code39.png");
        }

        // Inform the user that the barcode has been generated.
        Console.WriteLine("Code39 barcode generated with increased XDimension.");
    }
}