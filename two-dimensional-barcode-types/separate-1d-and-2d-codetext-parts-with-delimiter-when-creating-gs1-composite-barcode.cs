// Title: Create a GS1 Composite barcode with separate 1D and 2D components
// Description: Demonstrates how to build a GS1 Composite barcode by concatenating a linear GS1 Code128 component and a 2D PDF417 component using the ‘|’ delimiter.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.GS1CompositeBar. It shows configuring linear and 2D component types, adjusting visual parameters, and saving the result as an image. Developers working with GS1 standards often need to combine 1D and 2D data for logistics and retail applications.
// Prompt: Separate 1D and 2D CodeText parts with ‘|’ delimiter when creating a GS1 Composite barcode.
// Tags: gs1 composite, barcode generation, png, aspose.barcode, encode types

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a GS1 Composite barcode
/// containing separate linear (1D) and 2D components.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Builds the composite CodeText, configures the generator,
    /// and saves the barcode image to disk.
    /// </summary>
    static void Main()
    {
        // Linear (1D) component: GS1 Code128 with AI (01) GTIN-14 and AI (21) serial number
        string linearComponent = "(01)00123456789012(21)ABC123";

        // 2D component: PDF417 with AI (01) GTIN-14 and AI (10) batch/lot number
        string twoDComponent = "(01)00123456789012(10)BATCH01";

        // Concatenate components with '|' as required for GS1 Composite barcodes
        string codeText = $"{linearComponent}|{twoDComponent}";

        // Initialize the barcode generator for GS1 Composite Bar
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Specify that the linear part uses GS1 Code128
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Choose the 2D component type (CC_A corresponds to MicroPDF417)
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Optional visual settings
            generator.Parameters.Barcode.XDimension.Point = 2f;          // Module size (X-dimension)
            generator.Parameters.Barcode.BarHeight.Point = 100f;        // Height of the linear (1D) part
            generator.Parameters.Barcode.Pdf417.AspectRatio = 3f;       // Aspect ratio for the PDF417 (2D) part

            // Save the generated barcode as a PNG image
            string outputPath = "gs1_composite.png";
            generator.Save(outputPath);
            Console.WriteLine($"GS1 Composite barcode saved to {outputPath}");
        }
    }
}