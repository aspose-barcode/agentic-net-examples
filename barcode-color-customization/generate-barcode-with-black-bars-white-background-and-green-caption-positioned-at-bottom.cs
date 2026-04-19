using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the encoded value
            generator.CodeText = "123456";

            // Set barcode colors: black bars on white background
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Configure the caption below the barcode
            generator.Parameters.CaptionBelow.Visible = true;
            generator.Parameters.CaptionBelow.Text = "Sample Caption";
            generator.Parameters.CaptionBelow.TextColor = Color.Green;
            generator.Parameters.CaptionBelow.Alignment = TextAlignment.Center;

            // Save the barcode image to a file
            generator.Save("barcode.png");
        }
    }
}