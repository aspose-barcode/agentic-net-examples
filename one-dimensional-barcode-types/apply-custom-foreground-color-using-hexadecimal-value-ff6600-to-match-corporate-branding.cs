// Title: Apply custom foreground color to barcode using hexadecimal value
// Description: Demonstrates how to generate a Code128 barcode with a custom foreground color specified by a hexadecimal value, saving it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode appearance using the BarcodeGenerator class. It shows setting the BarColor property to match corporate branding, a common requirement when integrating barcodes into marketing materials, product packaging, or documents. Developers often need to adjust colors, sizes, and formats to align with brand guidelines, and this snippet provides a quick reference for that task.
// Prompt: Apply custom foreground color using hexadecimal value #FF6600 to match corporate branding.
// Tags: barcode, code128, color, customization, generation, png, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode with a custom foreground color and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, applies the custom color, saves the image, and outputs the file path.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the system's temporary directory
        string outputPath = Path.Combine(Path.GetTempPath(), "custom_color_barcode.png");

        // Initialize the barcode generator with Code128 symbology and the data to encode
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the barcode's foreground (bar) color using ARGB values that correspond to the hex color #FF6600
            generator.Parameters.Barcode.BarColor = Color.FromArgb(255, 255, 102, 0);

            // Save the generated barcode as a PNG image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Write the location of the saved barcode image to the console
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}