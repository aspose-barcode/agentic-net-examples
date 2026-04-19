using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a DataMatrix barcode generator with sample code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Sample123"))
        {
            // Set bottom caption padding to 8 pixels to create visual separation.
            generator.Parameters.CaptionBelow.Padding.Bottom.Pixels = 8f;

            // Optionally set a caption text to see the padding effect.
            generator.Parameters.CaptionBelow.Text = "Bottom Caption";

            // Save the barcode image to a PNG file.
            generator.Save("datamatrix.png");
        }
    }
}