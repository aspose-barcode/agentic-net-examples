// Title: Retrieve Default Barcode Bar and Background Colors
// Description: Demonstrates how to obtain the default bar and background colors of a BarcodeGenerator before any customizations are applied.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and the Parameters property to inspect default visual settings. Developers often need to read or modify barcode appearance such as bar color, background color, font, and size; this snippet illustrates the initial state retrieval, a common first step before applying custom styling in barcode creation workflows.
// Prompt: Retrieve and display the default bar and background colors before applying any customizations.
// Tags: barcode symbology, color retrieval, default colors, generation, aspose.barcode, code128, c#

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that retrieves and displays the default bar and background colors
/// of a BarcodeGenerator instance before any customizations are applied.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Initializes a BarcodeGenerator, reads the default colors,
    /// and writes them to the console.
    /// </summary>
    static void Main()
    {
        // Create a BarcodeGenerator for Code128 symbology with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample"))
        {
            // Retrieve the default bar (foreground) color.
            Color defaultBarColor = generator.Parameters.Barcode.BarColor;

            // Retrieve the default background color.
            Color defaultBackColor = generator.Parameters.BackColor;

            // Output the ARGB components of the default bar color.
            Console.WriteLine($"Default Bar Color: A={defaultBarColor.A}, R={defaultBarColor.R}, G={defaultBarColor.G}, B={defaultBarColor.B}");

            // Output the ARGB components of the default background color.
            Console.WriteLine($"Default Background Color: A={defaultBackColor.A}, R={defaultBackColor.R}, G={defaultBackColor.G}, B={defaultBackColor.B}");
        }
    }
}