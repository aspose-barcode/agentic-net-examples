using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a UPC-A barcode generator with sample code text
        using (var generator = new BarcodeGenerator(EncodeTypes.UPCA, "12345678901"))
        {
            // Set a caption above the barcode
            generator.Parameters.CaptionAbove.Text = "UPC-A Sample";

            // Set the caption text color to Purple using Color.FromName
            generator.Parameters.CaptionAbove.TextColor = Color.FromName("Purple");

            // Save the barcode image to a PNG file
            generator.Save("upca.png");
        }
    }
}