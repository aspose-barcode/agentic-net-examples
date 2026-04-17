using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Sample GS1 Code 128 data (AI 01 for GTIN)
        string codeText = "(01)12345678901231";

        // Create the barcode generator for GS1 Code 128
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
        {
            // Apply a custom font to the human‑readable text
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Save the barcode as PNG
            generator.Save("gs1code128.png");
        }
    }
}