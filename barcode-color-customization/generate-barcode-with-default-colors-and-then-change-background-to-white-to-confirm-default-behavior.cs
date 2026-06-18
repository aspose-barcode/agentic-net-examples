using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating Code128 barcodes using Aspose.BarCode with default and explicit white background.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates two barcode images and writes a confirmation to the console.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator for Code128 with the data "123456"
        // This instance uses default colors (black bars on white background)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Save the barcode image; the default background is already white
            generator.Save("barcode_default.png");
        }

        // Create another barcode generator for the same data
        // Explicitly set the background color to white (same as default) for demonstration
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the background color to white
            generator.Parameters.BackColor = Color.White;

            // Save the barcode image with the explicitly set white background
            generator.Save("barcode_whitebg.png");
        }

        // Output a message indicating the generated files
        Console.WriteLine("Barcodes generated: barcode_default.png, barcode_whitebg.png");
    }
}