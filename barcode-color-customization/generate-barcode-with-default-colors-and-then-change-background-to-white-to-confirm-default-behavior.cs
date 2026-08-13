// Title: Generate Code128 barcode with default colors and explicit white background
// Description: Demonstrates creating a barcode using default color settings and then overriding the background to white to verify default behavior.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, set encoding type, and customize visual parameters such as background color. Typical use cases include creating barcodes for product labeling, inventory, and shipping where default styling may need verification or adjustment. Developers often need to generate images in PNG format and control colors for branding or readability.
// Prompt: Generate a barcode with default colors and then change background to white to confirm default behavior.
// Tags: code128, barcode generation, default colors, background color, png, aspose.barcode, aspnet

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with default colors and then with an explicit white background.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates output folder, generates two barcode images, and writes their paths to the console.
    /// </summary>
    static void Main()
    {
        // Ensure the output directory exists
        string outputFolder = "output";
        Directory.CreateDirectory(outputFolder);

        // Define file paths for the two generated images
        string defaultPath = Path.Combine(outputFolder, "barcode_default.png");
        string whiteBgPath = Path.Combine(outputFolder, "barcode_whitebg.png");

        // Generate barcode using default colors (BarColor = Black, BackColor = White)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(defaultPath);
        }

        // Generate barcode with background explicitly set to white
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Parameters.BackColor = Aspose.Drawing.Color.White; // explicit white background
            generator.Save(whiteBgPath);
        }

        // Output the locations of the saved barcode images
        Console.WriteLine($"Default barcode saved to: {defaultPath}");
        Console.WriteLine($"White background barcode saved to: {whiteBgPath}");
    }
}