using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a PDF417 barcode generator with sample code text
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "1234567890"))
        {
            // Enable the top caption (CaptionAbove) and set its text
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Top Caption";

            // Optionally adjust caption appearance (font size, alignment, etc.)
            generator.Parameters.CaptionAbove.Font.Size.Point = 12f;
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;

            // Save the barcode image to a PNG file
            generator.Save("pdf417_caption.png");
        }
    }
}