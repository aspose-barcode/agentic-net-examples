using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 symbology with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the data to be encoded
            generator.CodeText = "123456";

            // Set barcode bars to black
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Set background to white
            generator.Parameters.BackColor = Color.White;

            // Configure the caption below the barcode
            generator.Parameters.CaptionBelow.Text = "Sample Caption";
            generator.Parameters.CaptionBelow.Visible = true;
            generator.Parameters.CaptionBelow.TextColor = Color.Green;

            // Save the barcode image to a file
            generator.Save("barcode.png");
        }
    }
}