// Title: Generate GS1 Composite barcode with Databar Expanded Stacked linear component and CC_A 2D component
// Description: Demonstrates how to create a GS1 Composite barcode where the linear part uses Databar Expanded Stacked and the 2D part uses CC_A, saving the result as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of the BarcodeGenerator class to produce GS1 Composite barcodes. It shows how to configure linear and 2D component types, adjust dimensions, and control code text placement—common tasks for developers integrating composite barcodes into packaging, labeling, or inventory systems.
// Prompt: Select Databar Expanded Stacked as linear component and CC_A as 2D component for GS1 Composite generation.
// Tags: gs1 composite barcode, databar expanded stacked, cc_a, c#, aspose.barcode, barcode generation, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a GS1 Composite barcode with specific component types.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates and saves the barcode image.
    /// </summary>
    static void Main()
    {
        // Define output directory in the temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "GS1CompositeExample");
        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Linear component data (GS1 Application Identifiers)
        string linearComponent = "(01)98898765432106(3202)012345(15)991231";
        // 2D component data (GS1 Application Identifiers)
        string twoDComponent = "(10)ABCD0123(240)0123456789";
        // Combine linear and 2D components with the required separator
        string codeText = $"{linearComponent}|{twoDComponent}";
        // Full path for the generated PNG file
        string outputPath = Path.Combine(outputDir, "GS1Composite_DatabarExpandedStacked_CC_A.png");

        // Initialize the barcode generator for GS1 Composite type with the combined code text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set the X-dimension (module width) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2;
            // Hide the human‑readable text (not needed for composite barcodes)
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;
            // Specify the linear component type as Databar Expanded Stacked
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.DatabarExpandedStacked;
            // Specify the 2D component type as CC_A
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image
        Console.WriteLine($"GS1 Composite barcode saved to: {outputPath}");
    }
}