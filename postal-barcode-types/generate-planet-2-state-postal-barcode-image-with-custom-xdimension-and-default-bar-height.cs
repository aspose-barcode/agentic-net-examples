using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample codetext for a Planet 2‑state postal barcode.
        const string codeText = "1234567890";

        // Create a barcode generator for the Planet symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Planet, codeText))
        {
            // Set a custom X‑dimension (smallest bar width). Value is in points.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Use default bar height (no explicit setting needed).

            // Save the generated barcode image to a PNG file.
            generator.Save("planet.png");
        }

        Console.WriteLine("Planet barcode image saved as 'planet.png'.");
    }
}