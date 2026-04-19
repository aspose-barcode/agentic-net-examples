using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // 1D (linear) and 2D parts are separated by the '|' character as required for GS1 Composite.
        string compositeCodeText = "(01)03212345678906|(21)A1B2C3D4E5F6G7H8";

        // Create the barcode generator for GS1 Composite Bar.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, compositeCodeText))
        {
            // Set the linear component type (e.g., GS1 Code128).
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Set the 2D component type (e.g., CC-A – MicroPDF417 variant).
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Configure dimensions.
            generator.Parameters.Barcode.XDimension.Pixels = 3f;   // X-dimension for both components.
            generator.Parameters.Barcode.BarHeight.Pixels = 100f; // Height of the linear component.

            // Optional: set colors.
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the barcode image.
            generator.Save("gs1composite.png");
        }
    }
}