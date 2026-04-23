using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the text to encode
            generator.CodeText = "1234567890";

            // Configure the human‑readable text font: Verdana, Bold, 14 pt
            var font = generator.Parameters.Barcode.CodeTextParameters.Font;
            font.FamilyName = "Verdana";
            font.Size.Point = 14f;
            font.Style = FontStyle.Bold;

            // Save the barcode image as PNG
            generator.Save("barcode.png");
        }
    }
}