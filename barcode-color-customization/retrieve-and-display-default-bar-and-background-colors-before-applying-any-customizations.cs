// Title: Retrieve default barcode and background colors
// Description: Demonstrates how to obtain the default bar and background colors from an Aspose.BarCode generator before any customizations are applied.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of the BarcodeGenerator class and its Parameters property to inspect default visual settings. Developers often need to query default colors to ensure consistency or to base custom color schemes on the original values. Typical use cases include UI previews, logging default configurations, or resetting colors after temporary changes.
// Prompt: Retrieve and display the default bar and background colors before applying any customizations.
// Tags: barcode, default colors, code128, aspnet, aspose.barcode, generation, colors

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that shows how to read the default bar and background colors
/// from a BarcodeGenerator instance before any visual customizations are made.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a BarcodeGenerator, retrieves the default
    /// colors, and writes them to the console.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator with the Code128 symbology and sample text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample"))
        {
            // Access the default bar (foreground) color from the generator's parameters.
            Color defaultBarColor = generator.Parameters.Barcode.BarColor;

            // Access the default background color from the generator's parameters.
            Color defaultBackColor = generator.Parameters.BackColor;

            // Output the retrieved default colors to the console.
            Console.WriteLine($"Default Bar Color: {defaultBarColor}");
            Console.WriteLine($"Default Background Color: {defaultBackColor}");
        }
    }
}