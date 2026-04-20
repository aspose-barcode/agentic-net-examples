using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a BarcodeGenerator for GS1 DataBar Stacked symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.DatabarStacked))
        {
            // Sample GS1 DataBar stacked code text (must follow GS1 formatting)
            generator.CodeText = "(01)12345678901231";

            // Configure DataBar parameters:
            // Set aspect ratio to 10 (height/width ratio)
            generator.Parameters.Barcode.DataBar.AspectRatio = 10f;

            // Disable the optional 2D composite component for testing
            generator.Parameters.Barcode.DataBar.Is2DCompositeComponent = false;

            // Save the generated barcode image
            generator.Save("databar_stacked.png");
        }
    }
}