using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeTextColorExample
{
    class Program
    {
        static void Main()
        {
            // Create a barcode generator for Code128 symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                // Set the value to be encoded
                generator.CodeText = "123ABC";

                // Apply a custom color to the human‑readable text (code text)
                // Bar and background colors remain at their defaults
                generator.Parameters.Barcode.CodeTextParameters.Color = Color.Red;

                // Save the barcode image to a PNG file
                generator.Save("barcode.png");
            }
        }
    }
}