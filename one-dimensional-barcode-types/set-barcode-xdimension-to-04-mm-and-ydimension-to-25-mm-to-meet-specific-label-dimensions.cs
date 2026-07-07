// Title: Set XDimension and YDimension for a Code128 barcode
// Description: Demonstrates configuring the module width (XDimension) and bar height (YDimension) of a Code128 barcode using Aspose.BarCode, then saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to customize barcode dimensions with the BarcodeGenerator class. Developers commonly adjust XDimension for module width and YDimension for bar height to meet specific label size requirements, using the Parameters.Barcode properties before rendering the image.
// Prompt: Set barcode XDimension to 0.4 mm and YDimension to 25 mm to meet specific label dimensions.
// Tags: code128, xdimension, ydimension, barcode, generation, png, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Code128 barcode with custom XDimension and YDimension settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, configures its dimensions, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 symbology with the sample text "123456"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the module width (XDimension) to 0.4 mm
            generator.Parameters.Barcode.XDimension.Millimeters = 0.4f;

            // Set the bar height (YDimension) to 25 mm
            generator.Parameters.Barcode.BarHeight.Millimeters = 25f;

            // Render and save the barcode image to a PNG file named "barcode.png"
            generator.Save("barcode.png");
        }
    }
}