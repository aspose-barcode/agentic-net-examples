// Title: Retrieve Default Barcode Bar and Background Colors
// Description: Demonstrates how to obtain the default bar and background colors from an Aspose.BarCode generator before any customizations are applied.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, its Parameters, and color properties. Developers often need to query default visual settings (bar color, background color) to ensure consistency or to base custom themes on them. Typical use cases include preparing barcode images for reports, labels, or UI previews where default styling information is required.
// Prompt: Retrieve and display the default bar and background colors before applying any customizations.
// Tags: barcode, symbology, color, default, generation, png, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that retrieves and displays the default bar and background colors
/// of a barcode generator before any customizations are applied.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a BarcodeGenerator, reads default colors,
    /// prints them to the console, and saves a sample barcode image.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator with the Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Retrieve the generator's default bar (foreground) color.
            Aspose.Drawing.Color defaultBarColor = generator.Parameters.Barcode.BarColor;

            // Retrieve the generator's default background color.
            Aspose.Drawing.Color defaultBackColor = generator.Parameters.BackColor;

            // Output the default colors to the console.
            Console.WriteLine($"Default Bar Color: {defaultBarColor}");
            Console.WriteLine($"Default Background Color: {defaultBackColor}");

            // Optionally generate a sample barcode image to verify the defaults.
            // The image is saved as "default_barcode.png" in the working directory.
            generator.Save("default_barcode.png");
        }
    }
}