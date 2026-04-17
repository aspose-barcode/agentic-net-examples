using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample codetext
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // ----- Configure main (human‑readable) text font -----
            // Set font family and size for the barcode's code text
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // ----- Configure caption font -----
            // Enable a caption above the barcode
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Sample Caption";

            // Set a different font for the caption
            generator.Parameters.CaptionAbove.Font.FamilyName = "Times New Roman";
            generator.Parameters.CaptionAbove.Font.Size.Point = 14f;

            // Save the barcode image to a file
            generator.Save("barcode.png");
        }
    }
}