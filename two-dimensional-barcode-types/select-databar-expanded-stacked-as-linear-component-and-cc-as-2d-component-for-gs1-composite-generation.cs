// Title: Generate GS1 Composite barcode with DataBar Expanded Stacked and CC_A components
// Description: Demonstrates creating a GS1 Composite barcode where the linear component is DataBar Expanded Stacked and the 2D component is CC_A, then saving the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on composite barcode creation. It showcases the use of BarcodeGenerator, EncodeTypes, and TwoDComponentType to configure GS1 Composite barcodes—common when encoding product identifiers alongside additional data. Developers often need to combine linear and 2D symbologies for GS1 standards, and this snippet illustrates the typical setup and saving workflow.
// Prompt: Select Databar Expanded Stacked as linear component and CC_A as 2D component for GS1 Composite generation.
// Tags: gs1 composite, databar expanded stacked, cc_a, barcode generation, png, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a GS1 Composite barcode with a DataBar Expanded Stacked linear component
/// and a CC_A 2D component, then saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Builds the composite barcode and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Sample GS1 Composite codetext:
        //   - 1D part (DataBar) encodes a GTIN (01) and a serial number (21)
        //   - 2D part (CC_A) encodes the same data in a 2‑dimensional component
        //   Parts are separated by the '|' character as required by the GS1 Composite format.
        string codetext = "(01)01234567890123|(21)ABC123";

        // Initialize the barcode generator for a GS1 Composite barcode using the provided codetext.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codetext))
        {
            // Configure the linear component to use DataBar Expanded Stacked.
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.DatabarExpandedStacked;

            // Configure the 2D component to use the CC_A symbology.
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Optional visual tweaks for better readability:
            //   - X dimension (module width) set to 3 pixels.
            //   - Bar height set to 100 pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 3f;
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;

            // Save the generated barcode image to a PNG file in the application directory.
            generator.Save("gs1composite.png");
        }
    }
}