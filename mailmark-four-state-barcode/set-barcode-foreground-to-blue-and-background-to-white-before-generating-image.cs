using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the text to encode
            generator.CodeText = "1234567890";

            // Set foreground (bars) color to blue
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set background color to white
            generator.Parameters.BackColor = Color.White;

            // Save the barcode image to a PNG file
            generator.Save("barcode.png");
        }
    }
}