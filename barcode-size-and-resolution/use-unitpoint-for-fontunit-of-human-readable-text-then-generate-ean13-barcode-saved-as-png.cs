using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeExample
{
    class Program
    {
        static void Main()
        {
            // Create a barcode generator for EAN13 with a 12‑digit code (checksum will be added automatically)
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.EAN13, "123456789012"))
            {
                // Set human‑readable text font using Unit.Point
                generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f; // 12 pt font size

                // Optional: align text and place it below the barcode
                generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

                // Save the barcode image as PNG
                generator.Save("ean13.png");
            }
        }
    }
}