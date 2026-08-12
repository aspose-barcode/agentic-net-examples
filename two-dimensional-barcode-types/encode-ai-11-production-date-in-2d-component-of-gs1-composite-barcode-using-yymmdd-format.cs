// Title: Encode AI 11 Production Date in GS1 Composite Barcode (YYMMDD)
// Description: Demonstrates how to embed the AI 11 production date in the 2‑dimensional component of a GS1 Composite barcode using the YYMMDD format.
// Category-Description: This example belongs to the Aspose.BarCode GS1 Composite barcode generation category. It shows how to combine linear (GS1 Code 128) and 2‑D (MicroPDF417 CC‑A) components, set visual parameters, and save the result as an image. Developers working with GS1 standards often need to create composite barcodes that carry both human‑readable and machine‑readable data, using classes like BarcodeGenerator, EncodeTypes, and TwoDComponentType.
// Prompt: Encode AI 11 (production date) in the 2D component of a GS1 Composite barcode using YYMMDD format.
// Tags: gs1 composite, barcode generation, png, aspose.barcode, encode types, two d component

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Program demonstrating encoding of AI 11 production date in a GS1 Composite barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates and saves a GS1 Composite barcode with AI 11 date.
    /// </summary>
    static void Main()
    {
        // Linear component (example GTIN) – AI 01
        string linearComponent = "(01)12345678901231";

        // Production date in YYMMDD format – AI 11
        string productionDate = DateTime.Now.ToString("yyMMdd");
        string twoDComponent = $"(11){productionDate}";

        // Combine linear and 2‑D parts with the required '|' separator
        string codeText = $"{linearComponent}|{twoDComponent}";

        // Create the GS1 Composite barcode generator with the combined data
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set linear component type to GS1 Code 128
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Set 2‑D component type to CC‑A (MicroPDF417)
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Optional visual settings: module size and bar height
            generator.Parameters.Barcode.XDimension.Pixels = 3f;
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;

            // Save the barcode image as PNG
            string outputFile = "gs1_composite.png";
            generator.Save(outputFile);
            Console.WriteLine($"GS1 Composite barcode saved to: {outputFile}");
        }
    }
}