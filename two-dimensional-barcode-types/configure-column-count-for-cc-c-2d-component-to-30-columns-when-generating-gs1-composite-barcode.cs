// Title: Generate GS1 Composite barcode with CC_C component and custom column count
// Description: Demonstrates configuring the PDF417 (CC_C) component of a GS1 Composite barcode to use 30 columns, then saving the image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on GS1 Composite symbology. It shows how to set linear and 2D component types, adjust PDF417 parameters, and customize common barcode settings. Developers creating composite barcodes for packaging, logistics, or retail can use these APIs to meet specification requirements.
// Prompt: Configure column count for CC_C 2D component to 30 columns when generating a GS1 Composite barcode.
// Tags: gs1 composite, pdf417, column count, barcode generation, aspose.barcode, csharp

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a GS1 Composite barcode with a CC_C (PDF417) 2D component
/// configured to 30 columns, and saving it as an image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a BarcodeGenerator, configures parameters,
    /// and saves the resulting barcode image.
    /// </summary>
    static void Main()
    {
        // Define the GS1 Composite code text: linear part and 2D part separated by '|'
        string codetext = "(01)03212345678906|(21)A12345678";

        // Initialize the generator for GS1 Composite barcode with the specified code text
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codetext))
        {
            // Set the linear component to GS1 Code128
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Set the 2D component type to CC_C (PDF417)
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_C;

            // Configure PDF417 to use 30 columns, as required for the CC_C component
            generator.Parameters.Barcode.Pdf417.Columns = 30;

            // Optional: improve visual quality by adjusting size and resolution
            generator.Parameters.Barcode.XDimension.Pixels = 2f;      // module width
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;   // linear component height
            generator.Parameters.Resolution = 96;                   // image DPI

            // Save the generated barcode to a PNG file
            string outputPath = "gs1_composite_cc_c.png";
            generator.Save(outputPath);
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}