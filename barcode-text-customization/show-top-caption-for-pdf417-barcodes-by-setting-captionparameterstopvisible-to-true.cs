using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a PDF417 barcode generator with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample PDF417"))
        {
            // Enable the top caption (CaptionAbove) visibility
            generator.Parameters.CaptionAbove.Visible = true;
            // Optional: set caption text
            generator.Parameters.CaptionAbove.Text = "Top Caption";

            // Save the barcode image to a file
            generator.Save("pdf417.png");
        }
    }
}