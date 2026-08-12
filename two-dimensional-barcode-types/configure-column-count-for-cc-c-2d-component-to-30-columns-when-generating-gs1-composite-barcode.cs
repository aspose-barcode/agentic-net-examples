// Title: Generate GS1 Composite barcode with CC_C component and custom column count
// Description: Demonstrates how to create a GS1 Composite barcode where the 2‑D component uses the CC_C (PDF417) symbology with a column count of 30. Shows setting linear and 2‑D component types and adjusting visual parameters before saving as an image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on composite symbologies. It illustrates using the BarcodeGenerator class together with EncodeTypes, TwoDComponentType, and PDF417 parameters to configure both linear and 2‑D parts of a GS1 Composite barcode. Developers commonly need to customize component settings such as column count, module size, and bar height when generating composite barcodes for packaging or labeling solutions.
// Prompt: Configure column count for CC_C 2D component to 30 columns when generating a GS1 Composite barcode.
// Tags: gs1 composite, pdf417, column count, barcode generation, aspose.barcode, image output

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a GS1 Composite barcode with a CC_C (PDF417) 2‑D component
/// configured to use 30 columns. The barcode is saved as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Builds the composite barcode text, configures generator parameters,
    /// and saves the resulting image to disk.
    /// </summary>
    static void Main()
    {
        // Define the linear and 2‑D parts of the GS1 Composite barcode.
        // Both parts contain a 14‑digit GTIN (AI (01)) as required by the standard.
        string linearPart = "(01)01234567890123";   // Linear component data
        string twoDPart   = "(01)00123456789012";   // 2‑D component data
        string codeText   = $"{linearPart}|{twoDPart}";

        // Initialize the barcode generator for the GS1 Composite symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set the linear component to use GS1 Code128.
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Set the 2‑D component to use CC_C (PDF417) and configure its column count.
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_C;
            generator.Parameters.Barcode.Pdf417.Columns = 30;

            // Optional visual tweaks: increase module size and bar height for better readability.
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.Barcode.BarHeight.Point = 100f;

            // Save the generated barcode as a PNG image.
            generator.Save("gs1_composite_ccc.png");
        }

        Console.WriteLine("GS1 Composite barcode with CC_C (30 columns) generated successfully.");
    }
}