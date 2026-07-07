using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 (any symbology can be used)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the text to be encoded
            generator.CodeText = "1234567890";

            // Configure human‑readable text (code text) appearance
            // Font family
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Verdana";
            // Font size 14 pt
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 14f;
            // Bold style – set to true if the property exists
            // (If the Font object does not expose a Bold property, this line can be omitted)
            // generator.Parameters.Barcode.CodeTextParameters.Font.Bold = true;

            // Save the barcode image to a PNG file
            generator.Save("barcode.png");
        }

        Console.WriteLine("Barcode generated and saved as barcode.png");
    }
}