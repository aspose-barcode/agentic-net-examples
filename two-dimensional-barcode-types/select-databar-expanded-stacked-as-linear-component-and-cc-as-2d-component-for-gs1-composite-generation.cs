// Title: Generate GS1 Composite Barcode with Databar Expanded Stacked and CC_A components
// Description: Demonstrates creating a GS1 Composite barcode where the linear component is Databar Expanded Stacked and the 2D component is CC_A, then saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on GS1 Composite barcode creation. It showcases the use of BarcodeGenerator, EncodeTypes, and GS1CompositeBar parameters to combine linear and 2D components. Developers working with retail, logistics, or inventory systems often need to generate GS1 Composite symbols for product identification and scanning efficiency.
// Prompt: Select Databar Expanded Stacked as linear component and CC_A as 2D component for GS1 Composite generation.
// Tags: gs1 composite, databar expanded stacked, cc_a, barcode generation, aspose.barcode, png output, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a GS1 Composite barcode with specific linear and 2D components
/// and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated barcode image
        string outPath = Path.Combine(outputDir, "GS1Composite_DatabarExpandedStacked_CC_A.png");

        // Sample GS1 Composite code text: linear part | 2D part
        string codeText = "(01)98898765432106(3202)012345(15)991231|(10)ABCD0123(240)0123456789";

        // Initialize the barcode generator with GS1 Composite type and the sample code text
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set the X-dimension (module width) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Hide the human‑readable text (code text) on the barcode image
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Configure the 2D component type to CC_A
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Configure the linear component type to Databar Expanded Stacked
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.DatabarExpandedStacked;

            // Save the generated barcode as a PNG file
            generator.Save(outPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outPath}");
    }
}