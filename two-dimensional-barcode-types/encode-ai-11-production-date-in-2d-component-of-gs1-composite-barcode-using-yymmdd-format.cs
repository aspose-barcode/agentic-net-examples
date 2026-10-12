// Title: Encode AI 11 Production Date in GS1 Composite Barcode (YYMMDD)
// Description: Demonstrates how to embed the GS1 Application Identifier 11 (production date) in the 2‑D component of a GS1 Composite barcode using the YYMMDD date format.
// Category-Description: This example belongs to the Aspose.BarCode generation suite, focusing on GS1 Composite barcodes. It showcases the use of BarcodeGenerator, EncodeTypes, and GS1CompositeBar parameters to create combined linear‑and‑2D symbols. Developers working with retail, logistics, or inventory systems often need to encode GS1 AI data such as GTIN and production dates in a single barcode for scanning efficiency.
// Prompt: Encode AI 11 (production date) in the 2D component of a GS1 Composite barcode using YYMMDD format.
// Tags: gs1 composite, ai 11, production date, barcode generation, png, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a GS1 Composite barcode that includes GTIN (AI 01) and production date (AI 11) encoded in YYMMDD format.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Build a temporary file path for the output image.
        string outputPath = Path.Combine(Path.GetTempPath(), "GS1Composite_AI11.png");

        // Define the barcode text:
        // (01) – GTIN, (11) – Production Date in YYMMDD (here 991231 = 31 Dec 1999).
        string codeText = "(01)98898765432106|(11)991231";

        // Initialize the generator for a GS1 Composite barcode with the specified text.
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set the X‑dimension (module width) to 2 pixels for better readability.
            gen.Parameters.Barcode.XDimension.Pixels = 2;

            // Hide the human‑readable text; only the barcode image is needed.
            gen.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Configure the 2‑D component to be a CC‑A (Composite Component A) symbol.
            gen.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Set the linear component to GS1‑Code128, the standard for GS1 Composite barcodes.
            gen.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Save the generated barcode as a PNG file.
            gen.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}