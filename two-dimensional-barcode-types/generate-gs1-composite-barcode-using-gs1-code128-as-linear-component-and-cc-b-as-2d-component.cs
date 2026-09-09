// Title: Generate GS1 Composite barcode with GS1 Code128 linear and CC_B 2D components
// Description: This example creates a GS1 Composite barcode where the linear component is GS1 Code128 and the 2‑dimensional component is CC_B, saves it as a PNG file, and then reads back the barcode to display decoded information.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition of GS1 Composite barcodes. The example uses BarcodeGenerator to configure linear (GS1Code128) and 2D (CC_B) components, saves the image, and employs BarCodeReader to decode it. Developers working with product identification, logistics, or retail can use these APIs to embed GS1 data in both linear and matrix formats, a common requirement for supply‑chain labeling.
// Prompt: Generate a GS1 Composite barcode using GS1 Code128 as linear component and CC_B as 2D component.
// Tags: gs1 composite, barcode generation, barcode recognition, code128, cc_b, png, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a GS1 Composite barcode (GS1 Code128 + CC_B),
/// saves it as an image, and then reads back the encoded data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode creation, saving, and decoding.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "GS1Composite_CC_B.png");

        // Sample GS1 Composite code text:
        //   Linear part (GS1 Code128) | 2D part (CC_B)
        string codeText = "(01)98898765432106(3202)012345|(10)ABCD0123(240)0123456789";

        // Generate the GS1 Composite barcode with the specified components.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set barcode visual parameters.
            generator.Parameters.Barcode.XDimension.Pixels = 2;                     // Module width.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None; // Hide human‑readable text.

            // Configure the composite components.
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_B;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {outputPath}");

        // Read the generated barcode image and display decoded information.
        using (BarCodeReader reader = new BarCodeReader(outputPath, DecodeType.GS1CompositeBar))
        {
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Decoded CodeText: {result.CodeText}");
                    Console.WriteLine($"2D Component Text: {result.Extended.GS1CompositeBar.TwoDCodeText}");
                }
            }
        }
    }
}