using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for ITF‑14 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.ITF14))
        {
            // Set the value to be encoded
            generator.CodeText = "12345678901234";

            // Right‑align the human‑readable text beneath the barcode
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Right;

            // Save the generated barcode image to a file
            generator.Save("itf14_right_aligned.png");
        }
    }
}