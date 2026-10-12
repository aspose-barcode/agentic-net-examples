// Title: Generate Code128 barcode with custom X and Y dimensions
// Description: Demonstrates how to set the X‑dimension (module width) to 0.4 mm and the Y‑dimension (image height) to 25 mm when generating a Code128 barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode size parameters via the BarcodeGenerator.Parameters properties. It covers key classes such as BarcodeGenerator, EncodeTypes, and BarCodeImageFormat, which developers use to create barcodes with precise physical dimensions for labeling, packaging, and inventory applications.
// Prompt: Set barcode XDimension to 0.4 mm and YDimension to 25 mm to meet specific label dimensions.
// Tags: code128, xdimension, ydimension, barcode generation, aspose.barcode, image output, png

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates setting XDimension and YDimension for a Code128 barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Creates a barcode image with custom dimensions and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Define the output file path
        string outputPath = "barcode.png";

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set module width (X dimension) to 0.4 mm
            generator.Parameters.Barcode.XDimension.Millimeters = 0.4f;

            // Set overall image height (Y dimension) to 25 mm
            generator.Parameters.ImageHeight.Millimeters = 25f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}