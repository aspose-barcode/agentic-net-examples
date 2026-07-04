// Title: Retrieve Default Barcode Colors
// Description: Demonstrates how to obtain the default foreground (bar) and background colors of a barcode before any customizations are applied.
// Prompt: Retrieve and display the default bar and background colors before applying any customizations.
// Tags: barcode, colors, default, aspose.barcode, c#

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that shows how to read the default bar and background colors
/// from an Aspose.BarCode <see cref="BarcodeGenerator"/> instance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Retrieves and prints the default barcode colors before any changes are made.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator with the default symbology (Code128)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Get the default foreground (bar) color
            Color defaultBarColor = generator.Parameters.Barcode.BarColor;

            // Get the default background color
            Color defaultBackColor = generator.Parameters.BackColor;

            // Output the retrieved default colors to the console
            Console.WriteLine($"Default Bar Color: {defaultBarColor}");
            Console.WriteLine($"Default Background Color: {defaultBackColor}");
        }
    }
}