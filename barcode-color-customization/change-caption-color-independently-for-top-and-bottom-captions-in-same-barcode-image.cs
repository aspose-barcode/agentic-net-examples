using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeCaptionColorExample
{
    class Program
    {
        static void Main()
        {
            // Create a barcode generator for Code128 symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                // Set the code text to be encoded
                generator.CodeText = "123456";

                // Configure the top caption (above the barcode)
                generator.Parameters.CaptionAbove.Visible = true;
                generator.Parameters.CaptionAbove.Text = "Top Caption";
                generator.Parameters.CaptionAbove.TextColor = Color.Blue;

                // Configure the bottom caption (below the barcode)
                generator.Parameters.CaptionBelow.Visible = true;
                generator.Parameters.CaptionBelow.Text = "Bottom Caption";
                generator.Parameters.CaptionBelow.TextColor = Color.Green;

                // Save the barcode image with the customized captions
                generator.Save("barcode_with_captions.png");
            }
        }
    }
}