using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the actual data to encode
            generator.CodeText = "1234567890";

            // Hide the main codetext by setting its location to None
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Configure the top caption (above the barcode) to display supplemental information
            generator.Parameters.CaptionAbove.Visible = true;
            generator.Parameters.CaptionAbove.Text = "Supplemental Info";

            // Optional: set image size (using unit members)
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Save the barcode image to a PNG file
            generator.Save("barcode_with_caption.png");
        }
    }
}