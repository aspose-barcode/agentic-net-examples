using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeFontExample
{
    class Program
    {
        static void Main()
        {
            // Create a barcode generator for Code128 with sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Configure the human‑readable text font: Times New Roman, italic, 16 pt
                generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Times New Roman";
                generator.Parameters.Barcode.CodeTextParameters.Font.Style = FontStyle.Italic;
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 16f;

                // Save the generated barcode image to a file
                generator.Save("barcode.png");
            }
        }
    }
}