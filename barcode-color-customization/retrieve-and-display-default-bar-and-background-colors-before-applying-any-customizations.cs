using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to retrieve and display the default barcode and background colors
/// using Aspose.BarCode's <see cref="BarcodeGenerator"/>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Creates a <see cref="BarcodeGenerator"/> with default settings,
    /// extracts the default bar (foreground) and background colors,
    /// and prints them to the console in ARGB hexadecimal format.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with default parameters.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Access the default bar (foreground) color from the generator's parameters.
            var barColor = generator.Parameters.Barcode.BarColor;

            // Access the default background color from the generator's parameters.
            var backColor = generator.Parameters.BackColor;

            // Output the bar color as an ARGB hex string (e.g., #FF000000).
            Console.WriteLine($"Default Bar Color: #{barColor.ToArgb():X8}");

            // Output the background color as an ARGB hex string.
            Console.WriteLine($"Default Background Color: #{backColor.ToArgb():X8}");
        }
    }
}