using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Decorative"))
        {
            // Set the human‑readable text font to Comic Sans MS, 11 pt
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Comic Sans MS";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 11f;

            // Align the text centrally
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Optional: define image size
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Save the barcode image
            generator.Save("decorative_label.png");
        }
    }
}