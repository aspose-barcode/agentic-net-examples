// Title: Generate GS1 Composite barcode with GS1 Code128 linear and CC_B 2D components
// Description: Demonstrates how to create a GS1 Composite barcode where the linear component is GS1 Code128 and the 2D component is CC_B, then saves it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of the BarcodeGenerator class with EncodeTypes.GS1CompositeBar. It shows configuring linear and 2D component types, adjusting barcode dimensions, and exporting the image. Developers working with GS1 standards, composite symbologies, or needing to produce combined linear/2D barcodes will find this pattern useful.
// Prompt: Generate a GS1 Composite barcode using GS1 Code128 as linear component and CC_B as 2D component.
// Tags: gs1 composite barcode, gs1code128, cc_b, barcode generation, aspose.barcode, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a GS1 Composite barcode (GS1 Code128 + CC_B) and saves it as PNG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates output directory, builds barcode data, configures generator, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define output folder and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated barcode image
        string outputPath = Path.Combine(outputDir, "GS1Composite_CC_B.png");

        // Linear component (GS1 Code128) with a valid GTIN-14
        string linearComponent = "(01)01234567890128";

        // 2D component data (GS1 Application Identifiers)
        string twoDComponent = "(10)ABCD0123(240)0123456789";

        // Combine linear and 2D components using the '|' separator required for GS1 Composite
        string codeText = $"{linearComponent}|{twoDComponent}";

        // Initialize the barcode generator for GS1 Composite symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set X-dimension (module width) to 2 pixels for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Hide the human‑readable text (code text) on the image
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Specify that the linear component should be GS1 Code128
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Specify that the 2D component should be CC_B
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_B;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"GS1 Composite barcode saved to: {outputPath}");
    }
}