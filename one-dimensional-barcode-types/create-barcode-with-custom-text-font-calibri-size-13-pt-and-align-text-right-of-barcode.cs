using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the text to be encoded
            generator.CodeText = "1234567890";

            // Configure human‑readable text font: Calibri, 13 pt
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Calibri";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 13f;

            // Align the text to the right of the barcode
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Right;

            // Save the barcode image to a file
            generator.Save("barcode.png");
        }
    }
}