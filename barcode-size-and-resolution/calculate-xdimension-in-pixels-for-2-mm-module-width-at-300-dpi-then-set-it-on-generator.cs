// Title: Calculate XDimension in Pixels for 2 mm module width at 300 dpi
// Description: Demonstrates how to compute the XDimension (module width) in pixels for a 2 mm barcode module at 300 dpi and apply it to a Code128 barcode generator.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating resolution handling and precise module sizing. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to create high‑resolution barcodes. Developers often need to control XDimension for printing accuracy and scanner compatibility, making this pattern common in manufacturing and logistics applications.
// Prompt: Calculate XDimension in Pixels for 2 mm module width at 300 dpi, then set it on generator.
// Tags: code128, xdimension, png, barcodegenerator, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that calculates the XDimension in pixels for a given module width
/// and DPI, then generates a Code128 barcode with that setting.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs the calculation, configures the generator,
    /// and saves the resulting barcode image.
    /// </summary>
    static void Main()
    {
        // Define the desired module width (in millimeters) and the target resolution (DPI)
        float moduleWidthMillimeters = 2f;
        float dpi = 300f;

        // Convert the module width from millimeters to inches (1 inch = 25.4 mm)
        // and then to pixels using the DPI value
        float xDimensionPixels = (moduleWidthMillimeters / 25.4f) * dpi;

        // Prepare a temporary folder to store the generated barcode image
        string outputFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeExample");
        Directory.CreateDirectory(outputFolder);
        string outputPath = Path.Combine(outputFolder, "barcode.png");

        // Create a barcode generator for Code128 with the sample data "123456"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Apply the calculated resolution and XDimension (in pixels) to the generator
            generator.Parameters.Resolution = dpi;
            generator.Parameters.Barcode.XDimension.Pixels = xDimensionPixels;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved file and the XDimension value used
        Console.WriteLine($"Barcode saved to: {outputPath}");
        Console.WriteLine($"XDimension set to {xDimensionPixels} pixels (2 mm at {dpi} dpi).");
    }
}