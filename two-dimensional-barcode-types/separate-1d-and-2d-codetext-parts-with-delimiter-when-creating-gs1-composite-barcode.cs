// Title: Generate a GS1 Composite barcode with separate 1D and 2D parts
// Description: Demonstrates how to create a GS1 Composite barcode by separating the linear and 2D component data with a ‘|’ delimiter.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.GS1CompositeBar, setting linear and 2D component types, and adjusting dimensions. Developers working with GS1 Composite symbologies often need to combine 1D and 2D data streams, configure component types, and export the result as an image.
// Prompt: Separate 1D and 2D CodeText parts with ‘|’ delimiter when creating a GS1 Composite barcode.
// Tags: gs1 composite barcode, 1d 2d delimiter, aspose.barcode, barcode generation, png output

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Demonstrates creating a GS1 Composite barcode where the 1D and 2D data parts are separated by a ‘|’ delimiter.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode image and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Define the GS1 Composite barcode text: linear (1D) part and 2D part separated by '|'
        string codeText = "(01)03212345678906|(21)A1B2C3D4E5F6G7H8";

        // Initialize the generator for GS1 Composite Bar symbology with the combined code text
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Configure the linear component to use GS1 Code128
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Configure the 2D component to use CC-A (Composite Component A)
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Optional: adjust visual dimensions
            generator.Parameters.Barcode.XDimension.Pixels = 3f;   // module (X) size in pixels
            generator.Parameters.Barcode.BarHeight.Pixels = 100f; // height of the linear (1D) component

            // Save the generated barcode as a PNG image
            generator.Save("gs1composite.png");
        }

        // Inform the user that the barcode has been generated
        Console.WriteLine("GS1 Composite barcode generated: gs1composite.png");
    }
}