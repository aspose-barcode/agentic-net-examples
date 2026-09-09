// Title: Toggle Barcode XDimension Units Between Pixels and Millimeters
// Description: Demonstrates how to generate a barcode using Aspose.BarCode with XDimension specified in pixels versus millimeters, illustrating the effect of unit toggling.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on barcode size configuration. It showcases the use of BarcodeGenerator, EncodeTypes, and the XDimension property to set module size in different measurement units. Developers often need to switch between pixel and millimeter units when rendering barcodes for screen display versus print, making this pattern essential for UI controls that allow unit selection.
// Prompt: Design UI control allowing users to toggle between Pixels and Millimeters for barcode size, updating preview instantly.
// Tags: barcode, code128, xdimension, pixels, millimeters, generation, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides a console demonstration of toggling barcode size units between pixels and millimeters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates two barcodes with different XDimension units and saves them to temporary files.
    /// </summary>
    static void Main()
    {
        // In a real UI you would provide a toggle control; this console example shows the core logic.

        // Create a unique temporary directory for the output images.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeToggleDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Common barcode data and output file paths.
        string codeText = "Demo123";
        string pixelPath = Path.Combine(outputDir, "Barcode_Pixels.png");
        string millimeterPath = Path.Combine(outputDir, "Barcode_Millimeters.png");

        // Generate barcode with XDimension set in pixels.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // 3 pixels per module.
            generator.Parameters.Barcode.XDimension.Pixels = 3f;
            generator.Save(pixelPath, BarCodeImageFormat.Png);
        }

        // Generate barcode with XDimension set in millimeters.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // 2 millimeters per module.
            generator.Parameters.Barcode.XDimension.Millimeters = 2f;
            generator.Save(millimeterPath, BarCodeImageFormat.Png);
        }

        // Output the locations of the generated barcode images.
        Console.WriteLine("Barcode generated with Pixels unit: " + pixelPath);
        Console.WriteLine("Barcode generated with Millimeters unit: " + millimeterPath);
    }
}