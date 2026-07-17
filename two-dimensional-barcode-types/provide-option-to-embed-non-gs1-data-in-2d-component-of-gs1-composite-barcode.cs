// Title: Embedding Non‑GS1 Data in 2D Component of GS1 Composite Barcode
// Description: Demonstrates how to generate a GS1 Composite barcode where the 2D component holds arbitrary (non‑GS1) data.
// Category-Description: This example belongs to the Aspose.BarCode generation suite, focusing on GS1 Composite barcodes. It showcases key API classes such as BarcodeGenerator, EncodeTypes, and TwoDComponentType. Typical use cases include combining a GS1 linear component with a custom 2D payload for packaging or logistics applications. Developers often need to toggle GS1 restrictions to embed free‑form data alongside standardized GS1 elements.
// Prompt: Provide option to embed non‑GS1 data in the 2D component of a GS1 Composite barcode.
// Tags: barcode, gs1, composite, non-gs1, 2d, csharp, aspose.barcode, generation, png

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a GS1 Composite barcode where the 2D component contains non‑GS1 data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Builds the barcode, configures linear and 2D components, and saves the image.
    /// </summary>
    static void Main()
    {
        // Linear component (GS1) and 2D component (non‑GS1) are separated by the '|' delimiter.
        string codeText = "(01)03212345678906|HelloWorld";

        // Initialize the generator for a GS1 Composite barcode with the combined code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set the linear component to GS1 Code 128 (standard for GS1 Composite).
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Choose a 2D component type that supports arbitrary data (CC_A – MicroPDF417).
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Disable the default GS1‑only restriction for the 2D component, allowing free‑form data.
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;

            // Optional visual adjustments: set module size and bar height.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.BarHeight.Pixels = 80f;

            // Save the generated barcode as a PNG image.
            generator.Save("gs1composite_non_gs1_2d.png");
        }

        Console.WriteLine("GS1 Composite barcode with non‑GS1 2D data generated successfully.");
    }
}