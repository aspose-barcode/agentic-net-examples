using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code39 with sample text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39, "1234567890"))
        {
            // Configure human‑readable text to use a custom font.
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Courier New";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Optional: set the text color.
            generator.Parameters.Barcode.CodeTextParameters.Color = Color.DarkBlue;

            // Export the barcode as an SVG file for scalable rendering.
            generator.Save("custom_font_barcode.svg");
        }
    }
}