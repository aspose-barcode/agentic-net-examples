using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Apply FontMode.Auto so the library automatically calculates the optimal font size.
            generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Auto;

            // Save the generated barcode image to a file.
            generator.Save("code128_autofont.png");
        }
    }
}