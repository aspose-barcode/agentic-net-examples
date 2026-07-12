// Title: Set XDimension for Planet barcode
// Description: Shows how to configure the XDimension (module width) of a Planet barcode to 0.75 mm, generate the barcode image, and confirm the setting.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode parameters such as XDimension using the BarcodeGenerator class. Typical use cases include adjusting module size for printing precision, meeting specification requirements, or matching branding guidelines. Developers often need to set dimensions, save images, and verify parameter values when working with various symbologies.
// Prompt: Set XDimension to 0.75 mm for a Planet barcode and verify resulting module width.
// Tags: planet, xdimension, module width, barcode generation, png, aspose.barcode, encode types

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates setting XDimension for a Planet barcode and saving the result.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Planet barcode with XDimension 0.75 mm, saves it, and writes the configured value to the console.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for the Planet symbology with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Planet, "1234567890"))
        {
            // Configure the module width (XDimension) to 0.75 mm
            generator.Parameters.Barcode.XDimension.Millimeters = 0.75f;

            // Define output file path and save the barcode as PNG
            string outputPath = "planet_barcode.png";
            generator.Save(outputPath);

            // Retrieve the actual XDimension value to verify the setting
            float setValue = generator.Parameters.Barcode.XDimension.Millimeters;

            // Output verification information
            Console.WriteLine($"XDimension set to: {setValue} mm");
            Console.WriteLine($"Barcode image saved to: {outputPath}");
        }
    }
}