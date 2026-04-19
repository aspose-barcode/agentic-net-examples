using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace GS1CompositeExample
{
    class Program
    {
        static void Main()
        {
            // Sample GS1 Composite barcode text.
            // Linear (1D) part: (01)03212345678906  – GTIN
            // 2D part: (21)A12345678          – Serial number
            // Parts are separated by the '|' character as required for GS1 Composite.
            string codetext = "(01)03212345678906|(21)A12345678";

            // Create the barcode generator for GS1 Composite Bar symbology.
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codetext))
            {
                // Set the linear component to GS1 Code128.
                generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

                // Set the 2D component to CC_B (MicroPDF417).
                generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_B;

                // Optional: define dimensions for both components.
                generator.Parameters.Barcode.XDimension.Pixels = 3f;   // X-dimension for 1D and 2D parts.
                generator.Parameters.Barcode.BarHeight.Pixels = 100f; // Height of the linear component.

                // Save the generated barcode image to a PNG file.
                generator.Save("GS1CompositeBar.png");
            }

            // Indicate successful completion.
            Console.WriteLine("GS1 Composite barcode generated successfully.");
        }
    }
}