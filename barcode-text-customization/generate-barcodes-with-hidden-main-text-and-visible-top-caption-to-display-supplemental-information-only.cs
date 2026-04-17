using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 with some dummy code text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Hide the main barcode text (human‑readable code text).
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Set a top caption that will be visible.
            generator.Parameters.CaptionAbove.Text = "Batch 001";
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;

            // Optional: adjust image size if needed.
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Save the barcode image.
            generator.Save("barcode.png");
        }
    }
}