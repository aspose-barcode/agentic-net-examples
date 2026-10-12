// Title: Generate GS1 Composite barcode with Databar Expanded Stacked linear component and CC_A 2D component
// Description: Demonstrates how to create a GS1 Composite barcode where the linear component is Databar Expanded Stacked and the 2‑D component is CC_A, saving the result as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of the BarcodeGenerator class with GS1CompositeBar encoding. It shows how to configure linear and 2‑D component types, adjust X‑dimension, and suppress linear text. Developers working with GS1 Composite symbologies for retail or logistics can use this pattern to produce combined linear‑and‑2D barcodes for packaging and labeling.
// Prompt: Select Databar Expanded Stacked as linear component and CC_A as 2D component for GS1 Composite generation.
// Tags: gs1 composite, databar expanded stacked, cc_a, barcode generation, png output, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a GS1 Composite barcode with specific component types
/// and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates the output folder, configures the barcode,
    /// and writes the generated image to disk.
    /// </summary>
    static void Main()
    {
        // Define a temporary directory to store the generated barcode image
        string outputDir = Path.Combine(Path.GetTempPath(), "GS1CompositeDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the output PNG file
        string outputPath = Path.Combine(outputDir, "GS1Composite_DatabarExpandedStacked_CC_A.png");

        // Sample GS1 Composite code text: linear part | 2D part
        string codeText = "(01)98898765432106(3202)012345(15)991231|(10)ABCD0123(240)0123456789";

        // Initialize the barcode generator with GS1 Composite encoding and the sample text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set the X-dimension (module width) to 2 pixels for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Hide the linear component's human‑readable text
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Configure the 2‑D component to use CC_A (Composite Component A)
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Configure the linear component to use Databar Expanded Stacked
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.DatabarExpandedStacked;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"GS1 Composite barcode saved to: {outputPath}");
    }
}