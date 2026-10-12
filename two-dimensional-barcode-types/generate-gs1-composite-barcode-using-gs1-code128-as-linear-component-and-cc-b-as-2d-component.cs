// Title: Generate GS1 Composite barcode with GS1 Code128 linear and CC_B 2D components
// Description: Demonstrates how to create a GS1 Composite barcode where the linear component is GS1 Code128 and the 2‑D component is CC_B, then save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of the BarcodeGenerator class with EncodeTypes.GS1CompositeBar. It shows configuring X‑dimension, hiding the human‑readable text, and selecting specific linear and 2‑D component types. Developers creating GS1 Composite symbols for packaging, logistics, or retail can follow this pattern to produce compliant barcodes in various image formats.
// Prompt: Generate a GS1 Composite barcode using GS1 Code128 as linear component and CC_B as 2D component.
// Tags: gs1 composite, code128, cc_b, png, aspose.barcode, csharp, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a GS1 Composite barcode (GS1 Code128 + CC_B) and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates the barcode, configures its parameters, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Prepare output directory in the system temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeOutput");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "GS1Composite_CC_B.png");

        // GS1 Composite barcode data: linear (GS1 Code128) and 2‑D (CC_B) components separated by '|'
        string codeText = "(01)98898765432106(3202)012345|(10)ABCD0123(240)0123456789";

        // Initialize the generator with the composite symbology and the combined code text
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set the X‑dimension (module width) to 2 pixels for better readability
            gen.Parameters.Barcode.XDimension.Pixels = 2f;

            // Hide the human‑readable text because the composite barcode already encodes the data
            gen.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Specify the 2‑D component type as CC_B
            gen.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_B;

            // Specify the linear component type as GS1 Code128
            gen.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Save the generated barcode as a PNG image
            gen.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}