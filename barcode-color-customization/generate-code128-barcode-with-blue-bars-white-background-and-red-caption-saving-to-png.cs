using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a Code128 barcode generator
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the text to encode
            generator.CodeText = "ABC123";

            // Set bar color to blue
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set background color to white
            generator.Parameters.BackColor = Color.White;

            // Configure a red caption above the barcode
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Red Caption";
            generator.Parameters.CaptionAbove.TextColor = Color.Red;
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;

            // Save the barcode as PNG
            generator.Save("code128.png");
        }
    }
}