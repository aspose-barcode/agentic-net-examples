// Title: Toggle XDimension Units Between Pixels and Millimeters
// Description: Demonstrates how to switch the XDimension measurement unit of a barcode between pixels and millimeters, generating separate images for each.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of the BarcodeGenerator class and its Parameters.Barcode.XDimension property. Developers often need to control barcode module size in different measurement units (pixels or millimeters) for print or screen rendering. The snippet illustrates typical use cases such as preparing barcode assets for UI previews or printable documents.
// Prompt: Design UI control allowing users to toggle between Pixels and Millimeters for barcode size, updating preview instantly.
// Tags: barcode symbology, size unit toggle, pixels, millimeters, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides a console demonstration of toggling barcode XDimension units between pixels and millimeters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates two barcode images with different XDimension units.
    /// </summary>
    static void Main()
    {
        // Prepare an output directory in the temporary folder.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeSizeDemo");
        Directory.CreateDirectory(outputDir);

        // The text to encode in the barcode.
        string codeText = "Demo";

        // ------------------------------------------------------------
        // Generate barcode with XDimension specified in Pixels.
        // ------------------------------------------------------------
        string pixelPath = Path.Combine(outputDir, "barcode_pixels.png");
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set module width to 3 pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 3f;
            // Save the barcode as a PNG image.
            generator.Save(pixelPath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Generate barcode with XDimension specified in Millimeters.
        // ------------------------------------------------------------
        string mmPath = Path.Combine(outputDir, "barcode_millimeters.png");
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set module width to 2 millimeters.
            generator.Parameters.Barcode.XDimension.Millimeters = 2f;
            // Save the barcode as a PNG image.
            generator.Save(mmPath, BarCodeImageFormat.Png);
        }

        // Output the locations of the generated files.
        Console.WriteLine("Barcode generated with Pixels unit: " + pixelPath);
        Console.WriteLine("Barcode generated with Millimeters unit: " + mmPath);
    }
}