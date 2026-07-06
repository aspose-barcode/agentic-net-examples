using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for ITF‑14 symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.ITF14))
        {
            // Set the data to encode (14‑digit example)
            generator.CodeText = "12345678901231";

            // Right‑align the human‑readable text
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Right;

            // Save the barcode image to a PNG file
            generator.Save("itf14_right_aligned.png");
        }
    }
}