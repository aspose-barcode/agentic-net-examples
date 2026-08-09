// Title: Generate a dense MaxiCode barcode with custom module size
// Description: Demonstrates how to create a MaxiCode barcode (Mode 2) using Aspose.BarCode, set the module size to produce a denser image suitable for compact labels, and save it as PNG.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on MaxiCode symbology. It shows how to use the ComplexBarcodeGenerator with MaxiCodeCodetextMode2, configure barcode parameters such as XDimension and resolution, and output the result. Developers working with shipping, logistics, or inventory systems often need to generate high‑density MaxiCode images for small packaging.
// Prompt: Set the generator's ModuleSize property to 2 to produce a denser MaxiCode image for compact labels.
// Tags: maxicode, barcode, module size, densify, complexbarcode, generation, png, aspnet.barcode

using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a dense MaxiCode barcode (Mode 2) and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a MaxiCode codetext, configures the generator,
    /// and saves the barcode image.
    /// </summary>
    static void Main()
    {
        // Create MaxiCode codetext for Mode 2 (postal + data)
        var maxiCodeData = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",
            CountryCode = 56,
            ServiceCategory = 999,
            // Add a secondary message to the barcode
            SecondMessage = new MaxiCodeStandardSecondMessage { Message = "Sample data" }
        };

        // Initialize the ComplexBarcodeGenerator with the codetext
        using (var generator = new ComplexBarcodeGenerator(maxiCodeData))
        {
            // Set the module size (XDimension) to 2 points for a denser MaxiCode
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Optional: set image resolution (dots per inch)
            generator.Parameters.Resolution = 300;

            // Generate and save the barcode image to a file
            generator.Save("maxicode_dense.png");
        }
    }
}