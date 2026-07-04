// Title: Generate Code128 Barcode with Fixed Size for Low‑Resolution Screens
// Description: Demonstrates disabling auto‑size, setting XDimension, and saving a PNG barcode suitable for low‑resolution display.
// Prompt: Set AutoSizeMode to None, assign XDimension, and generate a barcode suitable for low‑resolution screen display.
// Tags: code128, autosizemode, xdimension, png, aspnet.barcode, barcode generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Code128 barcode with manual sizing for low‑resolution screens.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, saves it as PNG, and writes a confirmation to the console.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample text "1234567890"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Disable automatic sizing to keep full control over the barcode dimensions
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Set a small XDimension (module width) in points, suitable for low‑resolution screen display
            generator.Parameters.Barcode.XDimension.Point = 1f;

            // Save the generated barcode image as a PNG file
            generator.Save("barcode.png");
        }

        // Inform the user that the barcode has been generated
        Console.WriteLine("Barcode generated: barcode.png");
    }
}