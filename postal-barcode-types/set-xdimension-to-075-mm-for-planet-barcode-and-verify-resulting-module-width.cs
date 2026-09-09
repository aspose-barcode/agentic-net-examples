// Title: Set XDimension for Planet barcode and verify module width
// Description: Demonstrates how to configure the XDimension (module width) of a Planet barcode to 0.75 mm using Aspose.BarCode and confirms the setting.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and barcode parameter settings. Typical use cases include customizing barcode dimensions for printing standards and ensuring compliance with size specifications. Developers often need to adjust XDimension to control module width for various symbologies.
// Prompt: Set XDimension to 0.75 mm for a Planet barcode and verify resulting module width.
// Tags: planet barcode, xdimension, module width, barcode generation, aspose.barcode, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates setting XDimension for a Planet barcode and verifying the module width.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Planet barcode with XDimension set to 0.75 mm, saves it as PNG, and outputs the set value.
    /// </summary>
    static void Main()
    {
        // Determine temporary output file path
        string outputPath = Path.Combine(Path.GetTempPath(), "PlanetBarcode.png");

        // Initialize barcode generator for Planet symbology with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Planet, "123456"))
        {
            // Configure XDimension (module width) to 0.75 millimeters
            generator.Parameters.Barcode.XDimension.Millimeters = 0.75f;

            // Save the generated barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Retrieve and display the XDimension value to verify the setting
            float setValue = generator.Parameters.Barcode.XDimension.Millimeters;
            Console.WriteLine($"XDimension set to {setValue} mm for Planet barcode.");
            Console.WriteLine($"Barcode image saved to: {outputPath}");
        }
    }
}