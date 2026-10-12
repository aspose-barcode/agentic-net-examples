// Title: Generate GS1 Composite barcode with CC_C component and 30 PDF417 columns
// Description: Demonstrates how to create a GS1 Composite barcode where the 2‑D component is CC_C and its PDF417 matrix is configured to 30 columns. Shows setting linear component type, visual options, and saving as PNG.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on composite symbologies. It illustrates using BarcodeGenerator, EncodeTypes, and GS1CompositeBar parameters to customize both linear and 2‑D components, a common requirement when integrating GS1 barcodes into packaging or labeling workflows. Developers often need to adjust PDF417 settings such as column count to meet industry specifications.
// Prompt: Configure column count for CC_C 2D component to 30 columns when generating a GS1 Composite barcode.
// Tags: gs1 composite, pdf417, column count, barcode generation, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a GS1 Composite barcode with a CC_C 2‑D component
/// and configures the PDF417 matrix to use 30 columns.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates the barcode, applies required settings, and saves the image.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for the generated image
        string outputDir = Path.Combine(Path.GetTempPath(), "Gs1CompositeExample");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "GS1Composite_CC_C_30Columns.png");

        // GS1 Composite barcode data: linear part + 2‑D part separated by '|'
        string codeText = "(01)98898765432106(3202)012345|(10)ABCD0123(240)0123456789";

        // Initialize the barcode generator with GS1 Composite symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set the 2‑D component type to CC_C (requires PDF417)
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_C;

            // Linear component must be GS1 Code128 for CC_C composite barcodes
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Configure PDF417 to use 30 columns for the CC_C component
            generator.Parameters.Barcode.Pdf417.Columns = 30;

            // Optional visual tweaks: reduce module size and hide human‑readable text
            generator.Parameters.Barcode.XDimension.Pixels = 2;
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}