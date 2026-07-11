// Title: Generate Planet 2‑state postal barcode with custom XDimension
// Description: Demonstrates creating a Planet 2‑state postal barcode image, customizing the XDimension while using the default bar height.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure barcode parameters such as XDimension for postal symbologies. It uses the BarcodeGenerator class with EncodeTypes.Planet, a common scenario for generating printable postal barcodes in logistics and mailing applications. Developers often need to adjust module width for scanner compatibility while keeping other settings default.
// Prompt: Generate a Planet 2‑state postal barcode image with custom XDimension and default bar height.
// Tags: planet, postal, barcode, generation, xdimension, png, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Planet 2‑state postal barcode image
/// with a custom XDimension (module width) while retaining the default bar height.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates the barcode, configures its XDimension,
    /// saves it as a PNG file, and writes a confirmation message to the console.
    /// </summary>
    static void Main()
    {
        // Initialize the barcode generator for the Planet symbology with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Planet, "123456789012"))
        {
            // Set a custom XDimension (module width) in points (2 points = 0.28 mm).
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // No explicit bar height is set; the default value will be used.

            // Save the generated barcode image to a PNG file.
            generator.Save("planet.png");
        }

        // Inform the user that the barcode image has been created.
        Console.WriteLine("Planet barcode generated: planet.png");
    }
}