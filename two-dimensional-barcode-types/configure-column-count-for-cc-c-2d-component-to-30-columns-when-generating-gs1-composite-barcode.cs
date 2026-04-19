using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample GS1 Composite barcode data: linear part and 2D part separated by '|'
        string codeText = "(01)03212345678906|(21)A12345678";

        // Create the generator for GS1 Composite Bar symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set linear component type (e.g., GS1-128)
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Choose CC_C as the 2D component (PDF417)
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_C;

            // Optional: set X-Dimension and BarHeight for better visibility
            generator.Parameters.Barcode.XDimension.Pixels = 3;
            generator.Parameters.Barcode.BarHeight.Pixels = 100;

            // Save the generated barcode image
            generator.Save("gs1_composite_ccc.png");
        }
    }
}