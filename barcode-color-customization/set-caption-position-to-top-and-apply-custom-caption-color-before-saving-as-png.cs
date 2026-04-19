using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the data to encode
            generator.CodeText = "123ABC";

            // Configure caption to appear above (top) the barcode
            generator.Parameters.CaptionAbove.Visible = true;               // Ensure caption is visible
            generator.Parameters.CaptionAbove.Text = "Top Caption";        // Caption text
            generator.Parameters.CaptionAbove.TextColor = Color.Blue;      // Custom caption color

            // Save the barcode as PNG
            generator.Save("barcode_with_caption.png");
        }
    }
}