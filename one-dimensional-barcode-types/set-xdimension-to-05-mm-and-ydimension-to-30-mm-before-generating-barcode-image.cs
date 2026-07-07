// Title: Generate Code128 Barcode with Custom X and Y Dimensions
// Description: This example shows how to set the X‑dimension to 0.5 mm and the image height (Y‑dimension) to 30 mm before generating a barcode image using Aspose.BarCode.
// Category-Description: The sample belongs to the Aspose.BarCode generation category, illustrating how to configure barcode size parameters via the BarcodeGenerator and its Parameters properties. It covers common tasks such as adjusting module width (XDimension) and image height for precise printing or display requirements. Developers often need these settings when integrating barcodes into labels, packaging, or UI components.
// Prompt: Set XDimension to 0.5 mm and YDimension to 30 mm before generating the barcode image.
// Tags: code128, xdimension, ydimension, imageheight, barcode generation, aspose.barcode, png

using System;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode with custom X and Y dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Configures barcode size parameters and saves the image.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set X-dimension (module width) to 0.5 millimeters
            generator.Parameters.Barcode.XDimension.Millimeters = 0.5f;

            // Set image height (Y dimension) to 30 millimeters
            generator.Parameters.ImageHeight.Millimeters = 30f;

            // Save the generated barcode image as PNG
            generator.Save("barcode.png");
        }
    }
}