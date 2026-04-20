using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 (suitable for receipt printing)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Align human‑readable text to the center
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Optional: set font properties for better readability on receipts
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 8f;

            // Enable automatic scaling (interpolation) and define target image size
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 200f;   // narrow receipt width
            generator.Parameters.ImageHeight.Point = 80f;   // appropriate height for the barcode

            // Save the barcode image as PNG
            generator.Save("receipt_barcode.png");
        }
    }
}