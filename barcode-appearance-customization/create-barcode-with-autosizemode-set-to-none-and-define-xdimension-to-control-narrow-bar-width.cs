// Title: Generate Code128 Barcode with Fixed XDimension
// Description: Demonstrates creating a Code128 barcode, disabling auto-size, and setting the narrow bar width via XDimension.
// Prompt: Create a barcode with AutoSizeMode set to None and define XDimension to control narrow bar width.
// Tags: code128, barcode, autosizemode, xdimension, png, aspose.barcode, aspnet

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode image with a custom narrow bar width.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, configures sizing options, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample text "1234567890".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Disable automatic sizing so manual dimensions can be applied.
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Define the narrow bar width (XDimension) as 2 points.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Set the bar height for the 1D barcode when AutoSizeMode is None.
            generator.Parameters.Barcode.BarHeight.Point = 50f;

            // Save the generated barcode image to a PNG file.
            generator.Save("barcode.png");
        }

        // Inform the user that the barcode has been created.
        Console.WriteLine("Barcode generated and saved to barcode.png");
    }
}