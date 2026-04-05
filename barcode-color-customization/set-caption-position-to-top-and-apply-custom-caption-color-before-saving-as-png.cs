using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the data to encode
            generator.CodeText = "123456";

            // Configure the caption to appear above the barcode (top position)
            generator.Parameters.CaptionAbove.Text = "Top Caption";
            generator.Parameters.CaptionAbove.Visible = true;

            // Apply a custom color to the caption text (e.g., dark green)
            generator.Parameters.CaptionAbove.TextColor = Color.FromArgb(255, 0, 128, 0);

            // Save the barcode image as PNG
            generator.Save("barcode.png");
        }
    }
}