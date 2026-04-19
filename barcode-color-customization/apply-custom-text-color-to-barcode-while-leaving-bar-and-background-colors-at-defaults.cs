using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeCustomTextColor
{
    class Program
    {
        static void Main()
        {
            // Create a barcode generator for Code128 with sample code text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
            {
                // Apply a custom color to the human‑readable text.
                // Bar and background colors remain at their default values.
                generator.Parameters.Barcode.CodeTextParameters.Color = Color.Blue;

                // Save the generated barcode image to a file.
                generator.Save("barcode_custom_text_color.png");
            }

            Console.WriteLine("Barcode generated successfully.");
        }
    }
}