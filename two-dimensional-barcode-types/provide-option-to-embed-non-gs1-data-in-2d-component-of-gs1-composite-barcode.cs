// Title: Embedding non‑GS1 data in the 2D component of a GS1 Composite barcode
// Description: Demonstrates how to generate a GS1 Composite barcode with non‑GS1 data in its 2D component, save it as an image, and read back both the linear and 2D parts.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, focusing on GS1 Composite barcodes. It showcases key API classes such as BarcodeGenerator, BarCodeReader, and related parameter objects. Typical use cases include creating composite barcodes that combine GS1 linear data with custom 2D payloads, and extracting each component for verification or processing. Developers often need to control encoding options, component types, and read detailed results, which this snippet illustrates.
// Prompt: Provide option to embed non‑GS1 data in the 2D component of a GS1 Composite barcode.
// Tags: gs1 composite, barcode generation, barcode recognition, non-gs1 data, 2d component, c#, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates embedding non‑GS1 data in the 2D component of a GS1 Composite barcode,
/// saving the image, and reading its components.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, saves it, and reads its components.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare the output directory and file path for the barcode image
        // ------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "GS1CompositeDemo");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }
        string barcodePath = Path.Combine(outputDir, "GS1CompositeNonGS1.png");

        // ------------------------------------------------------------
        // Create a GS1 Composite barcode where the 2D component holds non‑GS1 data
        // ------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(
            EncodeTypes.GS1CompositeBar,
            "(01)98898765432106(3202)012345|Aspose.Barcode"))
        {
            // Set visual and encoding parameters
            generator.Parameters.Barcode.XDimension.Pixels = 2f;                     // Module size
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None; // Hide human‑readable text
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_B; // Choose 2D component type
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128; // Linear part encoding
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false; // Permit non‑GS1 data in 2D part

            // Save the generated barcode as a PNG image
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {barcodePath}");

        // ------------------------------------------------------------
        // Read the saved barcode and output the texts of both components
        // ------------------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.GS1CompositeBar))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("Linear (1D) component text: " + result.Extended.GS1CompositeBar.OneDCodeText);
                Console.WriteLine("2D component text: " + result.Extended.GS1CompositeBar.TwoDCodeText);
                Console.WriteLine("2D component type: " + result.Extended.GS1CompositeBar.TwoDType);
            }
        }
    }
}