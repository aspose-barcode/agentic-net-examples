// Title: Generate Planet 2‑state postal barcode with custom XDimension
// Description: Creates a Planet (2‑state postal) barcode image using Aspose.BarCode, setting a custom XDimension while keeping the default bar height.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, demonstrating how to configure symbology‑specific parameters such as XDimension for a Planet barcode. It showcases the use of the BarcodeGenerator class together with EncodeTypes to produce PNG output, a common task for developers needing to embed postal barcodes in documents or applications.
// Prompt: Generate a Planet 2‑state postal barcode image with custom XDimension and default bar height.
// Tags: planet, barcode, 2-state, postal, xdimension, png, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to generate a Planet (2‑state postal) barcode image
/// with a custom XDimension while using the default bar height.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the data to encode in the Planet barcode.
        const string codeText = "1234567890";

        // Initialize a BarcodeGenerator for the Planet symbology with the provided text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Planet, codeText))
        {
            // Set a custom XDimension (module size) of 2 points.
            generator.Parameters.Barcode.XDimension.Point = 2f; // 2 points

            // No explicit BarHeight is set; the generator uses the default value.

            // Save the generated barcode image to a PNG file.
            generator.Save("planet.png");
        }

        // Inform the user that the barcode has been created.
        Console.WriteLine("Planet barcode generated: planet.png");
    }
}