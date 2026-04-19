using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample GS1 Composite codetext: linear part (01) and 2D part (21)
        string codetext = "(01)03212345678906|(21)A12345678";

        // Create the barcode generator for GS1 Composite Bar
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codetext))
        {
            // Select Databar Expanded Stacked as the linear component
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.DatabarExpandedStacked;

            // Select CC_A as the 2D component
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Optional: set dimensions for better visibility
            generator.Parameters.Barcode.XDimension.Pixels = 3f;   // X-dimension for both components
            generator.Parameters.Barcode.BarHeight.Pixels = 100f; // Height of the linear component

            // Save the generated barcode image
            generator.Save("gs1_composite.png");
        }
    }
}