// Title: Apply custom foreground color to a Code128 barcode
// Description: Demonstrates setting a barcode's foreground color using a hexadecimal value to match corporate branding.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode appearance with the BarcodeGenerator class. Typical use cases include branding, visual consistency, and UI integration where developers need to apply specific colors to generated barcodes.
// Prompt: Apply custom foreground color using hexadecimal value #FF6600 to match corporate branding.
// Tags: barcode symbology, color customization, code128, png output, aspose.barcode generation

using System;
using System.Globalization;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode with a custom foreground color defined by a hexadecimal value.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode, applies a custom color, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "custom_color_barcode.png";

        // Initialize the barcode generator for the Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the text that will be encoded into the barcode.
            generator.CodeText = "Sample123";

            // Convert the hexadecimal color string to an ARGB integer and apply it as the barcode's foreground color.
            generator.Parameters.Barcode.BarColor = Color.FromArgb(ParseHexColor("#FF6600"));

            // Save the generated barcode image to the specified file path.
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }

    /// <summary>
    /// Parses a hex color string (e.g., "#FF6600") to an ARGB integer.
    /// If the alpha component is omitted, it defaults to fully opaque (FF).
    /// </summary>
    /// <param name="hex">Hexadecimal color string, optionally prefixed with '#'.</param>
    /// <returns>Integer representing the ARGB color.</returns>
    static int ParseHexColor(string hex)
    {
        // Remove the leading '#' if present.
        if (hex.StartsWith("#"))
            hex = hex.Substring(1);

        // If only RGB components are provided, prepend 'FF' for full opacity.
        if (hex.Length == 6)
            hex = "FF" + hex;

        // Parse the hexadecimal string to an integer using invariant culture.
        return int.Parse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
    }
}