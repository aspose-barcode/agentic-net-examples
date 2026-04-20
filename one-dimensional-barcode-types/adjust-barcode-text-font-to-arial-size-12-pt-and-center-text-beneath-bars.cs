using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with sample code text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the human‑readable text font to Arial, 12 pt
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Center the text horizontally
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Place the text below the bars (default for 1D barcodes, set explicitly for clarity)
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Save the barcode image to a file
            generator.Save("barcode.png");
        }
    }
}