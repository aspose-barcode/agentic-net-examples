using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample code text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set custom font: Verdana, 10 pt
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Verdana";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 10f;

            // Align the human‑readable text to the left of the bars
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Left;

            // Save the generated barcode image
            generator.Save("barcode.png");
        }
    }
}