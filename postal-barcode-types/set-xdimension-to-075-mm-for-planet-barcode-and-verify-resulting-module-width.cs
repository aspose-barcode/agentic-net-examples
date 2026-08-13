// Title: Set XDimension for Planet Barcode and Verify Module Width
// Description: Demonstrates how to set the XDimension (module width) to 0.75 mm for a Planet barcode using Aspose.BarCode and confirms the setting.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on barcode parameter configuration. It showcases the use of BarcodeGenerator, EncodeTypes, and the XDimension property to control module size, a common requirement when matching printing specifications or integrating with scanning hardware. Developers often need to adjust dimensions for compliance with standards or to achieve desired visual density.
// Prompt: Set XDimension to 0.75 mm for a Planet barcode and verify resulting module width.
// Tags: planet barcode, xdimension, module width, barcode generation, aspose.barcode, c#

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Planet barcode, sets its XDimension to 0.75 mm,
/// verifies the value, and saves the image to a file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, configures XDimension,
    /// outputs the configured value, and saves the result.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for the Planet symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Planet))
        {
            // Assign sample numeric data to be encoded in the barcode.
            generator.CodeText = "1234567890";

            // Configure the module width (XDimension) to 0.75 millimeters.
            generator.Parameters.Barcode.XDimension.Millimeters = 0.75f;

            // Retrieve and display the XDimension value to verify the setting.
            float xDimMm = generator.Parameters.Barcode.XDimension.Millimeters;
            Console.WriteLine($"XDimension is set to {xDimMm} mm.");

            // Save the generated barcode image as a PNG file.
            generator.Save("planet.png");
        }
    }
}