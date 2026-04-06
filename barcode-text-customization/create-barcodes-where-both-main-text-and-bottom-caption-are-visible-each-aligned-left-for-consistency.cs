using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the encoded value
            generator.CodeText = "1234567890";

            // Align the main code text to the left
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Left;

            // Configure the bottom caption
            generator.Parameters.CaptionBelow.Visible = true;               // Make caption visible
            generator.Parameters.CaptionBelow.Text = "Bottom Caption";      // Caption text
            generator.Parameters.CaptionBelow.Alignment = TextAlignment.Left; // Align caption to the left

            // Save the barcode image
            generator.Save("barcode.png");
        }
    }
}