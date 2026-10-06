// Title: Retrieve default bar and background colors of a barcode
// Description: Demonstrates how to access and display the default bar color and background color of a BarcodeGenerator before any customizations are applied.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator and its Parameters to query default visual settings. Developers often need to read default styling values such as bar and background colors when building dynamic barcode rendering pipelines or when preserving original appearance. The snippet shows typical API classes like BarcodeGenerator, EncodeTypes, and Aspose.Drawing.Color, useful for quick diagnostics or UI previews.
// Prompt: Retrieve and display the default bar and background colors before applying any customizations.
// Tags: barcode, code128, default-colors, console, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Entry point for the example that retrieves and prints default barcode visual settings.
/// </summary>
class Program
{
    /// <summary>
    /// Main method demonstrating retrieval of default bar and background colors from a BarcodeGenerator.
    /// </summary>
    static void Main()
    {
        // Create a BarcodeGenerator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample"))
        {
            // Get the default bar color
            Color defaultBarColor = generator.Parameters.Barcode.BarColor;
            // Get the default background color
            Color defaultBackColor = generator.Parameters.BackColor;

            // Display the retrieved colors
            Console.WriteLine($"Default Bar Color: {defaultBarColor}");
            Console.WriteLine($"Default Background Color: {defaultBackColor}");
        }
    }
}