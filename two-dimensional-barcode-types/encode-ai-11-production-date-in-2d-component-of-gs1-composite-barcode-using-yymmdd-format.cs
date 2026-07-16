// Title: Encode AI 11 Production Date in GS1 Composite Barcode
// Description: Demonstrates encoding AI 11 (production date) in the 2D component of a GS1 Composite barcode using YYMMDD format and saving the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode GS1 Composite barcode generation category. It illustrates how to combine linear and 2D components, configure component types, and adjust dimensions using the BarcodeGenerator, EncodeTypes, and TwoDComponentType classes. Developers creating GS1 Composite barcodes for product labeling, inventory, or logistics can use this pattern to embed additional data such as production dates.
// Prompt: Encode AI 11 (production date) in the 2D component of a GS1 Composite barcode using YYMMDD format.
// Tags: gs1 composite, barcode generation, encode types, two d component, png output, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace GS1CompositeExample
{
    /// <summary>
    /// Demonstrates encoding AI 11 (production date) in the 2D component of a GS1 Composite barcode.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point. Generates a GS1 Composite barcode with a linear GTIN component and a 2D AI‑11 production date component, then saves it as a PNG file.
        /// </summary>
        static void Main()
        {
            // Linear component (e.g., GTIN) – required for the GS1 Composite barcode
            string linearComponent = "(01)01234567890123";

            // 2D component: AI 11 (production date) in YYMMDD format, e.g., 2023‑07‑31 => 230731
            string twoDComponent = "(11)230731";

            // Combine linear and 2D parts with the '|' separator required for GS1 Composite barcodes
            string codeText = $"{linearComponent}|{twoDComponent}";

            // Create the barcode generator for a GS1 Composite Bar with the combined codetext
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
            {
                // Set the linear component type (GS1 Code 128 is typical for the linear part)
                generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

                // Choose a 2D component type; CC_A corresponds to a MicroPDF417 variant
                generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

                // Optional: adjust dimensions for better readability
                generator.Parameters.Barcode.XDimension.Pixels = 3f;   // module width
                generator.Parameters.Barcode.BarHeight.Pixels = 100f; // height of the linear part

                // Save the generated barcode image to a PNG file
                generator.Save("gs1composite.png");
            }

            Console.WriteLine("GS1 Composite barcode generated: gs1composite.png");
        }
    }
}