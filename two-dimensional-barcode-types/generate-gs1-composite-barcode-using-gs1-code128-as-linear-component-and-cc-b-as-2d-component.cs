// Title: Generate GS1 Composite Barcode with GS1 Code128 Linear and CC_B 2D Components
// Description: Demonstrates how to create a GS1 Composite barcode where the linear component is GS1 Code128 and the 2D component is CC_B (MicroPDF417) using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.GS1CompositeBar. It shows how to configure linear and 2D component types, adjust visual parameters, and save the result as an image. Developers working with GS1 standards, composite symbologies, or needing to embed both 1D and 2D data in a single barcode will find this pattern useful.
// Prompt: Generate a GS1 Composite barcode using GS1 Code128 as linear component and CC_B as 2D component.
// Tags: gs1 composite, gs1code128, cc_b, barcode generation, aspose.barcode, png output

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a GS1 Composite barcode with specific linear and 2D components.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Builds the composite code text, configures the generator, and saves the barcode image.
    /// </summary>
    static void Main()
    {
        // Define the linear (1D) component – GS1 Application Identifier (01) with a GTIN.
        string linearComponent = "(01)03212345678906";

        // Define the 2D component – GS1 Application Identifier (21) with a serial number.
        string twoDComponent = "(21)A1B2C3D4E5F6G7H8";

        // Combine components with the required '|' separator for GS1 Composite barcodes.
        string codeText = $"{linearComponent}|{twoDComponent}";

        // Initialize the barcode generator for GS1 Composite symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set the linear component type to GS1 Code128.
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Set the 2D component type to CC_B (MicroPDF417).
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_B;

            // Optional: adjust visual appearance.
            generator.Parameters.Barcode.XDimension.Pixels = 3f;   // Width of the narrowest bar.
            generator.Parameters.Barcode.BarHeight.Pixels = 100f; // Height of the linear component.

            // Save the generated barcode as a PNG image.
            string outputPath = "gs1composite.png";
            generator.Save(outputPath);

            // Inform the user where the file was saved.
            Console.WriteLine($"GS1 Composite barcode saved to {outputPath}");
        }
    }
}