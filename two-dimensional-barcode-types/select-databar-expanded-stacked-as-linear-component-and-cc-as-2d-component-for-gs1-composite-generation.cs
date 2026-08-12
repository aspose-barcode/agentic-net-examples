// Title: Generate GS1 Composite Barcode with Databar Expanded Stacked Linear Component and CC_A 2D Component
// Description: Demonstrates how to create a GS1 Composite barcode where the linear component uses Databar Expanded Stacked and the 2D component uses the CC_A (MicroPDF417) variant, then saves it as a PNG image.
// Category-Description: This example belongs to the GS1 Composite barcode generation category of Aspose.BarCode for .NET. It showcases configuring linear and 2D components using the BarcodeGenerator, setting dimensions, and exporting the result. Developers working with GS1 standards often need to combine linear and 2D symbologies for packaging and labeling, and this snippet illustrates the typical API usage.
// Prompt: Select Databar Expanded Stacked as linear component and CC_A as 2D component for GS1 Composite generation.
// Tags: gs1 composite, databar expanded stacked, cc_a, barcode generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a GS1 Composite barcode with specific linear and 2D components
/// and saves the result as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Prepares the output folder, builds the composite codetext,
    /// configures the barcode generator, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Prepare output directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Build GS1 Composite codetext: linear part | 2D part
        // Linear component must contain AI (01) with exactly 14 digits.
        string linearComponent = "(01)00123456789012"; // 14‑digit GTIN
        string twoDComponent = "(21)A12345678";       // Sample AI for serial number
        string codeText = $"{linearComponent}|{twoDComponent}";

        // Create GS1 Composite barcode generator with the combined codetext
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set linear component to Databar Expanded Stacked
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.DatabarExpandedStacked;

            // Set 2D component to CC_A (MicroPDF417 structural variant)
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Optional: adjust dimensions for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 3f;
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;

            // Save the barcode image to the output folder
            string outputPath = Path.Combine(outputDir, "gs1composite.png");
            generator.Save(outputPath);
            Console.WriteLine($"GS1 Composite barcode saved to: {outputPath}");
        }
    }
}