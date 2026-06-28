using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a Planet 2‑state postal barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Planet barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the barcode text: numeric string with length appropriate for Planet symbology.
        string codeText = "123456789012";

        // Initialize a BarcodeGenerator for the Planet symbology with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Planet, codeText))
        {
            // Configure the XDimension (module width) in points; 2.0 points yields a custom module size.
            generator.Parameters.Barcode.XDimension.Point = 2.0f;

            // Render and save the barcode image to a PNG file named "planet.png".
            generator.Save("planet.png");
        }

        // Inform the user that the barcode has been generated.
        Console.WriteLine("Planet barcode generated: planet.png");
    }
}