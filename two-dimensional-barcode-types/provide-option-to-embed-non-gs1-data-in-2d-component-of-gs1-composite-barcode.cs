// Title: Generate GS1 Composite Barcode with Non‑GS1 Data in 2D Component
// Description: Demonstrates how to create a GS1 Composite barcode where the linear component follows GS1 AI standards and the 2D component contains arbitrary (non‑GS1) data.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on composite symbologies. It shows usage of the BarcodeGenerator class with EncodeTypes.GS1CompositeBar, configuring linear and 2D component types via the GS1CompositeBar parameters, and disabling strict GS1 encoding to embed custom data. Developers often need to combine GS1 linear data with additional information in the 2D part for packaging, logistics, or marketing applications.
// Prompt: Provide option to embed non‑GS1 data in the 2D component of a GS1 Composite barcode.
// Tags: gs1 composite barcode, non-gs1 data, 2d component, aspose.barcode, c#, barcode generation

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Example program that generates a GS1 Composite barcode with custom (non‑GS1) data
/// in the 2D component while keeping the linear component GS1‑compliant.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Builds the composite codetext, configures generator parameters,
    /// and saves the resulting barcode image.
    /// </summary>
    static void Main()
    {
        // Linear component must be a valid GS1 AI (01) with 14 digits.
        string linearComponent = "(01)00123456789012";

        // Non‑GS1 data to be placed in the 2D component.
        string twoDComponent = "HelloWorld";

        // GS1 Composite codetext uses '|' as separator between linear and 2D parts.
        string codeText = $"{linearComponent}|{twoDComponent}";

        // Initialize the generator for a GS1 Composite barcode.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Use GS1 Code128 for the linear part.
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Choose a 2D component type (e.g., CC_A – MicroPDF417).
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Allow non‑GS1 data in the 2D component.
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;

            // Optional: adjust dimensions for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 3f;   // Module width
            generator.Parameters.Barcode.BarHeight.Pixels = 100f; // Linear part height

            // Save the barcode image to a file.
            generator.Save("GS1Composite.png");
        }

        Console.WriteLine("GS1 Composite barcode generated: GS1Composite.png");
    }
}