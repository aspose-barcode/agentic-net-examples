// Title: Retrieve Default Barcode Bar and Background Colors
// Description: Demonstrates how to obtain the default bar color and background color from an Aspose.BarCode generator before any customizations are applied.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to access default visual parameters such as bar and background colors using the BarcodeGenerator class. Typical use cases include inspecting default settings, creating consistent styling, or resetting customizations. Developers working with barcode creation often need to query or modify these properties via the Parameters.Barcode and Parameters objects.
// Prompt: Retrieve and display the default bar and background colors before applying any customizations.
// Tags: barcode, default colors, code128, generation, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that retrieves and displays the default bar and background colors of a barcode generator.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // Define the barcode symbology and the code text to encode.
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Initialize the barcode generator within a using block to ensure proper disposal.
        using (var generator = new BarcodeGenerator(encodeType, "Sample"))
        {
            // Retrieve the generator's default bar color.
            Aspose.Drawing.Color defaultBarColor = generator.Parameters.Barcode.BarColor;

            // Retrieve the generator's default background color.
            Aspose.Drawing.Color defaultBackColor = generator.Parameters.BackColor;

            // Output the default colors to the console.
            Console.WriteLine($"Default Bar Color: {defaultBarColor}");
            Console.WriteLine($"Default Background Color: {defaultBackColor}");
        }
    }
}