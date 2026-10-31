// Title: Set XDimension and ImageHeight for a Code128 barcode
// Description: Demonstrates how to configure the XDimension (module width) to 0.4 mm and set the overall image height to 25 mm for a Code128 barcode, then save it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to customize barcode dimensions. Developers often need to adjust module size and image dimensions to fit specific label layouts or printing requirements. The snippet shows typical steps for setting measurement properties and exporting the barcode image.
// Prompt: Set barcode XDimension to 0.4 mm and YDimension to 25 mm to meet specific label dimensions.
// Tags: barcode symbology, dimension, png, aspose.barcode, code128

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates setting XDimension and image height for a Code128 barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode with specified dimensions and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Define the full path where the barcode image will be saved
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Create a BarcodeGenerator for Code128 with the sample text "Sample123"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the module width (XDimension) to 0.4 mm
            generator.Parameters.Barcode.XDimension.Millimeters = 0.4f;

            // Set the overall image height to 25 mm (YDimension is not directly exposed)
            generator.Parameters.ImageHeight.Millimeters = 25f;

            // Save the generated barcode as a PNG file at the specified location
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been created
        Console.WriteLine($"Barcode generated at: {outputPath}");
    }
}