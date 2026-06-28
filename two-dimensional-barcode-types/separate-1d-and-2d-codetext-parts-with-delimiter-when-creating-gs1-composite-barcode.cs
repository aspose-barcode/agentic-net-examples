using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a GS1 Composite barcode (linear + 2D components) using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a GS1 Composite barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the barcode data: linear part and 2D part are separated by the '|' character.
        // (01) – Application Identifier for GTIN, (21) – AI for serial number.
        string codeText = "(01)03212345678906|(21)A1B2C3D4E5F6G7H8";

        // Initialize the barcode generator for the GS1 Composite symbology.
        // The constructor receives the symbology type and the data to encode.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Configure the linear component (e.g., GS1 Code128) of the composite barcode.
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Configure the 2D component (e.g., CC-A – MicroPDF417) of the composite barcode.
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Optional: fine‑tune visual dimensions.
            generator.Parameters.Barcode.XDimension.Point = 2f;   // Width of a single module (in points).
            generator.Parameters.Barcode.BarHeight.Point = 50f; // Height of the linear (1D) component.

            // Render the barcode and write it to a PNG file.
            generator.Save("gs1_composite.png");
        }

        // Inform the user that the barcode image has been created.
        Console.WriteLine("GS1 Composite barcode generated: gs1_composite.png");
    }
}