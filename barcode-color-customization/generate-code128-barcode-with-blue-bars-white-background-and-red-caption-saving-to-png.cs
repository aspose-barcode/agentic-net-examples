using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeExample
{
    class Program
    {
        static void Main()
        {
            // Create a Code128 barcode generator
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                // Set the text to encode
                generator.CodeText = "123ABC";

                // Set bar (foreground) color to blue
                generator.Parameters.Barcode.BarColor = Color.Blue;

                // Set background color to white
                generator.Parameters.BackColor = Color.White;

                // Configure caption (above the barcode)
                generator.Parameters.CaptionAbove.Visible = true;
                generator.Parameters.CaptionAbove.Text = "Sample Caption";
                generator.Parameters.CaptionAbove.TextColor = Color.Red;

                // Save the barcode as PNG
                generator.Save("code128.png");
            }
        }
    }
}