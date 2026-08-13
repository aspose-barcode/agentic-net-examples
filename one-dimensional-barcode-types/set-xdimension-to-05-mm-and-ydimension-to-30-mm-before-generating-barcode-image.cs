// Title: Generate Code128 barcode with custom X and Y dimensions
// Description: This example shows how to set the XDimension (module width) to 0.5 mm and the image height (Y dimension) to 30 mm before creating a barcode image using Aspose.BarCode.
// Category-Description: Aspose.BarCode barcode generation examples demonstrate how to configure barcode parameters such as size, format, and symbology. The key API classes include BarcodeGenerator, EncodeTypes, and the Parameters property hierarchy. Typical use cases involve creating printable barcodes for inventory, shipping, or point‑of‑sale systems where precise module dimensions are required.
// Prompt: Set XDimension to 0.5 mm and YDimension to 30 mm before generating the barcode image.
// Tags: code128, xdimension, ydimension, imageheight, barcode, generation, png, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode image with custom X and Y dimensions using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Configures barcode parameters and saves the image.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 symbology with the sample text "12345"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            // Set the module width (XDimension) to 0.5 mm
            generator.Parameters.Barcode.XDimension.Millimeters = 0.5f;

            // Set the image height (Y dimension) to 30 mm
            generator.Parameters.ImageHeight.Millimeters = 30f;

            // Save the generated barcode as a PNG file
            generator.Save("barcode.png");
        }

        // Inform the user that the barcode image has been created
        Console.WriteLine("Barcode image generated: barcode.png");
    }
}