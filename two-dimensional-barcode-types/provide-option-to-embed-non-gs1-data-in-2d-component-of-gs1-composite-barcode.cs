// Title: Generate GS1 Composite barcode with non‑GS1 data in the 2D component
// Description: Demonstrates creating a GS1 Composite barcode where the 2D component contains non‑GS1 data, and shows how to read back both linear and 2D components.
// Category-Description: This example belongs to the Aspose.BarCode composite barcode operations collection. It illustrates using BarcodeGenerator and BarCodeReader to work with GS1 Composite barcodes, configuring linear and 2D component types, and handling non‑GS1 data in the 2D part. Developers often need to embed custom information alongside GS1 data for packaging and logistics solutions.
// Prompt: Provide option to embed non‑GS1 data in the 2D component of a GS1 Composite barcode.
// Tags: gs1 composite barcode, non-gs1 data, 2d component, aspose.barcode, barcode generation, barcode recognition, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation and recognition of a GS1 Composite barcode
/// where the 2D component contains non‑GS1 data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a GS1 Composite barcode,
    /// saves it as PNG, and reads it back to display component values.
    /// </summary>
    static void Main()
    {
        // -----------------------------------------------------------------
        // Prepare output directory
        // -----------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "Gs1CompositeDemo");
        Directory.CreateDirectory(outputDir);

        // -----------------------------------------------------------------
        // Define barcode components
        // -----------------------------------------------------------------
        // Linear component (GS1 Code128) with a valid GTIN‑14
        string linearComponent = "(01)01234567890128";

        // 2D component data (non‑GS1 example)
        string twoDComponent = "Aspose.BarCode";

        // Combine components using the pipe separator required for GS1 Composite
        string codeText = $"{linearComponent}|{twoDComponent}";

        // Path for the generated barcode image
        string barcodePath = Path.Combine(outputDir, "Gs1Composite_NonGS1.png");

        // -----------------------------------------------------------------
        // Generate GS1 Composite barcode allowing non‑GS1 data in 2D component
        // -----------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set visual density
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Hide linear component text in the image
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Configure 2D component type (CC-C) and linear component type (GS1 Code128)
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_C;
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Allow non‑GS1 data in the 2D component
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;

            // Save the barcode as PNG
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {barcodePath}");

        // -----------------------------------------------------------------
        // Read back the barcode to verify both components
        // -----------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.GS1CompositeBar))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Full CodeText: {result.CodeText}");

                // Ensure extended GS1 Composite data is available
                if (result.Extended?.GS1CompositeBar != null)
                {
                    Console.WriteLine($"Linear (1D) component: {result.Extended.GS1CompositeBar.OneDCodeText}");
                    Console.WriteLine($"2D component: {result.Extended.GS1CompositeBar.TwoDCodeText}");
                    Console.WriteLine($"2D component type: {result.Extended.GS1CompositeBar.TwoDType}");
                }
            }
        }
    }
}