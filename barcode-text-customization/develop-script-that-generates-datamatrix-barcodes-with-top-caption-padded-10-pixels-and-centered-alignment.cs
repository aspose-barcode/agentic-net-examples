using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a DataMatrix barcode generator with sample codetext
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "123456"))
        {
            // Configure the top caption (CaptionAbove)
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Top Caption";

            // Center the caption horizontally
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;

            // Add 10 pixels padding to the top of the caption
            generator.Parameters.CaptionAbove.Padding.Top.Pixels = 10f;

            // Save the generated barcode image
            generator.Save("datamatrix.png");
        }
    }
}