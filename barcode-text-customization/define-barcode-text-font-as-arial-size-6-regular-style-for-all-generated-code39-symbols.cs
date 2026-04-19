using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeExample
{
    class Program
    {
        static void Main()
        {
            // Create a Code39 barcode generator with sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "ABC123"))
            {
                // Configure the human‑readable text font: Arial, 6pt, regular
                var font = generator.Parameters.Barcode.CodeTextParameters.Font;
                font.FamilyName = "Arial";
                font.Size.Point = 6f;
                font.Style = FontStyle.Regular;

                // Save the generated barcode image as PNG
                generator.Save("code39.png");
            }
        }
    }
}